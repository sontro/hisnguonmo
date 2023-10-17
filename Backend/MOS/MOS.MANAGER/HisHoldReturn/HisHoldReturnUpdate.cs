using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisHoldReturn
{
    partial class HisHoldReturnUpdate : BusinessBase
    {
		private List<HIS_HOLD_RETURN> beforeUpdateHisHoldReturns = new List<HIS_HOLD_RETURN>();
		
        internal HisHoldReturnUpdate()
            : base()
        {

        }

        internal HisHoldReturnUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_HOLD_RETURN data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisHoldReturnCheck checker = new HisHoldReturnCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_HOLD_RETURN raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisHoldReturnDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHoldReturn_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisHoldReturn that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisHoldReturns.Add(raw);
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

        internal bool Update(HIS_HOLD_RETURN data, HIS_HOLD_RETURN before)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisHoldReturnCheck checker = new HisHoldReturnCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsUnLock(before);
                if (valid)
                {
                    if (!DAOWorker.HisHoldReturnDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHoldReturn_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisHoldReturn that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisHoldReturns.Add(before);
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

        internal bool UpdateList(List<HIS_HOLD_RETURN> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisHoldReturnCheck checker = new HisHoldReturnCheck(param);
                List<HIS_HOLD_RETURN> listRaw = new List<HIS_HOLD_RETURN>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisHoldReturnDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHoldReturn_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisHoldReturn that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisHoldReturns.AddRange(listRaw);
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

        internal bool UpdateList(List<HIS_HOLD_RETURN> listData,List<HIS_HOLD_RETURN> listBefore)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisHoldReturnCheck checker = new HisHoldReturnCheck(param);
                valid = valid && checker.IsUnLock(listBefore);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisHoldReturnDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHoldReturn_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisHoldReturn that bai." + LogUtil.TraceData("listData", listData));
                    }

                    this.beforeUpdateHisHoldReturns.AddRange(listBefore);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisHoldReturns))
            {
                if (!DAOWorker.HisHoldReturnDAO.UpdateList(this.beforeUpdateHisHoldReturns))
                {
                    LogSystem.Warn("Rollback du lieu HisHoldReturn that bai, can kiem tra lai." + LogUtil.TraceData("HisHoldReturns", this.beforeUpdateHisHoldReturns));
                }
				this.beforeUpdateHisHoldReturns = null;
            }
        }
    }
}
