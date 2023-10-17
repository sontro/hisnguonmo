using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBcsMetyReqDt
{
    partial class HisBcsMetyReqDtUpdate : BusinessBase
    {
        private List<HIS_BCS_METY_REQ_DT> beforeUpdateHisBcsMetyReqDts = new List<HIS_BCS_METY_REQ_DT>();

        internal HisBcsMetyReqDtUpdate()
            : base()
        {

        }

        internal HisBcsMetyReqDtUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_BCS_METY_REQ_DT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBcsMetyReqDtCheck checker = new HisBcsMetyReqDtCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_BCS_METY_REQ_DT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    if (!DAOWorker.HisBcsMetyReqDtDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBcsMetyReqDt_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBcsMetyReqDt that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisBcsMetyReqDts.Add(raw);
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

        internal bool UpdateList(List<HIS_BCS_METY_REQ_DT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBcsMetyReqDtCheck checker = new HisBcsMetyReqDtCheck(param);
                List<HIS_BCS_METY_REQ_DT> listRaw = new List<HIS_BCS_METY_REQ_DT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisBcsMetyReqDtDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBcsMetyReqDt_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBcsMetyReqDt that bai." + LogUtil.TraceData("listData", listData));
                    }

                    this.beforeUpdateHisBcsMetyReqDts.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisBcsMetyReqDts))
            {
                if (!DAOWorker.HisBcsMetyReqDtDAO.UpdateList(this.beforeUpdateHisBcsMetyReqDts))
                {
                    LogSystem.Warn("Rollback du lieu HisBcsMetyReqDt that bai, can kiem tra lai." + LogUtil.TraceData("HisBcsMetyReqDts", this.beforeUpdateHisBcsMetyReqDts));
                }
                this.beforeUpdateHisBcsMetyReqDts = null;
            }
        }
    }
}
