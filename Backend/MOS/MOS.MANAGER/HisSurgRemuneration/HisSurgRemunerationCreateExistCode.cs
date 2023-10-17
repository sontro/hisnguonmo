using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSurgRemuneration
{
    partial class HisSurgRemunerationCreate : BusinessBase
    {
		private List<HIS_SURG_REMUNERATION> recentHisSurgRemunerations = new List<HIS_SURG_REMUNERATION>();
		
        internal HisSurgRemunerationCreate()
            : base()
        {

        }

        internal HisSurgRemunerationCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SURG_REMUNERATION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSurgRemunerationCheck checker = new HisSurgRemunerationCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.SURG_REMUNERATION_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisSurgRemunerationDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSurgRemuneration_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSurgRemuneration that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisSurgRemunerations.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisSurgRemunerations))
            {
                if (!DAOWorker.HisSurgRemunerationDAO.TruncateList(this.recentHisSurgRemunerations))
                {
                    LogSystem.Warn("Rollback du lieu HisSurgRemuneration that bai, can kiem tra lai." + LogUtil.TraceData("recentHisSurgRemunerations", this.recentHisSurgRemunerations));
                }
				this.recentHisSurgRemunerations = null;
            }
        }
    }
}
