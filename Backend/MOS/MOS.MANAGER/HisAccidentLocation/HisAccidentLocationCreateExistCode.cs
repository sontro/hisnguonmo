using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAccidentLocation
{
    partial class HisAccidentLocationCreate : BusinessBase
    {
		private List<HIS_ACCIDENT_LOCATION> recentHisAccidentLocations = new List<HIS_ACCIDENT_LOCATION>();
		
        internal HisAccidentLocationCreate()
            : base()
        {

        }

        internal HisAccidentLocationCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ACCIDENT_LOCATION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAccidentLocationCheck checker = new HisAccidentLocationCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.ACCIDENT_LOCATION_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisAccidentLocationDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAccidentLocation_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAccidentLocation that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisAccidentLocations.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisAccidentLocations))
            {
                if (!new HisAccidentLocationTruncate(param).TruncateList(this.recentHisAccidentLocations))
                {
                    LogSystem.Warn("Rollback du lieu HisAccidentLocation that bai, can kiem tra lai." + LogUtil.TraceData("recentHisAccidentLocations", this.recentHisAccidentLocations));
                }
            }
        }
    }
}
