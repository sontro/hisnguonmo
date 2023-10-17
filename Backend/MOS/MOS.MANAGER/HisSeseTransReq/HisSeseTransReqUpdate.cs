using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSeseTransReq
{
    partial class HisSeseTransReqUpdate : BusinessBase
    {
		private List<HIS_SESE_TRANS_REQ> beforeUpdateHisSeseTransReqs = new List<HIS_SESE_TRANS_REQ>();
		
        internal HisSeseTransReqUpdate()
            : base()
        {

        }

        internal HisSeseTransReqUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SESE_TRANS_REQ data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSeseTransReqCheck checker = new HisSeseTransReqCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SESE_TRANS_REQ raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisSeseTransReqDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSeseTransReq_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSeseTransReq that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisSeseTransReqs.Add(raw);
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

        internal bool UpdateList(List<HIS_SESE_TRANS_REQ> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSeseTransReqCheck checker = new HisSeseTransReqCheck(param);
                List<HIS_SESE_TRANS_REQ> listRaw = new List<HIS_SESE_TRANS_REQ>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisSeseTransReqDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSeseTransReq_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSeseTransReq that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisSeseTransReqs.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisSeseTransReqs))
            {
                if (!DAOWorker.HisSeseTransReqDAO.UpdateList(this.beforeUpdateHisSeseTransReqs))
                {
                    LogSystem.Warn("Rollback du lieu HisSeseTransReq that bai, can kiem tra lai." + LogUtil.TraceData("HisSeseTransReqs", this.beforeUpdateHisSeseTransReqs));
                }
				this.beforeUpdateHisSeseTransReqs = null;
            }
        }
    }
}
