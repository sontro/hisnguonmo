using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDiimType
{
    partial class HisDiimTypeUpdate : BusinessBase
    {
		private List<HIS_DIIM_TYPE> beforeUpdateHisDiimTypes = new List<HIS_DIIM_TYPE>();
		
        internal HisDiimTypeUpdate()
            : base()
        {

        }

        internal HisDiimTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_DIIM_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDiimTypeCheck checker = new HisDiimTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_DIIM_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.DIIM_TYPE_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisDiimTypeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDiimType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDiimType that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisDiimTypes.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_DIIM_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDiimTypeCheck checker = new HisDiimTypeCheck(param);
                List<HIS_DIIM_TYPE> listRaw = new List<HIS_DIIM_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.DIIM_TYPE_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisDiimTypeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDiimType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDiimType that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisDiimTypes.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisDiimTypes))
            {
                if (!DAOWorker.HisDiimTypeDAO.UpdateList(this.beforeUpdateHisDiimTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisDiimType that bai, can kiem tra lai." + LogUtil.TraceData("HisDiimTypes", this.beforeUpdateHisDiimTypes));
                }
				this.beforeUpdateHisDiimTypes = null;
            }
        }
    }
}
