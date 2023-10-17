using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBidType
{
    partial class HisBidTypeUpdate : BusinessBase
    {
		private List<HIS_BID_TYPE> beforeUpdateHisBidTypes = new List<HIS_BID_TYPE>();
		
        internal HisBidTypeUpdate()
            : base()
        {

        }

        internal HisBidTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_BID_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBidTypeCheck checker = new HisBidTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_BID_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisBidTypes.Add(raw);
					if (!DAOWorker.HisBidTypeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBidType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBidType that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_BID_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBidTypeCheck checker = new HisBidTypeCheck(param);
                List<HIS_BID_TYPE> listRaw = new List<HIS_BID_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisBidTypes.AddRange(listRaw);
					if (!DAOWorker.HisBidTypeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBidType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBidType that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisBidTypes))
            {
                if (!new HisBidTypeUpdate(param).UpdateList(this.beforeUpdateHisBidTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisBidType that bai, can kiem tra lai." + LogUtil.TraceData("HisBidTypes", this.beforeUpdateHisBidTypes));
                }
            }
        }
    }
}
