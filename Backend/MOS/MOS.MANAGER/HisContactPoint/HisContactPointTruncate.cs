using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisContact;
using MOS.MANAGER.HisRestRetrType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisContactPoint
{
    partial class HisContactPointTruncate : BusinessBase
    {
        internal HisContactPointTruncate()
            : base()
        {

        }

        internal HisContactPointTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisContactPointCheck checker = new HisContactPointCheck(param);
                HIS_CONTACT_POINT raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(id);
                if (valid)
                {
                    HisContactFilterQuery filter = new HisContactFilterQuery();
                    filter.CONTACT_POINT1_ID__OR__CONTACT_POINT2_ID = id;
                    List<HIS_CONTACT> contacts = new HisContactGet().Get(filter);

                    if (!IsNotNullOrEmpty(contacts) || new HisContactTruncate().TruncateList(contacts))
                    {
                        result = DAOWorker.HisContactPointDAO.Truncate(raw);
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

        internal bool TruncateList(List<HIS_CONTACT_POINT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisContactPointCheck checker = new HisContactPointCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
					valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisContactPointDAO.TruncateList(listData);
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
