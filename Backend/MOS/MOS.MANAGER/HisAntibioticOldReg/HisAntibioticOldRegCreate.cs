using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntibioticOldReg
{
    partial class HisAntibioticOldRegCreate : BusinessBase
    {
		private List<HIS_ANTIBIOTIC_OLD_REG> recentHisAntibioticOldRegs = new List<HIS_ANTIBIOTIC_OLD_REG>();
		
        internal HisAntibioticOldRegCreate()
            : base()
        {

        }

        internal HisAntibioticOldRegCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ANTIBIOTIC_OLD_REG data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAntibioticOldRegCheck checker = new HisAntibioticOldRegCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisAntibioticOldRegDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAntibioticOldReg_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAntibioticOldReg that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisAntibioticOldRegs.Add(data);
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
		
		internal bool CreateList(List<HIS_ANTIBIOTIC_OLD_REG> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAntibioticOldRegCheck checker = new HisAntibioticOldRegCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisAntibioticOldRegDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAntibioticOldReg_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAntibioticOldReg that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisAntibioticOldRegs.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisAntibioticOldRegs))
            {
                if (!DAOWorker.HisAntibioticOldRegDAO.TruncateList(this.recentHisAntibioticOldRegs))
                {
                    LogSystem.Warn("Rollback du lieu HisAntibioticOldReg that bai, can kiem tra lai." + LogUtil.TraceData("recentHisAntibioticOldRegs", this.recentHisAntibioticOldRegs));
                }
				this.recentHisAntibioticOldRegs = null;
            }
        }
    }
}
