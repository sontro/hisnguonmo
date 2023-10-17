using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisProcessingMethod
{
    partial class HisProcessingMethodCreate : BusinessBase
    {
		private List<HIS_PROCESSING_METHOD> recentHisProcessingMethods = new List<HIS_PROCESSING_METHOD>();
		
        internal HisProcessingMethodCreate()
            : base()
        {

        }

        internal HisProcessingMethodCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_PROCESSING_METHOD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisProcessingMethodCheck checker = new HisProcessingMethodCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.PROCESSING_METHOD_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisProcessingMethodDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisProcessingMethod_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisProcessingMethod that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisProcessingMethods.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisProcessingMethods))
            {
                if (!DAOWorker.HisProcessingMethodDAO.TruncateList(this.recentHisProcessingMethods))
                {
                    LogSystem.Warn("Rollback du lieu HisProcessingMethod that bai, can kiem tra lai." + LogUtil.TraceData("recentHisProcessingMethods", this.recentHisProcessingMethods));
                }
				this.recentHisProcessingMethods = null;
            }
        }
    }
}
