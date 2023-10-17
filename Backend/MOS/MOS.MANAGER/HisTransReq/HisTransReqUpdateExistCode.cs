using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTransReq
{
    partial class HisTransReqUpdate : BusinessBase
    {
		private List<HIS_TRANS_REQ> beforeUpdateHisTransReqs = new List<HIS_TRANS_REQ>();
		
        internal HisTransReqUpdate()
            : base()
        {

        }

        internal HisTransReqUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_TRANS_REQ data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTransReqCheck checker = new HisTransReqCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_TRANS_REQ raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                //valid = valid && checker.ExistsCode(data.TRANS_REQ_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisTransReqDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransReq_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTransReq that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisTransReqs.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_TRANS_REQ> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTransReqCheck checker = new HisTransReqCheck(param);
                List<HIS_TRANS_REQ> listRaw = new List<HIS_TRANS_REQ>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    //valid = valid && checker.ExistsCode(data.TRANS_REQ_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisTransReqDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransReq_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTransReq that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisTransReqs.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisTransReqs))
            {
                if (!DAOWorker.HisTransReqDAO.UpdateList(this.beforeUpdateHisTransReqs))
                {
                    LogSystem.Warn("Rollback du lieu HisTransReq that bai, can kiem tra lai." + LogUtil.TraceData("HisTransReqs", this.beforeUpdateHisTransReqs));
                }
				this.beforeUpdateHisTransReqs = null;
            }
        }
    }
}
