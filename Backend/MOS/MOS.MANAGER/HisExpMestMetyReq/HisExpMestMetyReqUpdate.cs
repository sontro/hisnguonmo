using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpMestMetyReq
{
    partial class HisExpMestMetyReqUpdate : BusinessBase
    {
        private List<HIS_EXP_MEST_METY_REQ> beforeUpdateHisExpMestMetyReqs = new List<HIS_EXP_MEST_METY_REQ>();

        internal HisExpMestMetyReqUpdate()
            : base()
        {

        }

        internal HisExpMestMetyReqUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EXP_MEST_METY_REQ data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExpMestMetyReqCheck checker = new HisExpMestMetyReqCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_EXP_MEST_METY_REQ raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisExpMestMetyReqs.Add(raw);
                    if (!DAOWorker.HisExpMestMetyReqDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestMetyReq_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExpMestMetyReq that bai." + LogUtil.TraceData("data", data));
                    }

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

        internal bool UpdateList(List<HIS_EXP_MEST_METY_REQ> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpMestMetyReqCheck checker = new HisExpMestMetyReqCheck(param);
                List<HIS_EXP_MEST_METY_REQ> listRaw = new List<HIS_EXP_MEST_METY_REQ>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisExpMestMetyReqs.AddRange(listRaw);
                    if (!DAOWorker.HisExpMestMetyReqDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestMetyReq_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExpMestMetyReq that bai." + LogUtil.TraceData("listData", listData));
                    }
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

        internal bool UpdateList(List<HIS_EXP_MEST_METY_REQ> toUpdates, List<HIS_EXP_MEST_METY_REQ> beforeUpdates)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(toUpdates);
                HisExpMestMetyReqCheck checker = new HisExpMestMetyReqCheck(param);
                valid = valid && checker.IsUnLock(beforeUpdates);
                foreach (var data in toUpdates)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisExpMestMetyReqs.AddRange(beforeUpdates);
                    if (!DAOWorker.HisExpMestMetyReqDAO.UpdateList(toUpdates))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestMetyReq_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExpMestMetyReq that bai." + LogUtil.TraceData("toUpdates", toUpdates));
                    }
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisExpMestMetyReqs))
            {
                if (!DAOWorker.HisExpMestMetyReqDAO.UpdateList(this.beforeUpdateHisExpMestMetyReqs))
                {
                    LogSystem.Warn("Rollback du lieu HisExpMestMetyReq that bai, can kiem tra lai." + LogUtil.TraceData("HisExpMestMetyReqs", this.beforeUpdateHisExpMestMetyReqs));
                }
            }
        }
    }
}
