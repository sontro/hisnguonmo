using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceChangeReq
{
    partial class HisServiceChangeReqUpdate : BusinessBase
    {
		private List<HIS_SERVICE_CHANGE_REQ> beforeUpdateHisServiceChangeReqs = new List<HIS_SERVICE_CHANGE_REQ>();
		
        internal HisServiceChangeReqUpdate()
            : base()
        {

        }

        internal HisServiceChangeReqUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SERVICE_CHANGE_REQ data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceChangeReqCheck checker = new HisServiceChangeReqCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SERVICE_CHANGE_REQ raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisServiceChangeReqDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceChangeReq_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisServiceChangeReq that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisServiceChangeReqs.Add(raw);
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

        internal bool UpdateList(List<HIS_SERVICE_CHANGE_REQ> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceChangeReqCheck checker = new HisServiceChangeReqCheck(param);
                List<HIS_SERVICE_CHANGE_REQ> listRaw = new List<HIS_SERVICE_CHANGE_REQ>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisServiceChangeReqDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceChangeReq_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisServiceChangeReq that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisServiceChangeReqs.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisServiceChangeReqs))
            {
                if (!DAOWorker.HisServiceChangeReqDAO.UpdateList(this.beforeUpdateHisServiceChangeReqs))
                {
                    LogSystem.Warn("Rollback du lieu HisServiceChangeReq that bai, can kiem tra lai." + LogUtil.TraceData("HisServiceChangeReqs", this.beforeUpdateHisServiceChangeReqs));
                }
				this.beforeUpdateHisServiceChangeReqs = null;
            }
        }
    }
}
