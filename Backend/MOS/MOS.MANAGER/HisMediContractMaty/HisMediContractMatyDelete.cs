using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMediContractMaty
{
    partial class HisMediContractMatyDelete : BusinessBase
    {
        internal HisMediContractMatyDelete()
            : base()
        {

        }

        internal HisMediContractMatyDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_MEDI_CONTRACT_MATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediContractMatyCheck checker = new HisMediContractMatyCheck(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_CONTRACT_MATY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisMediContractMatyDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_MEDI_CONTRACT_MATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMediContractMatyCheck checker = new HisMediContractMatyCheck(param);
                List<HIS_MEDI_CONTRACT_MATY> listRaw = new List<HIS_MEDI_CONTRACT_MATY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisMediContractMatyDAO.DeleteList(listData);
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
