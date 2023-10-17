using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAntibioticOldReg
{
    partial class HisAntibioticOldRegUpdate : BusinessBase
    {
		private List<HIS_ANTIBIOTIC_OLD_REG> beforeUpdateHisAntibioticOldRegs = new List<HIS_ANTIBIOTIC_OLD_REG>();
		
        internal HisAntibioticOldRegUpdate()
            : base()
        {

        }

        internal HisAntibioticOldRegUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_ANTIBIOTIC_OLD_REG data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAntibioticOldRegCheck checker = new HisAntibioticOldRegCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_ANTIBIOTIC_OLD_REG raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.ANTIBIOTIC_OLD_REG_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisAntibioticOldRegDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAntibioticOldReg_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAntibioticOldReg that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisAntibioticOldRegs.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_ANTIBIOTIC_OLD_REG> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAntibioticOldRegCheck checker = new HisAntibioticOldRegCheck(param);
                List<HIS_ANTIBIOTIC_OLD_REG> listRaw = new List<HIS_ANTIBIOTIC_OLD_REG>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.ANTIBIOTIC_OLD_REG_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisAntibioticOldRegDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAntibioticOldReg_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAntibioticOldReg that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisAntibioticOldRegs.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisAntibioticOldRegs))
            {
                if (!DAOWorker.HisAntibioticOldRegDAO.UpdateList(this.beforeUpdateHisAntibioticOldRegs))
                {
                    LogSystem.Warn("Rollback du lieu HisAntibioticOldReg that bai, can kiem tra lai." + LogUtil.TraceData("HisAntibioticOldRegs", this.beforeUpdateHisAntibioticOldRegs));
                }
				this.beforeUpdateHisAntibioticOldRegs = null;
            }
        }
    }
}