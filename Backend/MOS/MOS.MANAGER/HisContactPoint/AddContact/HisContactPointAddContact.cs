using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisContact;
using MOS.MANAGER.HisContactPoint.Save;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using MOS.MANAGER.HisContactPoint.SetContactLevel;

namespace MOS.MANAGER.HisContactPoint.AddContact
{
    partial class HisContactPointAddContact : BusinessBase
    {
        private HisContactPointCreate hisContactPointCreate;
        private HisContactPointUpdate hisContactPointUpdate;
        private HisContactCreate hisContactCreate;
        private HisContactUpdate hisContactUpdate;

        internal HisContactPointAddContact()
            : base()
        {
            this.Init();
        }

        internal HisContactPointAddContact(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisContactPointCreate = new HisContactPointCreate(param);
            this.hisContactPointUpdate = new HisContactPointUpdate(param);
            this.hisContactCreate = new HisContactCreate(param);
            this.hisContactUpdate = new HisContactUpdate(param);
        }

        internal bool Run(HisContactSDO data, ref HisContactResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_CONTACT_POINT patient = null;
                HisContactPointAddContactCheck checker = new HisContactPointAddContactCheck(param);
                HisContactPointSaveCheck saveChecker = new HisContactPointSaveCheck(param);
                valid = valid && checker.IsValidData(data);
                valid = valid && saveChecker.IsValidData(data);
                valid = valid && saveChecker.IsNotExists(data);
                valid = valid && checker.IsValidContactLevel(data, ref patient);

                if (valid)
                {
                    Mapper.CreateMap<HIS_CONTACT_POINT, HIS_CONTACT_POINT>();
                    HIS_CONTACT_POINT contactPeople = Mapper.Map<HIS_CONTACT_POINT>(data);
                    bool isExistContactPeople = false;

                    //Neu truong hop them moi
                    if (contactPeople.ID <= 0)
                    {

                        if (!this.hisContactPointCreate.Create(contactPeople))
                        {
                            return false;
                        }
                    }
                    else if (contactPeople.ID > 0)
                    {
                        isExistContactPeople = true;

                        HIS_CONTACT_POINT oldContactPeople = new HisContactPointGet().GetById(contactPeople.ID);

                        //Neu co su thay doi contact-level thi xu ly de ngoai viec update thong tin thi con update 
                        //ca contact-level cua cac doi tuong tiep xuc lien quan
                        if (oldContactPeople.CONTACT_LEVEL.HasValue && contactPeople.CONTACT_LEVEL.HasValue && oldContactPeople.CONTACT_LEVEL > contactPeople.CONTACT_LEVEL)
                        {
                            List<HIS_CONTACT_POINT> toUpdateList = new List<HIS_CONTACT_POINT>();
                            new HisContactPointSetContactLevel().SetContactLevel(contactPeople, contactPeople.CONTACT_LEVEL.Value, true, ref toUpdateList);

                            if (IsNotNullOrEmpty(toUpdateList))
                            {
                                if (!this.hisContactPointUpdate.UpdateList(toUpdateList))
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            if (!this.hisContactPointUpdate.Update(contactPeople))
                            {
                                return false;
                            }
                        }

                    }
                    HIS_CONTACT contact = null;
                    this.ProcessContact(data, contactPeople, isExistContactPeople, ref contact);

                    resultData = new HisContactResultSDO();
                    resultData.ContactInfo = contact;
                    resultData.ContactPoint = contactPeople;
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessContact(HisContactSDO data, HIS_CONTACT_POINT contactPeople, bool isExistContactPeople, ref HIS_CONTACT contact)
        {
            if (data != null && contactPeople != null)
            {
                bool isUpdate = false;

                //Neu du lieu doi tuong tiep xuc da co tren he thong thi kiem tra xem da co du lieu khai bao tiep xuc
                //giua nguoi benh va doi tuong tiep xuc do chua
                if (isExistContactPeople)
                {
                    HisContactFilterQuery filter = new HisContactFilterQuery();
                    filter.CONTACT_POINT1_ID__OR__CONTACT_POINT2_IDs = new List<long>(){ contactPeople.ID, data.ContactPointId };
                    List<HIS_CONTACT> exists = new HisContactGet().Get(filter);

                    contact = IsNotNullOrEmpty(exists) ? exists.OrderByDescending(o => o.ID).FirstOrDefault() : null;
                }

                isUpdate = contact != null;

                if (contact == null)
                {
                    contact = new HIS_CONTACT();
                }
                
                contact.CONTACT_TIME = data.ContactTime;
                contact.CONTACT_POINT1_ID = data.ContactPointId;
                contact.CONTACT_POINT2_ID = contactPeople.ID;
                contact.CONTACT_PLACE = data.ContactPlace;

                if (isUpdate)
                {
                    if (!this.hisContactUpdate.Update(contact))
                    {
                        throw new Exception("update his_contact that bai");
                    }
                }
                else
                {
                    if (!this.hisContactCreate.Create(contact))
                    {
                        throw new Exception("Create his_contact that bai");
                    }
                }
            }
        }

        private void Rollback()
        {
            try
            {
                this.hisContactCreate.RollbackData();
                this.hisContactUpdate.RollbackData();
                this.hisContactPointCreate.RollbackData();
                this.hisContactPointUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
