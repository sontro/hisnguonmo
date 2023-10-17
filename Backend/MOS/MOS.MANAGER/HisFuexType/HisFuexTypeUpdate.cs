using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisFuexType
{
    partial class HisFuexTypeUpdate : BusinessBase
    {
		private List<HIS_FUEX_TYPE> beforeUpdateHisFuexTypes = new List<HIS_FUEX_TYPE>();
		
        internal HisFuexTypeUpdate()
            : base()
        {

        }

        internal HisFuexTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_FUEX_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisFuexTypeCheck checker = new HisFuexTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_FUEX_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisFuexTypeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisFuexType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisFuexType that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisFuexTypes.Add(raw);
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

        internal bool UpdateList(List<HIS_FUEX_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisFuexTypeCheck checker = new HisFuexTypeCheck(param);
                List<HIS_FUEX_TYPE> listRaw = new List<HIS_FUEX_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisFuexTypeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisFuexType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisFuexType that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisFuexTypes.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisFuexTypes))
            {
                if (!DAOWorker.HisFuexTypeDAO.UpdateList(this.beforeUpdateHisFuexTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisFuexType that bai, can kiem tra lai." + LogUtil.TraceData("HisFuexTypes", this.beforeUpdateHisFuexTypes));
                }
				this.beforeUpdateHisFuexTypes = null;
            }
        }
    }
}
