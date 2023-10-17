using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisConfig
{
    partial class HisConfigCreate : BusinessBase
    {
        private List<HIS_CONFIG> recentHisConfigs = new List<HIS_CONFIG>();

        internal HisConfigCreate()
            : base()
        {

        }

        internal HisConfigCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_CONFIG data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisConfigCheck checker = new HisConfigCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.KEY, data.BRANCH_ID, null);
                if (valid)
                {
                    if (!DAOWorker.HisConfigDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisConfig_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisConfig that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisConfigs.Add(data);
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

        internal bool CreateList(List<HIS_CONFIG> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisConfigCheck checker = new HisConfigCheck(param);
                valid = valid && IsNotNullOrEmpty(listData);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.KEY, data.BRANCH_ID, null);
                }
                if (valid)
                {
                    if (!DAOWorker.HisConfigDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisConfig_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisConfig that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisConfigs.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisConfigs))
            {
                if (!DAOWorker.HisConfigDAO.TruncateList(this.recentHisConfigs))
                {
                    LogSystem.Warn("Rollback du lieu HisConfig that bai, can kiem tra lai." + LogUtil.TraceData("recentHisConfigs", this.recentHisConfigs));
                }
            }
        }
    }
}
