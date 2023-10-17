using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCashierAddConfig
{
    partial class HisCashierAddConfigCreate : BusinessBase
    {
		private List<HIS_CASHIER_ADD_CONFIG> recentHisCashierAddConfigs = new List<HIS_CASHIER_ADD_CONFIG>();
		
        internal HisCashierAddConfigCreate()
            : base()
        {

        }

        internal HisCashierAddConfigCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_CASHIER_ADD_CONFIG data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCashierAddConfigCheck checker = new HisCashierAddConfigCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisCashierAddConfigDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCashierAddConfig_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisCashierAddConfig that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisCashierAddConfigs.Add(data);
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
		
		internal bool CreateList(List<HIS_CASHIER_ADD_CONFIG> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCashierAddConfigCheck checker = new HisCashierAddConfigCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisCashierAddConfigDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCashierAddConfig_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisCashierAddConfig that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisCashierAddConfigs.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisCashierAddConfigs))
            {
                if (!DAOWorker.HisCashierAddConfigDAO.TruncateList(this.recentHisCashierAddConfigs))
                {
                    LogSystem.Warn("Rollback du lieu HisCashierAddConfig that bai, can kiem tra lai." + LogUtil.TraceData("recentHisCashierAddConfigs", this.recentHisCashierAddConfigs));
                }
				this.recentHisCashierAddConfigs = null;
            }
        }
    }
}
