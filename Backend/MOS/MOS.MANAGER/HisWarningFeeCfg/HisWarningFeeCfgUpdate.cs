using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisWarningFeeCfg
{
    partial class HisWarningFeeCfgUpdate : BusinessBase
    {
        private List<HIS_WARNING_FEE_CFG> beforeUpdateHisWarningFeeCfgs = new List<HIS_WARNING_FEE_CFG>();

        internal HisWarningFeeCfgUpdate()
            : base()
        {

        }

        internal HisWarningFeeCfgUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_WARNING_FEE_CFG data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisWarningFeeCfgCheck checker = new HisWarningFeeCfgCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_WARNING_FEE_CFG raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    data.PATIENT_TYPE_ID = HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT;
                    if (!DAOWorker.HisWarningFeeCfgDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisWarningFeeCfg_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisWarningFeeCfg that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisWarningFeeCfgs.Add(raw);
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

        internal bool UpdateList(List<HIS_WARNING_FEE_CFG> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisWarningFeeCfgCheck checker = new HisWarningFeeCfgCheck(param);
                List<HIS_WARNING_FEE_CFG> listRaw = new List<HIS_WARNING_FEE_CFG>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    listData.ForEach(o => o.PATIENT_TYPE_ID = HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT);
                    if (!DAOWorker.HisWarningFeeCfgDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisWarningFeeCfg_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisWarningFeeCfg that bai." + LogUtil.TraceData("listData", listData));
                    }

                    this.beforeUpdateHisWarningFeeCfgs.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisWarningFeeCfgs))
            {
                if (!DAOWorker.HisWarningFeeCfgDAO.UpdateList(this.beforeUpdateHisWarningFeeCfgs))
                {
                    LogSystem.Warn("Rollback du lieu HisWarningFeeCfg that bai, can kiem tra lai." + LogUtil.TraceData("HisWarningFeeCfgs", this.beforeUpdateHisWarningFeeCfgs));
                }
                this.beforeUpdateHisWarningFeeCfgs = null;
            }
        }
    }
}
