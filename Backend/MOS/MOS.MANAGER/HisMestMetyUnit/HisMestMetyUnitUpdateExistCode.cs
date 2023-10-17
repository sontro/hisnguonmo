using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMestMetyUnit
{
    partial class HisMestMetyUnitUpdate : BusinessBase
    {
		private List<HIS_MEST_METY_UNIT> beforeUpdateHisMestMetyUnits = new List<HIS_MEST_METY_UNIT>();
		
        internal HisMestMetyUnitUpdate()
            : base()
        {

        }

        internal HisMestMetyUnitUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEST_METY_UNIT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestMetyUnitCheck checker = new HisMestMetyUnitCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MEST_METY_UNIT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.MEST_METY_UNIT_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisMestMetyUnitDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestMetyUnit_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMestMetyUnit that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisMestMetyUnits.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_MEST_METY_UNIT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMestMetyUnitCheck checker = new HisMestMetyUnitCheck(param);
                List<HIS_MEST_METY_UNIT> listRaw = new List<HIS_MEST_METY_UNIT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.MEST_METY_UNIT_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisMestMetyUnitDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestMetyUnit_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMestMetyUnit that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisMestMetyUnits.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMestMetyUnits))
            {
                if (!DAOWorker.HisMestMetyUnitDAO.UpdateList(this.beforeUpdateHisMestMetyUnits))
                {
                    LogSystem.Warn("Rollback du lieu HisMestMetyUnit that bai, can kiem tra lai." + LogUtil.TraceData("HisMestMetyUnits", this.beforeUpdateHisMestMetyUnits));
                }
				this.beforeUpdateHisMestMetyUnits = null;
            }
        }
    }
}
