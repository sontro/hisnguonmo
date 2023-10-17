using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskDriver
{
    partial class HisKskDriverCreate : BusinessBase
    {
		private List<HIS_KSK_DRIVER> recentHisKskDrivers = new List<HIS_KSK_DRIVER>();
		
        internal HisKskDriverCreate()
            : base()
        {

        }

        internal HisKskDriverCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_KSK_DRIVER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskDriverCheck checker = new HisKskDriverCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisKskDriverDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskDriver_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisKskDriver that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisKskDrivers.Add(data);
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
		
		internal bool CreateList(List<HIS_KSK_DRIVER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisKskDriverCheck checker = new HisKskDriverCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisKskDriverDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskDriver_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisKskDriver that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisKskDrivers.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisKskDrivers))
            {
                if (!DAOWorker.HisKskDriverDAO.TruncateList(this.recentHisKskDrivers))
                {
                    LogSystem.Warn("Rollback du lieu HisKskDriver that bai, can kiem tra lai." + LogUtil.TraceData("recentHisKskDrivers", this.recentHisKskDrivers));
                }
				this.recentHisKskDrivers = null;
            }
        }
    }
}
