using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicalContract
{
    partial class HisMedicalContractCreate : BusinessBase
    {
		private List<HIS_MEDICAL_CONTRACT> recentHisMedicalContracts = new List<HIS_MEDICAL_CONTRACT>();
		
        internal HisMedicalContractCreate()
            : base()
        {

        }

        internal HisMedicalContractCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEDICAL_CONTRACT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicalContractCheck checker = new HisMedicalContractCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.MEDICAL_CONTRACT_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisMedicalContractDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicalContract_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMedicalContract that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMedicalContracts.Add(data);
                    result = true;
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
		
		internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.recentHisMedicalContracts))
            {
                if (!DAOWorker.HisMedicalContractDAO.TruncateList(this.recentHisMedicalContracts))
                {
                    LogSystem.Warn("Rollback du lieu HisMedicalContract that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMedicalContracts", this.recentHisMedicalContracts));
                }
				this.recentHisMedicalContracts = null;
            }
        }
    }
}
