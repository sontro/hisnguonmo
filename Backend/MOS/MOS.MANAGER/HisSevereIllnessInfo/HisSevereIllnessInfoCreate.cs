using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSevereIllnessInfo
{
    partial class HisSevereIllnessInfoCreate : BusinessBase
    {
		private List<HIS_SEVERE_ILLNESS_INFO> recentHisSevereIllnessInfos = new List<HIS_SEVERE_ILLNESS_INFO>();
		
        internal HisSevereIllnessInfoCreate()
            : base()
        {

        }

        internal HisSevereIllnessInfoCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SEVERE_ILLNESS_INFO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSevereIllnessInfoCheck checker = new HisSevereIllnessInfoCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisSevereIllnessInfoDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSevereIllnessInfo_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSevereIllnessInfo that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisSevereIllnessInfos.Add(data);
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
		
		internal bool CreateList(List<HIS_SEVERE_ILLNESS_INFO> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSevereIllnessInfoCheck checker = new HisSevereIllnessInfoCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisSevereIllnessInfoDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSevereIllnessInfo_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSevereIllnessInfo that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisSevereIllnessInfos.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisSevereIllnessInfos))
            {
                if (!DAOWorker.HisSevereIllnessInfoDAO.TruncateList(this.recentHisSevereIllnessInfos))
                {
                    LogSystem.Warn("Rollback du lieu HisSevereIllnessInfo that bai, can kiem tra lai." + LogUtil.TraceData("recentHisSevereIllnessInfos", this.recentHisSevereIllnessInfos));
                }
				this.recentHisSevereIllnessInfos = null;
            }
        }
    }
}
