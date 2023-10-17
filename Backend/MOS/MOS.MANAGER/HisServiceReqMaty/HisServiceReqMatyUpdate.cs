using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceReqMaty
{
    partial class HisServiceReqMatyUpdate : BusinessBase
    {
		private List<HIS_SERVICE_REQ_MATY> beforeUpdateHisServiceReqMatys = new List<HIS_SERVICE_REQ_MATY>();
		
        internal HisServiceReqMatyUpdate()
            : base()
        {

        }

        internal HisServiceReqMatyUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SERVICE_REQ_MATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceReqMatyCheck checker = new HisServiceReqMatyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SERVICE_REQ_MATY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsNotExisted(data);
                if (valid)
                {
                    this.beforeUpdateHisServiceReqMatys.Add(raw);
					if (!DAOWorker.HisServiceReqMatyDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceReqMaty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisServiceReqMaty that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_SERVICE_REQ_MATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceReqMatyCheck checker = new HisServiceReqMatyCheck(param);
                List<HIS_SERVICE_REQ_MATY> listRaw = new List<HIS_SERVICE_REQ_MATY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisServiceReqMatys.AddRange(listRaw);
					if (!DAOWorker.HisServiceReqMatyDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceReqMaty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisServiceReqMaty that bai." + LogUtil.TraceData("listData", listData));
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

        internal bool UpdateList(List<HIS_SERVICE_REQ_MATY> listData, List<HIS_SERVICE_REQ_MATY> befores)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceReqMatyCheck checker = new HisServiceReqMatyCheck(param);
                valid = valid && checker.IsUnLock(befores);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisServiceReqMatys.AddRange(befores);
                    if (!DAOWorker.HisServiceReqMatyDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceReqMaty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisServiceReqMaty that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisServiceReqMatys))
            {
                if (!new HisServiceReqMatyUpdate(param).UpdateList(this.beforeUpdateHisServiceReqMatys))
                {
                    LogSystem.Warn("Rollback du lieu HisServiceReqMaty that bai, can kiem tra lai." + LogUtil.TraceData("HisServiceReqMatys", this.beforeUpdateHisServiceReqMatys));
                }
            }
        }
    }
}
