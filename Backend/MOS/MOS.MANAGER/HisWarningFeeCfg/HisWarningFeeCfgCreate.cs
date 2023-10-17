using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisWarningFeeCfg
{
    partial class HisWarningFeeCfgCreate : BusinessBase
    {
        private List<HIS_WARNING_FEE_CFG> recentHisWarningFeeCfgs = new List<HIS_WARNING_FEE_CFG>();

        internal HisWarningFeeCfgCreate()
            : base()
        {

        }

        internal HisWarningFeeCfgCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_WARNING_FEE_CFG data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisWarningFeeCfgCheck checker = new HisWarningFeeCfgCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    data.PATIENT_TYPE_ID = HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT;
                    if (!DAOWorker.HisWarningFeeCfgDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisWarningFeeCfg_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisWarningFeeCfg that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisWarningFeeCfgs.Add(data);
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

        internal bool CreateList(List<HIS_WARNING_FEE_CFG> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisWarningFeeCfgCheck checker = new HisWarningFeeCfgCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    listData.ForEach(o => o.PATIENT_TYPE_ID = HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT);
                    if (!DAOWorker.HisWarningFeeCfgDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisWarningFeeCfg_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisWarningFeeCfg that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisWarningFeeCfgs.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisWarningFeeCfgs))
            {
                if (!DAOWorker.HisWarningFeeCfgDAO.TruncateList(this.recentHisWarningFeeCfgs))
                {
                    LogSystem.Warn("Rollback du lieu HisWarningFeeCfg that bai, can kiem tra lai." + LogUtil.TraceData("recentHisWarningFeeCfgs", this.recentHisWarningFeeCfgs));
                }
                this.recentHisWarningFeeCfgs = null;
            }
        }
    }
}
