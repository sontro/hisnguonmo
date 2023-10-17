using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceReqMety
{
    partial class HisServiceReqMetyUpdate : BusinessBase
    {
		private List<HIS_SERVICE_REQ_METY> beforeUpdateHisServiceReqMetys = new List<HIS_SERVICE_REQ_METY>();
		
        internal HisServiceReqMetyUpdate()
            : base()
        {

        }

        internal HisServiceReqMetyUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SERVICE_REQ_METY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceReqMetyCheck checker = new HisServiceReqMetyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SERVICE_REQ_METY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisServiceReqMetys.Add(raw);
					if (!DAOWorker.HisServiceReqMetyDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceReqMety_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisServiceReqMety that bai." + LogUtil.TraceData("data", data));
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

        internal bool Update(HIS_SERVICE_REQ_METY data, HIS_SERVICE_REQ_METY before)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceReqMetyCheck checker = new HisServiceReqMetyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsUnLock(before);
                if (valid)
                {
                    if (!DAOWorker.HisServiceReqMetyDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceReqMety_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisServiceReqMety that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisServiceReqMetys.Add(before);

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

        internal bool UpdateList(List<HIS_SERVICE_REQ_METY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceReqMetyCheck checker = new HisServiceReqMetyCheck(param);
                List<HIS_SERVICE_REQ_METY> listRaw = new List<HIS_SERVICE_REQ_METY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisServiceReqMetys.AddRange(listRaw);
					if (!DAOWorker.HisServiceReqMetyDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceReqMety_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisServiceReqMety that bai." + LogUtil.TraceData("listData", listData));
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

        internal bool UpdateList(List<HIS_SERVICE_REQ_METY> listData, List<HIS_SERVICE_REQ_METY> befores)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceReqMetyCheck checker = new HisServiceReqMetyCheck(param);
                valid = valid && checker.IsUnLock(befores);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisServiceReqMetys.AddRange(befores);
                    if (!DAOWorker.HisServiceReqMetyDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceReqMety_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisServiceReqMety that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisServiceReqMetys))
            {
                if (!DAOWorker.HisServiceReqMetyDAO.UpdateList(this.beforeUpdateHisServiceReqMetys))
                {
                    LogSystem.Warn("Rollback du lieu HisServiceReqMety that bai, can kiem tra lai." + LogUtil.TraceData("HisServiceReqMetys", this.beforeUpdateHisServiceReqMetys));
                }
            }
        }
    }
}
