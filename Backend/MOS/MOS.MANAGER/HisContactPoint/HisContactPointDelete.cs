using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisContact;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisContactPoint
{
    partial class HisContactPointDelete : BusinessBase
    {
        internal HisContactPointDelete()
            : base()
        {

        }

        internal HisContactPointDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_CONTACT_POINT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisContactPointCheck checker = new HisContactPointCheck(param);
                valid = valid && IsNotNull(data);
                HIS_CONTACT_POINT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    HisContactFilterQuery filter = new HisContactFilterQuery();
                    filter.CONTACT_POINT1_ID__OR__CONTACT_POINT2_ID = data.ID;
                    List<HIS_CONTACT> contacts = new HisContactGet().Get(filter);

                    if (!IsNotNullOrEmpty(contacts) || new HisContactTruncate().TruncateList(contacts))
                    {
                        result = DAOWorker.HisContactPointDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_CONTACT_POINT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisContactPointCheck checker = new HisContactPointCheck(param);
                List<HIS_CONTACT_POINT> listRaw = new List<HIS_CONTACT_POINT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisContactPointDAO.DeleteList(listData);
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
    }
}
