using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisContact;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.HisContactPoint.SetContactLevel
{
    partial class HisContactPointSetContactLevel : BusinessBase
    {
        private HisContactPointCreate hisContactPointCreate;
        private HisContactPointUpdate hisContactPointUpdate;

        internal HisContactPointSetContactLevel()
            : base()
        {
            this.Init();
        }

        internal HisContactPointSetContactLevel(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisContactPointCreate = new HisContactPointCreate(param);
            this.hisContactPointUpdate = new HisContactPointUpdate(param);
        }

        internal bool Run(HisContactLevelSDO sdo, ref HIS_CONTACT_POINT resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_CONTACT_POINT contactPoint = null;
                HisContactPointSetContactLevelCheck checker = new HisContactPointSetContactLevelCheck(param);
                HisContactPointCheck contactPointChecker = new HisContactPointCheck(param);
                valid = valid && contactPointChecker.VerifyId(sdo.ContactPointId, ref contactPoint);

                if (valid)
                {
                    //Neu thong tin can set khac voi thong tin hien tai thi moi thuc hien update
                    if (contactPoint.CONTACT_LEVEL.Value != sdo.ContactLevel)
                    {
                        List<HIS_CONTACT_POINT> toUpdates = new List<HIS_CONTACT_POINT>();

                        this.SetContactLevel(contactPoint, sdo.ContactLevel, true, ref toUpdates);

                        if (!IsNotNullOrEmpty(toUpdates) || this.hisContactPointUpdate.UpdateList(toUpdates))
                        {
                            result = true;
                            contactPoint.CONTACT_LEVEL = sdo.ContactLevel;
                            resultData = contactPoint;
                        }
                    }
                    else
                    {
                        result = true;
                        resultData = contactPoint;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void SetContactLevel(long contactPointId, long toUpdateContactLevel, bool isForceUpdate, ref List<HIS_CONTACT_POINT> traversaledList)
        {
            //Lay thong tin nguoi benh tuong ung
            HIS_CONTACT_POINT contactPoint = new HisContactPointGet().GetById(contactPointId);

            this.SetContactLevel(contactPoint, toUpdateContactLevel, isForceUpdate, ref traversaledList);
        }

        internal void SetContactLevel(HIS_CONTACT_POINT contactPoint, long toUpdateContactLevel, bool isForceUpdate, ref List<HIS_CONTACT_POINT> traversaledList)
        {
            if (contactPoint == null)
            {
                return;
            }

            //Neu la bat buoc cap nhat (trong truong hop nguoi dung chon update) 
            //hoac gia tri phan loai lon hon thi moi thuc hien cap nhat
            if (contactPoint.CONTACT_LEVEL.HasValue && contactPoint.CONTACT_LEVEL <= toUpdateContactLevel && !isForceUpdate)
            {
                return;
            }

            if (traversaledList == null)
            {
                traversaledList = new List<HIS_CONTACT_POINT>();
            }

            contactPoint.CONTACT_LEVEL = toUpdateContactLevel;
            traversaledList.Add(contactPoint);

            //Lay cac du lieu tiep xuc tuong ung voi contact-point-id
            HisContactFilterQuery filter = new HisContactFilterQuery();
            filter.CONTACT_POINT1_ID__OR__CONTACT_POINT2_ID = contactPoint.ID;
            List<HIS_CONTACT> contacts = new HisContactGet().Get(filter);

            if (IsNotNullOrEmpty(contacts))
            {
                //D/s contact-point-id da duoc duyet
                List<long> traversaledIds = traversaledList != null ? traversaledList.Select(o => o.ID).ToList() : null;

                //Danh sach can duyet
                List<long> toTraversalIds = new List<long>();
                //Chi lay cac id ko phai la contact_point dang duyet va ko phai contact-point da duoc duyet
                List<long> ids1 = contacts
                    .Where(o => o.CONTACT_POINT1_ID != contactPoint.ID
                        && (traversaledIds == null || !traversaledIds.Contains(o.CONTACT_POINT1_ID)))
                    .Select(o => o.CONTACT_POINT1_ID).Distinct().ToList();
                List<long> ids2 = contacts
                    .Where(o => o.CONTACT_POINT2_ID != contactPoint.ID
                        && (traversaledIds == null || !traversaledIds.Contains(o.CONTACT_POINT2_ID)))
                    .Select(o => o.CONTACT_POINT2_ID).Distinct().ToList();

                if (IsNotNullOrEmpty(ids1))
                {
                    toTraversalIds.AddRange(ids1);
                }
                if (IsNotNullOrEmpty(ids2))
                {
                    toTraversalIds.AddRange(ids2);
                }

                if (IsNotNullOrEmpty(toTraversalIds))
                {
                    foreach (long id in toTraversalIds)
                    {
                        this.SetContactLevel(id, contactPoint.CONTACT_LEVEL.Value + 1, false, ref traversaledList);
                    }
                }
            }
        }
    }
}
