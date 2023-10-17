using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisImpMestPropose
{
    partial class HisImpMestProposeUpdate : BusinessBase
    {
        private List<HIS_IMP_MEST_PROPOSE> beforeUpdateHisImpMestProposes = new List<HIS_IMP_MEST_PROPOSE>();

        internal HisImpMestProposeUpdate()
            : base()
        {

        }

        internal HisImpMestProposeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_IMP_MEST_PROPOSE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpMestProposeCheck checker = new HisImpMestProposeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_IMP_MEST_PROPOSE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    if (!DAOWorker.HisImpMestProposeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMestPropose_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisImpMestPropose that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisImpMestProposes.Add(raw);
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

        internal bool Update(HIS_IMP_MEST_PROPOSE data, HIS_IMP_MEST_PROPOSE before)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpMestProposeCheck checker = new HisImpMestProposeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsUnLock(before);
                if (valid)
                {
                    if (!DAOWorker.HisImpMestProposeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMestPropose_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisImpMestPropose that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisImpMestProposes.Add(before);
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

        internal bool UpdateList(List<HIS_IMP_MEST_PROPOSE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisImpMestProposeCheck checker = new HisImpMestProposeCheck(param);
                List<HIS_IMP_MEST_PROPOSE> listRaw = new List<HIS_IMP_MEST_PROPOSE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisImpMestProposeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMestPropose_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisImpMestPropose that bai." + LogUtil.TraceData("listData", listData));
                    }

                    this.beforeUpdateHisImpMestProposes.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisImpMestProposes))
            {
                if (!DAOWorker.HisImpMestProposeDAO.UpdateList(this.beforeUpdateHisImpMestProposes))
                {
                    LogSystem.Warn("Rollback du lieu HisImpMestPropose that bai, can kiem tra lai." + LogUtil.TraceData("HisImpMestProposes", this.beforeUpdateHisImpMestProposes));
                }
                this.beforeUpdateHisImpMestProposes = null;
            }
        }
    }
}
