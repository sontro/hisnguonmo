using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPaanLiquid
{
    partial class HisPaanLiquidCreate : BusinessBase
    {
		private List<HIS_PAAN_LIQUID> recentHisPaanLiquids = new List<HIS_PAAN_LIQUID>();
		
        internal HisPaanLiquidCreate()
            : base()
        {

        }

        internal HisPaanLiquidCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_PAAN_LIQUID data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPaanLiquidCheck checker = new HisPaanLiquidCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.PAAN_LIQUID_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisPaanLiquidDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPaanLiquid_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPaanLiquid that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisPaanLiquids.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisPaanLiquids))
            {
                if (!new HisPaanLiquidTruncate(param).TruncateList(this.recentHisPaanLiquids))
                {
                    LogSystem.Warn("Rollback du lieu HisPaanLiquid that bai, can kiem tra lai." + LogUtil.TraceData("recentHisPaanLiquids", this.recentHisPaanLiquids));
                }
            }
        }
    }
}
