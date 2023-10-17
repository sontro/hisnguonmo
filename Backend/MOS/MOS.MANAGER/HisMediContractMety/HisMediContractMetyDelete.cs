using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMediContractMety
{
    partial class HisMediContractMetyDelete : BusinessBase
    {
        internal HisMediContractMetyDelete()
            : base()
        {

        }

        internal HisMediContractMetyDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_MEDI_CONTRACT_METY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediContractMetyCheck checker = new HisMediContractMetyCheck(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_CONTRACT_METY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisMediContractMetyDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_MEDI_CONTRACT_METY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMediContractMetyCheck checker = new HisMediContractMetyCheck(param);
                List<HIS_MEDI_CONTRACT_METY> listRaw = new List<HIS_MEDI_CONTRACT_METY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisMediContractMetyDAO.DeleteList(listData);
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
