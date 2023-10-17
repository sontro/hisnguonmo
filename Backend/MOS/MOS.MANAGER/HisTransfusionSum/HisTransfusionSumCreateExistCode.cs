using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransfusionSum
{
    partial class HisTransfusionSumCreate : BusinessBase
    {
		private List<HIS_TRANSFUSION_SUM> recentHisTransfusionSums = new List<HIS_TRANSFUSION_SUM>();
		
        internal HisTransfusionSumCreate()
            : base()
        {

        }

        internal HisTransfusionSumCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_TRANSFUSION_SUM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTransfusionSumCheck checker = new HisTransfusionSumCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.TRANSFUSION_SUM_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisTransfusionSumDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransfusionSum_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisTransfusionSum that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisTransfusionSums.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisTransfusionSums))
            {
                if (!DAOWorker.HisTransfusionSumDAO.TruncateList(this.recentHisTransfusionSums))
                {
                    LogSystem.Warn("Rollback du lieu HisTransfusionSum that bai, can kiem tra lai." + LogUtil.TraceData("recentHisTransfusionSums", this.recentHisTransfusionSums));
                }
				this.recentHisTransfusionSums = null;
            }
        }
    }
}
