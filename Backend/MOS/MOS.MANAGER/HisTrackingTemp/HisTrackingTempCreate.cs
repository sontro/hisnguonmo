using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTrackingTemp
{
    partial class HisTrackingTempCreate : BusinessBase
    {
		private List<HIS_TRACKING_TEMP> recentHisTrackingTemps = new List<HIS_TRACKING_TEMP>();
		
        internal HisTrackingTempCreate()
            : base()
        {

        }

        internal HisTrackingTempCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_TRACKING_TEMP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTrackingTempCheck checker = new HisTrackingTempCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.TRACKING_TEMP_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisTrackingTempDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTrackingTemp_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisTrackingTemp that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisTrackingTemps.Add(data);
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
		
		internal bool CreateList(List<HIS_TRACKING_TEMP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTrackingTempCheck checker = new HisTrackingTempCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.TRACKING_TEMP_CODE, null);
                }
                if (valid)
                {
                    if (!DAOWorker.HisTrackingTempDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTrackingTemp_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisTrackingTemp that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisTrackingTemps.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisTrackingTemps))
            {
                if (!DAOWorker.HisTrackingTempDAO.TruncateList(this.recentHisTrackingTemps))
                {
                    LogSystem.Warn("Rollback du lieu HisTrackingTemp that bai, can kiem tra lai." + LogUtil.TraceData("recentHisTrackingTemps", this.recentHisTrackingTemps));
                }
				this.recentHisTrackingTemps = null;
            }
        }
    }
}
