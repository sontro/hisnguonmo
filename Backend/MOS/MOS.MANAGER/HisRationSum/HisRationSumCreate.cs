using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRationSum
{
    partial class HisRationSumCreate : BusinessBase
    {
		private List<HIS_RATION_SUM> recentHisRationSums = new List<HIS_RATION_SUM>();
		
        internal HisRationSumCreate()
            : base()
        {

        }

        internal HisRationSumCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_RATION_SUM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRationSumCheck checker = new HisRationSumCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisRationSumDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRationSum_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisRationSum that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisRationSums.Add(data);
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
		
		internal bool CreateList(List<HIS_RATION_SUM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRationSumCheck checker = new HisRationSumCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisRationSumDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRationSum_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisRationSum that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisRationSums.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisRationSums))
            {
                if (!DAOWorker.HisRationSumDAO.TruncateList(this.recentHisRationSums))
                {
                    LogSystem.Warn("Rollback du lieu HisRationSum that bai, can kiem tra lai." + LogUtil.TraceData("recentHisRationSums", this.recentHisRationSums));
                }
				this.recentHisRationSums = null;
            }
        }
    }
}
