using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntibioticNewReg
{
    partial class HisAntibioticNewRegCreate : BusinessBase
    {
		private List<HIS_ANTIBIOTIC_NEW_REG> recentHisAntibioticNewRegs = new List<HIS_ANTIBIOTIC_NEW_REG>();
		
        internal HisAntibioticNewRegCreate()
            : base()
        {

        }

        internal HisAntibioticNewRegCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ANTIBIOTIC_NEW_REG data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAntibioticNewRegCheck checker = new HisAntibioticNewRegCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.ANTIBIOTIC_NEW_REG_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisAntibioticNewRegDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAntibioticNewReg_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAntibioticNewReg that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisAntibioticNewRegs.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisAntibioticNewRegs))
            {
                if (!DAOWorker.HisAntibioticNewRegDAO.TruncateList(this.recentHisAntibioticNewRegs))
                {
                    LogSystem.Warn("Rollback du lieu HisAntibioticNewReg that bai, can kiem tra lai." + LogUtil.TraceData("recentHisAntibioticNewRegs", this.recentHisAntibioticNewRegs));
                }
				this.recentHisAntibioticNewRegs = null;
            }
        }
    }
}
