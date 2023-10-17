using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBornType
{
    partial class HisBornTypeUpdate : BusinessBase
    {
		private List<HIS_BORN_TYPE> beforeUpdateHisBornTypes = new List<HIS_BORN_TYPE>();
		
        internal HisBornTypeUpdate()
            : base()
        {

        }

        internal HisBornTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_BORN_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBornTypeCheck checker = new HisBornTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_BORN_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.BORN_TYPE_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisBornTypes.Add(raw);
					if (!DAOWorker.HisBornTypeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBornType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBornType that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_BORN_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBornTypeCheck checker = new HisBornTypeCheck(param);
                List<HIS_BORN_TYPE> listRaw = new List<HIS_BORN_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.BORN_TYPE_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisBornTypes.AddRange(listRaw);
					if (!DAOWorker.HisBornTypeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBornType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBornType that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisBornTypes))
            {
                if (!new HisBornTypeUpdate(param).UpdateList(this.beforeUpdateHisBornTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisBornType that bai, can kiem tra lai." + LogUtil.TraceData("HisBornTypes", this.beforeUpdateHisBornTypes));
                }
            }
        }
    }
}
