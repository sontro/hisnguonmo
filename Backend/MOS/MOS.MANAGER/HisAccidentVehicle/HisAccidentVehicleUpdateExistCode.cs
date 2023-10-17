using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAccidentVehicle
{
    partial class HisAccidentVehicleUpdate : BusinessBase
    {
		private List<HIS_ACCIDENT_VEHICLE> beforeUpdateHisAccidentVehicles = new List<HIS_ACCIDENT_VEHICLE>();
		
        internal HisAccidentVehicleUpdate()
            : base()
        {

        }

        internal HisAccidentVehicleUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_ACCIDENT_VEHICLE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAccidentVehicleCheck checker = new HisAccidentVehicleCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_ACCIDENT_VEHICLE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.ACCIDENT_VEHICLE_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisAccidentVehicles.Add(raw);
					if (!DAOWorker.HisAccidentVehicleDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAccidentVehicle_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAccidentVehicle that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_ACCIDENT_VEHICLE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAccidentVehicleCheck checker = new HisAccidentVehicleCheck(param);
                List<HIS_ACCIDENT_VEHICLE> listRaw = new List<HIS_ACCIDENT_VEHICLE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.ACCIDENT_VEHICLE_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisAccidentVehicles.AddRange(listRaw);
					if (!DAOWorker.HisAccidentVehicleDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAccidentVehicle_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAccidentVehicle that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisAccidentVehicles))
            {
                if (!new HisAccidentVehicleUpdate(param).UpdateList(this.beforeUpdateHisAccidentVehicles))
                {
                    LogSystem.Warn("Rollback du lieu HisAccidentVehicle that bai, can kiem tra lai." + LogUtil.TraceData("HisAccidentVehicles", this.beforeUpdateHisAccidentVehicles));
                }
            }
        }
    }
}
