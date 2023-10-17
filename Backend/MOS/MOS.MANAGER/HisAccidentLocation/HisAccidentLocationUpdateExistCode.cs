using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAccidentLocation
{
    partial class HisAccidentLocationUpdate : BusinessBase
    {
		private List<HIS_ACCIDENT_LOCATION> beforeUpdateHisAccidentLocations = new List<HIS_ACCIDENT_LOCATION>();
		
        internal HisAccidentLocationUpdate()
            : base()
        {

        }

        internal HisAccidentLocationUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_ACCIDENT_LOCATION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAccidentLocationCheck checker = new HisAccidentLocationCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_ACCIDENT_LOCATION raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.ACCIDENT_LOCATION_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisAccidentLocations.Add(raw);
					if (!DAOWorker.HisAccidentLocationDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAccidentLocation_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAccidentLocation that bai." + LogUtil.TraceData("data", data));
                    }
                    
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

        internal bool UpdateList(List<HIS_ACCIDENT_LOCATION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAccidentLocationCheck checker = new HisAccidentLocationCheck(param);
                List<HIS_ACCIDENT_LOCATION> listRaw = new List<HIS_ACCIDENT_LOCATION>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.ACCIDENT_LOCATION_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisAccidentLocations.AddRange(listRaw);
					if (!DAOWorker.HisAccidentLocationDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAccidentLocation_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAccidentLocation that bai." + LogUtil.TraceData("listData", listData));
                    }
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisAccidentLocations))
            {
                if (!new HisAccidentLocationUpdate(param).UpdateList(this.beforeUpdateHisAccidentLocations))
                {
                    LogSystem.Warn("Rollback du lieu HisAccidentLocation that bai, can kiem tra lai." + LogUtil.TraceData("HisAccidentLocations", this.beforeUpdateHisAccidentLocations));
                }
            }
        }
    }
}
