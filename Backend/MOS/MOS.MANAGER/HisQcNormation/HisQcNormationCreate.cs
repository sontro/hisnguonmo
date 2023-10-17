using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisQcNormation
{
    partial class HisQcNormationCreate : BusinessBase
    {
		private List<HIS_QC_NORMATION> recentHisQcNormations = new List<HIS_QC_NORMATION>();
		
        internal HisQcNormationCreate()
            : base()
        {

        }

        internal HisQcNormationCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_QC_NORMATION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisQcNormationCheck checker = new HisQcNormationCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisQcNormationDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisQcNormation_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisQcNormation that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisQcNormations.Add(data);
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
		
		internal bool CreateList(List<HIS_QC_NORMATION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisQcNormationCheck checker = new HisQcNormationCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisQcNormationDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisQcNormation_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisQcNormation that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisQcNormations.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisQcNormations))
            {
                if (!DAOWorker.HisQcNormationDAO.TruncateList(this.recentHisQcNormations))
                {
                    LogSystem.Warn("Rollback du lieu HisQcNormation that bai, can kiem tra lai." + LogUtil.TraceData("recentHisQcNormations", this.recentHisQcNormations));
                }
				this.recentHisQcNormations = null;
            }
        }
    }
}
