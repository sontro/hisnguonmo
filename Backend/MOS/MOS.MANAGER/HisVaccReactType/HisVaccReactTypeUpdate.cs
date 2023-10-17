using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisVaccReactType
{
    partial class HisVaccReactTypeUpdate : BusinessBase
    {
		private List<HIS_VACC_REACT_TYPE> beforeUpdateHisVaccReactTypes = new List<HIS_VACC_REACT_TYPE>();
		
        internal HisVaccReactTypeUpdate()
            : base()
        {

        }

        internal HisVaccReactTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_VACC_REACT_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccReactTypeCheck checker = new HisVaccReactTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_VACC_REACT_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisVaccReactTypeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccReactType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisVaccReactType that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisVaccReactTypes.Add(raw);
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

        internal bool UpdateList(List<HIS_VACC_REACT_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVaccReactTypeCheck checker = new HisVaccReactTypeCheck(param);
                List<HIS_VACC_REACT_TYPE> listRaw = new List<HIS_VACC_REACT_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisVaccReactTypeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccReactType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisVaccReactType that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisVaccReactTypes.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisVaccReactTypes))
            {
                if (!DAOWorker.HisVaccReactTypeDAO.UpdateList(this.beforeUpdateHisVaccReactTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisVaccReactType that bai, can kiem tra lai." + LogUtil.TraceData("HisVaccReactTypes", this.beforeUpdateHisVaccReactTypes));
                }
				this.beforeUpdateHisVaccReactTypes = null;
            }
        }
    }
}
