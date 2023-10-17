using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRemuneration
{
    partial class HisRemunerationCreate : BusinessBase
    {
		private List<HIS_REMUNERATION> recentHisRemunerations = new List<HIS_REMUNERATION>();
		
        internal HisRemunerationCreate()
            : base()
        {

        }

        internal HisRemunerationCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_REMUNERATION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRemunerationCheck checker = new HisRemunerationCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.REMUNERATION_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisRemunerationDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRemuneration_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisRemuneration that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisRemunerations.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisRemunerations))
            {
                if (!new HisRemunerationTruncate(param).TruncateList(this.recentHisRemunerations))
                {
                    LogSystem.Warn("Rollback du lieu HisRemuneration that bai, can kiem tra lai." + LogUtil.TraceData("recentHisRemunerations", this.recentHisRemunerations));
                }
            }
        }
    }
}
