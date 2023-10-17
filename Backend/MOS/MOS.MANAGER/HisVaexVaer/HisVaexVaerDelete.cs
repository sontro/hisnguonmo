using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisVaexVaer
{
    partial class HisVaexVaerDelete : BusinessBase
    {
        internal HisVaexVaerDelete()
            : base()
        {

        }

        internal HisVaexVaerDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_VAEX_VAER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaexVaerCheck checker = new HisVaexVaerCheck(param);
                valid = valid && IsNotNull(data);
                HIS_VAEX_VAER raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisVaexVaerDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_VAEX_VAER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVaexVaerCheck checker = new HisVaexVaerCheck(param);
                List<HIS_VAEX_VAER> listRaw = new List<HIS_VAEX_VAER>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisVaexVaerDAO.DeleteList(listData);
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
