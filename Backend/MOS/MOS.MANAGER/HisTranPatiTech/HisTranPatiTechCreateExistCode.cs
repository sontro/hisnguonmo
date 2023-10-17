using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTranPatiTech
{
    partial class HisTranPatiTechCreate : BusinessBase
    {
		private List<HIS_TRAN_PATI_TECH> recentHisTranPatiTechs = new List<HIS_TRAN_PATI_TECH>();
		
        internal HisTranPatiTechCreate()
            : base()
        {

        }

        internal HisTranPatiTechCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_TRAN_PATI_TECH data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTranPatiTechCheck checker = new HisTranPatiTechCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.TRAN_PATI_TECH_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisTranPatiTechDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTranPatiTech_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisTranPatiTech that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisTranPatiTechs.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisTranPatiTechs))
            {
                if (!DAOWorker.HisTranPatiTechDAO.TruncateList(this.recentHisTranPatiTechs))
                {
                    LogSystem.Warn("Rollback du lieu HisTranPatiTech that bai, can kiem tra lai." + LogUtil.TraceData("recentHisTranPatiTechs", this.recentHisTranPatiTechs));
                }
				this.recentHisTranPatiTechs = null;
            }
        }
    }
}
