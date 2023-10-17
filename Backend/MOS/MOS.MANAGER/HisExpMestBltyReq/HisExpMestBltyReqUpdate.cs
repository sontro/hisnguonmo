using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpMestBltyReq
{
    partial class HisExpMestBltyReqUpdate : BusinessBase
    {
		private List<HIS_EXP_MEST_BLTY_REQ> beforeUpdateHisExpMestBltyReqs = new List<HIS_EXP_MEST_BLTY_REQ>();
		
        internal HisExpMestBltyReqUpdate()
            : base()
        {

        }

        internal HisExpMestBltyReqUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EXP_MEST_BLTY_REQ data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExpMestBltyReqCheck checker = new HisExpMestBltyReqCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_EXP_MEST_BLTY_REQ raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    //ko thuoc goi nao thi set truong IS_OUT_PARENT_FEE = null
                    if (!data.SERE_SERV_PARENT_ID.HasValue)
                    {
                        data.IS_OUT_PARENT_FEE = null;
                    }

                    this.beforeUpdateHisExpMestBltyReqs.Add(raw);
					if (!DAOWorker.HisExpMestBltyReqDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestBltyReq_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExpMestBltyReq that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_EXP_MEST_BLTY_REQ> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpMestBltyReqCheck checker = new HisExpMestBltyReqCheck(param);
                List<HIS_EXP_MEST_BLTY_REQ> listRaw = new List<HIS_EXP_MEST_BLTY_REQ>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisExpMestBltyReqs.AddRange(listRaw);
					if (!DAOWorker.HisExpMestBltyReqDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestBltyReq_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExpMestBltyReq that bai." + LogUtil.TraceData("listData", listData));
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

        internal bool UpdateList(List<HIS_EXP_MEST_BLTY_REQ> toUpdates, List<HIS_EXP_MEST_BLTY_REQ> beforeUpdates)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(toUpdates);
                HisExpMestBltyReqCheck checker = new HisExpMestBltyReqCheck(param);
                valid = valid && checker.IsUnLock(beforeUpdates);
                foreach (var data in toUpdates)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisExpMestBltyReqs.AddRange(beforeUpdates);
                    if (!DAOWorker.HisExpMestBltyReqDAO.UpdateList(toUpdates))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestBltyReq_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExpMestBltyReq that bai." + LogUtil.TraceData("toUpdates", toUpdates));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisExpMestBltyReqs))
            {
                if (!DAOWorker.HisExpMestBltyReqDAO.UpdateList(this.beforeUpdateHisExpMestBltyReqs))
                {
                    LogSystem.Warn("Rollback du lieu HisExpMestBltyReq that bai, can kiem tra lai." + LogUtil.TraceData("HisExpMestBltyReqs", this.beforeUpdateHisExpMestBltyReqs));
                }
            }
        }
    }
}
