using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEkip
{
    partial class HisEkipCreate : BusinessBase
    {
		private List<HIS_EKIP> recentHisEkips = new List<HIS_EKIP>();
		
        internal HisEkipCreate()
            : base()
        {

        }

        internal HisEkipCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EKIP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEkipCheck checker = new HisEkipCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.EKIP_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisEkipDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEkip_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisEkip that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisEkips.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisEkips))
            {
                if (!new HisEkipTruncate(param).TruncateList(this.recentHisEkips))
                {
                    LogSystem.Warn("Rollback du lieu HisEkip that bai, can kiem tra lai." + LogUtil.TraceData("recentHisEkips", this.recentHisEkips));
                }
            }
        }
    }
}
