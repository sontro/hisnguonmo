using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSpeedUnit
{
    partial class HisSpeedUnitCreate : BusinessBase
    {
		private List<HIS_SPEED_UNIT> recentHisSpeedUnits = new List<HIS_SPEED_UNIT>();
		
        internal HisSpeedUnitCreate()
            : base()
        {

        }

        internal HisSpeedUnitCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SPEED_UNIT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSpeedUnitCheck checker = new HisSpeedUnitCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisSpeedUnitDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSpeedUnit_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSpeedUnit that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisSpeedUnits.Add(data);
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
		
		internal bool CreateList(List<HIS_SPEED_UNIT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSpeedUnitCheck checker = new HisSpeedUnitCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisSpeedUnitDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSpeedUnit_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSpeedUnit that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisSpeedUnits.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisSpeedUnits))
            {
                if (!DAOWorker.HisSpeedUnitDAO.TruncateList(this.recentHisSpeedUnits))
                {
                    LogSystem.Warn("Rollback du lieu HisSpeedUnit that bai, can kiem tra lai." + LogUtil.TraceData("recentHisSpeedUnits", this.recentHisSpeedUnits));
                }
				this.recentHisSpeedUnits = null;
            }
        }
    }
}
