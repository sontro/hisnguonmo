using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisUnlimitReason
{
    partial class HisUnlimitReasonCreate : BusinessBase
    {
		private List<HIS_UNLIMIT_REASON> recentHisUnlimitReasons = new List<HIS_UNLIMIT_REASON>();
		
        internal HisUnlimitReasonCreate()
            : base()
        {

        }

        internal HisUnlimitReasonCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_UNLIMIT_REASON data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisUnlimitReasonCheck checker = new HisUnlimitReasonCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisUnlimitReasonDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisUnlimitReason_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisUnlimitReason that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisUnlimitReasons.Add(data);
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
		
		internal bool CreateList(List<HIS_UNLIMIT_REASON> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisUnlimitReasonCheck checker = new HisUnlimitReasonCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisUnlimitReasonDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisUnlimitReason_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisUnlimitReason that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisUnlimitReasons.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisUnlimitReasons))
            {
                if (!DAOWorker.HisUnlimitReasonDAO.TruncateList(this.recentHisUnlimitReasons))
                {
                    LogSystem.Warn("Rollback du lieu HisUnlimitReason that bai, can kiem tra lai." + LogUtil.TraceData("recentHisUnlimitReasons", this.recentHisUnlimitReasons));
                }
				this.recentHisUnlimitReasons = null;
            }
        }
    }
}
