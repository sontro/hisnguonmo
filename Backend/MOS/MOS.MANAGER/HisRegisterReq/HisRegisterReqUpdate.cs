using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRegisterReq
{
    partial class HisRegisterReqUpdate : BusinessBase
    {
		private List<HIS_REGISTER_REQ> beforeUpdateHisRegisterReqs = new List<HIS_REGISTER_REQ>();
		
        internal HisRegisterReqUpdate()
            : base()
        {

        }

        internal HisRegisterReqUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_REGISTER_REQ data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRegisterReqCheck checker = new HisRegisterReqCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_REGISTER_REQ raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisRegisterReqs.Add(raw);
					if (!DAOWorker.HisRegisterReqDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRegisterReq_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRegisterReq that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_REGISTER_REQ> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRegisterReqCheck checker = new HisRegisterReqCheck(param);
                List<HIS_REGISTER_REQ> listRaw = new List<HIS_REGISTER_REQ>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisRegisterReqs.AddRange(listRaw);
					if (!DAOWorker.HisRegisterReqDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRegisterReq_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRegisterReq that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisRegisterReqs))
            {
                if (!new HisRegisterReqUpdate(param).UpdateList(this.beforeUpdateHisRegisterReqs))
                {
                    LogSystem.Warn("Rollback du lieu HisRegisterReq that bai, can kiem tra lai." + LogUtil.TraceData("HisRegisterReqs", this.beforeUpdateHisRegisterReqs));
                }
            }
        }
    }
}
