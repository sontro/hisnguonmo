using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntigen
{
    partial class HisAntigenCreate : BusinessBase
    {
		private List<HIS_ANTIGEN> recentHisAntigens = new List<HIS_ANTIGEN>();
		
        internal HisAntigenCreate()
            : base()
        {

        }

        internal HisAntigenCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ANTIGEN data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAntigenCheck checker = new HisAntigenCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.ANTIGEN_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisAntigenDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAntigen_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAntigen that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisAntigens.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisAntigens))
            {
                if (!DAOWorker.HisAntigenDAO.TruncateList(this.recentHisAntigens))
                {
                    LogSystem.Warn("Rollback du lieu HisAntigen that bai, can kiem tra lai." + LogUtil.TraceData("recentHisAntigens", this.recentHisAntigens));
                }
				this.recentHisAntigens = null;
            }
        }
    }
}
