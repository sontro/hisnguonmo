using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBcsMatyReqReq
{
    partial class HisBcsMatyReqReqUpdate : BusinessBase
    {
        private List<HIS_BCS_MATY_REQ_REQ> beforeUpdateHisBcsMatyReqReqs = new List<HIS_BCS_MATY_REQ_REQ>();

        internal HisBcsMatyReqReqUpdate()
            : base()
        {

        }

        internal HisBcsMatyReqReqUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_BCS_MATY_REQ_REQ data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBcsMatyReqReqCheck checker = new HisBcsMatyReqReqCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_BCS_MATY_REQ_REQ raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    if (!DAOWorker.HisBcsMatyReqReqDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBcsMatyReqReq_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBcsMatyReqReq that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisBcsMatyReqReqs.Add(raw);
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

        internal bool UpdateList(List<HIS_BCS_MATY_REQ_REQ> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBcsMatyReqReqCheck checker = new HisBcsMatyReqReqCheck(param);
                List<HIS_BCS_MATY_REQ_REQ> listRaw = new List<HIS_BCS_MATY_REQ_REQ>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisBcsMatyReqReqDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBcsMatyReqReq_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBcsMatyReqReq that bai." + LogUtil.TraceData("listData", listData));
                    }

                    this.beforeUpdateHisBcsMatyReqReqs.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisBcsMatyReqReqs))
            {
                if (!DAOWorker.HisBcsMatyReqReqDAO.UpdateList(this.beforeUpdateHisBcsMatyReqReqs))
                {
                    LogSystem.Warn("Rollback du lieu HisBcsMatyReqReq that bai, can kiem tra lai." + LogUtil.TraceData("HisBcsMatyReqReqs", this.beforeUpdateHisBcsMatyReqReqs));
                }
                this.beforeUpdateHisBcsMatyReqReqs = null;
            }
        }
    }
}
