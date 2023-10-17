using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransfusion
{
    partial class HisTransfusionCreate : BusinessBase
    {
		private List<HIS_TRANSFUSION> recentHisTransfusions = new List<HIS_TRANSFUSION>();
		
        internal HisTransfusionCreate()
            : base()
        {

        }

        internal HisTransfusionCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_TRANSFUSION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTransfusionCheck checker = new HisTransfusionCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.TRANSFUSION_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisTransfusionDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransfusion_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisTransfusion that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisTransfusions.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisTransfusions))
            {
                if (!DAOWorker.HisTransfusionDAO.TruncateList(this.recentHisTransfusions))
                {
                    LogSystem.Warn("Rollback du lieu HisTransfusion that bai, can kiem tra lai." + LogUtil.TraceData("recentHisTransfusions", this.recentHisTransfusions));
                }
				this.recentHisTransfusions = null;
            }
        }
    }
}
