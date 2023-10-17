using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAccidentVehicle
{
    partial class HisAccidentVehicleCreate : BusinessBase
    {
		private List<HIS_ACCIDENT_VEHICLE> recentHisAccidentVehicles = new List<HIS_ACCIDENT_VEHICLE>();
		
        internal HisAccidentVehicleCreate()
            : base()
        {

        }

        internal HisAccidentVehicleCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ACCIDENT_VEHICLE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAccidentVehicleCheck checker = new HisAccidentVehicleCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.ACCIDENT_VEHICLE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisAccidentVehicleDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAccidentVehicle_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAccidentVehicle that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisAccidentVehicles.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisAccidentVehicles))
            {
                if (!new HisAccidentVehicleTruncate(param).TruncateList(this.recentHisAccidentVehicles))
                {
                    LogSystem.Warn("Rollback du lieu HisAccidentVehicle that bai, can kiem tra lai." + LogUtil.TraceData("recentHisAccidentVehicles", this.recentHisAccidentVehicles));
                }
            }
        }
    }
}
