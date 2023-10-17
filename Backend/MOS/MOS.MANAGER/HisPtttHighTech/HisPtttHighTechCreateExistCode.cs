using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPtttHighTech
{
    partial class HisPtttHighTechCreate : BusinessBase
    {
		private List<HIS_PTTT_HIGH_TECH> recentHisPtttHighTechs = new List<HIS_PTTT_HIGH_TECH>();
		
        internal HisPtttHighTechCreate()
            : base()
        {

        }

        internal HisPtttHighTechCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_PTTT_HIGH_TECH data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPtttHighTechCheck checker = new HisPtttHighTechCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.PTTT_HIGH_TECH_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisPtttHighTechDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPtttHighTech_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPtttHighTech that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisPtttHighTechs.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisPtttHighTechs))
            {
                if (!DAOWorker.HisPtttHighTechDAO.TruncateList(this.recentHisPtttHighTechs))
                {
                    LogSystem.Warn("Rollback du lieu HisPtttHighTech that bai, can kiem tra lai." + LogUtil.TraceData("recentHisPtttHighTechs", this.recentHisPtttHighTechs));
                }
				this.recentHisPtttHighTechs = null;
            }
        }
    }
}
