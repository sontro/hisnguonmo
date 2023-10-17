using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisKskDriverCar
{
    partial class HisKskDriverCarUpdate : BusinessBase
    {
		private List<HIS_KSK_DRIVER_CAR> beforeUpdateHisKskDriverCars = new List<HIS_KSK_DRIVER_CAR>();
		
        internal HisKskDriverCarUpdate()
            : base()
        {

        }

        internal HisKskDriverCarUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_KSK_DRIVER_CAR data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskDriverCarCheck checker = new HisKskDriverCarCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_KSK_DRIVER_CAR raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisKskDriverCarDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskDriverCar_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisKskDriverCar that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisKskDriverCars.Add(raw);
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

        internal bool UpdateList(List<HIS_KSK_DRIVER_CAR> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisKskDriverCarCheck checker = new HisKskDriverCarCheck(param);
                List<HIS_KSK_DRIVER_CAR> listRaw = new List<HIS_KSK_DRIVER_CAR>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisKskDriverCarDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskDriverCar_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisKskDriverCar that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisKskDriverCars.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisKskDriverCars))
            {
                if (!DAOWorker.HisKskDriverCarDAO.UpdateList(this.beforeUpdateHisKskDriverCars))
                {
                    LogSystem.Warn("Rollback du lieu HisKskDriverCar that bai, can kiem tra lai." + LogUtil.TraceData("HisKskDriverCars", this.beforeUpdateHisKskDriverCars));
                }
				this.beforeUpdateHisKskDriverCars = null;
            }
        }
    }
}
