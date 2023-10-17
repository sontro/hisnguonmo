using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAntigenMety
{
    partial class HisAntigenMetyDelete : BusinessBase
    {
        internal HisAntigenMetyDelete()
            : base()
        {

        }

        internal HisAntigenMetyDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_ANTIGEN_METY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAntigenMetyCheck checker = new HisAntigenMetyCheck(param);
                valid = valid && IsNotNull(data);
                HIS_ANTIGEN_METY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisAntigenMetyDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_ANTIGEN_METY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAntigenMetyCheck checker = new HisAntigenMetyCheck(param);
                List<HIS_ANTIGEN_METY> listRaw = new List<HIS_ANTIGEN_METY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisAntigenMetyDAO.DeleteList(listData);
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
