using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBcsMatyReqDt
{
    partial class HisBcsMatyReqDtUpdate : BusinessBase
    {
        private List<HIS_BCS_MATY_REQ_DT> beforeUpdateHisBcsMatyReqDts = new List<HIS_BCS_MATY_REQ_DT>();

        internal HisBcsMatyReqDtUpdate()
            : base()
        {

        }

        internal HisBcsMatyReqDtUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_BCS_MATY_REQ_DT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBcsMatyReqDtCheck checker = new HisBcsMatyReqDtCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_BCS_MATY_REQ_DT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    if (!DAOWorker.HisBcsMatyReqDtDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBcsMatyReqDt_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBcsMatyReqDt that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisBcsMatyReqDts.Add(raw);
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

        internal bool UpdateList(List<HIS_BCS_MATY_REQ_DT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBcsMatyReqDtCheck checker = new HisBcsMatyReqDtCheck(param);
                List<HIS_BCS_MATY_REQ_DT> listRaw = new List<HIS_BCS_MATY_REQ_DT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisBcsMatyReqDtDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBcsMatyReqDt_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBcsMatyReqDt that bai." + LogUtil.TraceData("listData", listData));
                    }

                    this.beforeUpdateHisBcsMatyReqDts.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisBcsMatyReqDts))
            {
                if (!DAOWorker.HisBcsMatyReqDtDAO.UpdateList(this.beforeUpdateHisBcsMatyReqDts))
                {
                    LogSystem.Warn("Rollback du lieu HisBcsMatyReqDt that bai, can kiem tra lai." + LogUtil.TraceData("HisBcsMatyReqDts", this.beforeUpdateHisBcsMatyReqDts));
                }
                this.beforeUpdateHisBcsMatyReqDts = null;
            }
        }
    }
}
