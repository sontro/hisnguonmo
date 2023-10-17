using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMedicalContract
{
    partial class HisMedicalContractDelete : BusinessBase
    {
        internal HisMedicalContractDelete()
            : base()
        {

        }

        internal HisMedicalContractDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_MEDICAL_CONTRACT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicalContractCheck checker = new HisMedicalContractCheck(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICAL_CONTRACT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisMedicalContractDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_MEDICAL_CONTRACT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicalContractCheck checker = new HisMedicalContractCheck(param);
                List<HIS_MEDICAL_CONTRACT> listRaw = new List<HIS_MEDICAL_CONTRACT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisMedicalContractDAO.DeleteList(listData);
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
