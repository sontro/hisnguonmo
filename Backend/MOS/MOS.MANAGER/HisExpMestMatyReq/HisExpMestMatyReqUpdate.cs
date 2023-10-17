using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpMestMatyReq
{
    partial class HisExpMestMatyReqUpdate : BusinessBase
    {
        private List<HIS_EXP_MEST_MATY_REQ> beforeUpdateHisExpMestMatyReqs = new List<HIS_EXP_MEST_MATY_REQ>();

        internal HisExpMestMatyReqUpdate()
            : base()
        {

        }

        internal HisExpMestMatyReqUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EXP_MEST_MATY_REQ data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExpMestMatyReqCheck checker = new HisExpMestMatyReqCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_EXP_MEST_MATY_REQ raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisExpMestMatyReqs.Add(raw);
                    if (!DAOWorker.HisExpMestMatyReqDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestMatyReq_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExpMestMatyReq that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_EXP_MEST_MATY_REQ> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpMestMatyReqCheck checker = new HisExpMestMatyReqCheck(param);
                List<HIS_EXP_MEST_MATY_REQ> listRaw = new List<HIS_EXP_MEST_MATY_REQ>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisExpMestMatyReqs.AddRange(listRaw);
                    if (!DAOWorker.HisExpMestMatyReqDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestMatyReq_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExpMestMatyReq that bai." + LogUtil.TraceData("listData", listData));
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

        internal bool UpdateList(List<HIS_EXP_MEST_MATY_REQ> listData, List<HIS_EXP_MEST_MATY_REQ> beforeUpdates)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpMestMatyReqCheck checker = new HisExpMestMatyReqCheck(param);
                valid = valid && checker.IsUnLock(beforeUpdates);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisExpMestMatyReqs.AddRange(beforeUpdates);
                    if (!DAOWorker.HisExpMestMatyReqDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestMatyReq_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExpMestMatyReq that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisExpMestMatyReqs))
            {
                if (!DAOWorker.HisExpMestMatyReqDAO.UpdateList(this.beforeUpdateHisExpMestMatyReqs))
                {
                    LogSystem.Warn("Rollback du lieu HisExpMestMatyReq that bai, can kiem tra lai." + LogUtil.TraceData("HisExpMestMatyReqs", this.beforeUpdateHisExpMestMatyReqs));
                }
            }
        }
    }
}
