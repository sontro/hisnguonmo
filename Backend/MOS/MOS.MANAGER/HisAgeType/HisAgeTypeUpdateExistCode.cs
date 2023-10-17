using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAgeType
{
    partial class HisAgeTypeUpdate : BusinessBase
    {
		private List<HIS_AGE_TYPE> beforeUpdateHisAgeTypes = new List<HIS_AGE_TYPE>();
		
        internal HisAgeTypeUpdate()
            : base()
        {

        }

        internal HisAgeTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_AGE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAgeTypeCheck checker = new HisAgeTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_AGE_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.AGE_TYPE_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisAgeTypeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAgeType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAgeType that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisAgeTypes.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_AGE_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAgeTypeCheck checker = new HisAgeTypeCheck(param);
                List<HIS_AGE_TYPE> listRaw = new List<HIS_AGE_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.AGE_TYPE_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisAgeTypeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAgeType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAgeType that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisAgeTypes.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisAgeTypes))
            {
                if (!DAOWorker.HisAgeTypeDAO.UpdateList(this.beforeUpdateHisAgeTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisAgeType that bai, can kiem tra lai." + LogUtil.TraceData("HisAgeTypes", this.beforeUpdateHisAgeTypes));
                }
				this.beforeUpdateHisAgeTypes = null;
            }
        }
    }
}
