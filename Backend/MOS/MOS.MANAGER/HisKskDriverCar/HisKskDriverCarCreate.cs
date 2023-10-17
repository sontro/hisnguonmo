using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskDriverCar
{
    partial class HisKskDriverCarCreate : BusinessBase
    {
		private List<HIS_KSK_DRIVER_CAR> recentHisKskDriverCars = new List<HIS_KSK_DRIVER_CAR>();
		
        internal HisKskDriverCarCreate()
            : base()
        {

        }

        internal HisKskDriverCarCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_KSK_DRIVER_CAR data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskDriverCarCheck checker = new HisKskDriverCarCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisKskDriverCarDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskDriverCar_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisKskDriverCar that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisKskDriverCars.Add(data);
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
		
		internal bool CreateList(List<HIS_KSK_DRIVER_CAR> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisKskDriverCarCheck checker = new HisKskDriverCarCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisKskDriverCarDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskDriverCar_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisKskDriverCar that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisKskDriverCars.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisKskDriverCars))
            {
                if (!DAOWorker.HisKskDriverCarDAO.TruncateList(this.recentHisKskDriverCars))
                {
                    LogSystem.Warn("Rollback du lieu HisKskDriverCar that bai, can kiem tra lai." + LogUtil.TraceData("recentHisKskDriverCars", this.recentHisKskDriverCars));
                }
				this.recentHisKskDriverCars = null;
            }
        }
    }
}
