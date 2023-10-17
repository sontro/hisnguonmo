using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisIcdCm
{
    partial class HisIcdCmCreate : BusinessBase
    {
		private List<HIS_ICD_CM> recentHisIcdCms = new List<HIS_ICD_CM>();
		
        internal HisIcdCmCreate()
            : base()
        {

        }

        internal HisIcdCmCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ICD_CM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisIcdCmCheck checker = new HisIcdCmCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.ICD_CM_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisIcdCmDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisIcdCm_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisIcdCm that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisIcdCms.Add(data);
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

        internal bool CreateList(List<HIS_ICD_CM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisIcdCmCheck checker = new HisIcdCmCheck(param);
                foreach (HIS_ICD_CM data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.ICD_CM_CODE, null);
                }
                if (valid)
                {
                    if (!DAOWorker.HisIcdCmDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisIcdCm_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisIcdCm that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisIcdCms.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisIcdCms))
            {
                if (!new HisIcdCmTruncate(param).TruncateList(this.recentHisIcdCms))
                {
                    LogSystem.Warn("Rollback du lieu HisIcdCm that bai, can kiem tra lai." + LogUtil.TraceData("recentHisIcdCms", this.recentHisIcdCms));
                }
            }
        }
    }
}
