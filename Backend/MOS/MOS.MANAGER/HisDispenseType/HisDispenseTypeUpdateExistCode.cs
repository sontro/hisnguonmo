using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDispenseType
{
    partial class HisDispenseTypeUpdate : BusinessBase
    {
		private List<HIS_DISPENSE_TYPE> beforeUpdateHisDispenseTypes = new List<HIS_DISPENSE_TYPE>();
		
        internal HisDispenseTypeUpdate()
            : base()
        {

        }

        internal HisDispenseTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_DISPENSE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDispenseTypeCheck checker = new HisDispenseTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_DISPENSE_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.DISPENSE_TYPE_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisDispenseTypeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDispenseType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDispenseType that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisDispenseTypes.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_DISPENSE_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDispenseTypeCheck checker = new HisDispenseTypeCheck(param);
                List<HIS_DISPENSE_TYPE> listRaw = new List<HIS_DISPENSE_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.DISPENSE_TYPE_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisDispenseTypeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDispenseType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDispenseType that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisDispenseTypes.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisDispenseTypes))
            {
                if (!DAOWorker.HisDispenseTypeDAO.UpdateList(this.beforeUpdateHisDispenseTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisDispenseType that bai, can kiem tra lai." + LogUtil.TraceData("HisDispenseTypes", this.beforeUpdateHisDispenseTypes));
                }
				this.beforeUpdateHisDispenseTypes = null;
            }
        }
    }
}
