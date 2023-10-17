using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPrepare
{
    partial class HisPrepareUpdate : BusinessBase
    {
		private List<HIS_PREPARE> beforeUpdateHisPrepares = new List<HIS_PREPARE>();
		
        internal HisPrepareUpdate()
            : base()
        {

        }

        internal HisPrepareUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_PREPARE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPrepareCheck checker = new HisPrepareCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_PREPARE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    if (String.IsNullOrWhiteSpace(data.REQ_LOGINNAME))
                    {
                        data.REQ_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        data.REQ_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    }
					if (!DAOWorker.HisPrepareDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPrepare_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPrepare that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisPrepares.Add(raw);
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

        internal bool Update(HIS_PREPARE data, HIS_PREPARE before)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPrepareCheck checker = new HisPrepareCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsUnLock(before);
                if (valid)
                {
                    if (!DAOWorker.HisPrepareDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPrepare_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPrepare that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisPrepares.Add(before);
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

        internal bool UpdateList(List<HIS_PREPARE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPrepareCheck checker = new HisPrepareCheck(param);
                List<HIS_PREPARE> listRaw = new List<HIS_PREPARE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisPrepareDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPrepare_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPrepare that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisPrepares.AddRange(listRaw);
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

        internal bool UpdateList(List<HIS_PREPARE> listData, List<HIS_PREPARE> listBefore)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPrepareCheck checker = new HisPrepareCheck(param);
                valid = valid && checker.IsUnLock(listBefore);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisPrepareDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPrepare_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPrepare that bai." + LogUtil.TraceData("listData", listData));
                    }

                    this.beforeUpdateHisPrepares.AddRange(listBefore);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisPrepares))
            {
                if (!DAOWorker.HisPrepareDAO.UpdateList(this.beforeUpdateHisPrepares))
                {
                    LogSystem.Warn("Rollback du lieu HisPrepare that bai, can kiem tra lai." + LogUtil.TraceData("HisPrepares", this.beforeUpdateHisPrepares));
                }
				this.beforeUpdateHisPrepares = null;
            }
        }
    }
}
