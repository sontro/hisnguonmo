using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAccidentVehicle
{
    partial class HisAccidentVehicleDelete : BusinessBase
    {
        internal HisAccidentVehicleDelete()
            : base()
        {

        }

        internal HisAccidentVehicleDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_ACCIDENT_VEHICLE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAccidentVehicleCheck checker = new HisAccidentVehicleCheck(param);
                valid = valid && IsNotNull(data);
                HIS_ACCIDENT_VEHICLE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisAccidentVehicleDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_ACCIDENT_VEHICLE> listData)
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
                if (valid)
                {
                    result = DAOWorker.HisAccidentVehicleDAO.DeleteList(listData);
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
    }
}
