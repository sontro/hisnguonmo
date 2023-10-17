using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMedicalContract
{
    partial class HisMedicalContractUpdate : BusinessBase
    {
		private List<HIS_MEDICAL_CONTRACT> beforeUpdateHisMedicalContracts = new List<HIS_MEDICAL_CONTRACT>();
		
        internal HisMedicalContractUpdate()
            : base()
        {

        }

        internal HisMedicalContractUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEDICAL_CONTRACT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicalContractCheck checker = new HisMedicalContractCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MEDICAL_CONTRACT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.MEDICAL_CONTRACT_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisMedicalContractDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicalContract_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMedicalContract that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisMedicalContracts.Add(raw);
                    
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

        internal bool Update(HIS_MEDICAL_CONTRACT data,HIS_MEDICAL_CONTRACT before)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicalContractCheck checker = new HisMedicalContractCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsUnLock(before);
                valid = valid && checker.ExistsCode(data.MEDICAL_CONTRACT_CODE, data.ID);
                if (valid)
                {
                    if (!DAOWorker.HisMedicalContractDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicalContract_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMedicalContract that bai." + LogUtil.TraceData("data", data));
                    }

                    this.beforeUpdateHisMedicalContracts.Add(before);

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

        internal bool UpdateList(List<HIS_MEDICAL_CONTRACT> listData)
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
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.MEDICAL_CONTRACT_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisMedicalContractDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicalContract_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMedicalContract that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisMedicalContracts.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMedicalContracts))
            {
                if (!DAOWorker.HisMedicalContractDAO.UpdateList(this.beforeUpdateHisMedicalContracts))
                {
                    LogSystem.Warn("Rollback du lieu HisMedicalContract that bai, can kiem tra lai." + LogUtil.TraceData("HisMedicalContracts", this.beforeUpdateHisMedicalContracts));
                }
				this.beforeUpdateHisMedicalContracts = null;
            }
        }
    }
}
