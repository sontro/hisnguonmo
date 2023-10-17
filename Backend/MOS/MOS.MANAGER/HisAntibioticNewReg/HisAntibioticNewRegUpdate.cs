using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAntibioticNewReg
{
    partial class HisAntibioticNewRegUpdate : BusinessBase
    {
		private List<HIS_ANTIBIOTIC_NEW_REG> beforeUpdateHisAntibioticNewRegs = new List<HIS_ANTIBIOTIC_NEW_REG>();
		
        internal HisAntibioticNewRegUpdate()
            : base()
        {

        }

        internal HisAntibioticNewRegUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_ANTIBIOTIC_NEW_REG data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAntibioticNewRegCheck checker = new HisAntibioticNewRegCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_ANTIBIOTIC_NEW_REG raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisAntibioticNewRegDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAntibioticNewReg_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAntibioticNewReg that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisAntibioticNewRegs.Add(raw);
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

        internal bool UpdateList(List<HIS_ANTIBIOTIC_NEW_REG> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAntibioticNewRegCheck checker = new HisAntibioticNewRegCheck(param);
                List<HIS_ANTIBIOTIC_NEW_REG> listRaw = new List<HIS_ANTIBIOTIC_NEW_REG>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisAntibioticNewRegDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAntibioticNewReg_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAntibioticNewReg that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisAntibioticNewRegs.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisAntibioticNewRegs))
            {
                if (!DAOWorker.HisAntibioticNewRegDAO.UpdateList(this.beforeUpdateHisAntibioticNewRegs))
                {
                    LogSystem.Warn("Rollback du lieu HisAntibioticNewReg that bai, can kiem tra lai." + LogUtil.TraceData("HisAntibioticNewRegs", this.beforeUpdateHisAntibioticNewRegs));
                }
				this.beforeUpdateHisAntibioticNewRegs = null;
            }
        }
    }
}
