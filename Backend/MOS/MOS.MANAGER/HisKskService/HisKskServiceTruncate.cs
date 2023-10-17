using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisKskService
{
    partial class HisKskServiceTruncate : BusinessBase
    {
        internal HisKskServiceTruncate()
            : base()
        {

        }

        internal HisKskServiceTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskServiceCheck checker = new HisKskServiceCheck(param);
                HIS_KSK_SERVICE raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(id);
                if (valid)
                {
                    result = DAOWorker.HisKskServiceDAO.Truncate(raw);
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

        internal bool TruncateList(List<long> ids)
        {
            bool result = false;
            try
            {
                bool valid = true;
                List<HIS_KSK_SERVICE> listRaw = new List<HIS_KSK_SERVICE>();
                valid = IsNotNullOrEmpty(ids);
                HisKskServiceCheck checker = new HisKskServiceCheck(param);
                valid = valid && checker.VerifyIds(ids, listRaw);
                foreach (var data in listRaw)
                {
                    valid = valid && checker.IsUnLock(data);
                    valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisKskServiceDAO.TruncateList(listRaw);
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
