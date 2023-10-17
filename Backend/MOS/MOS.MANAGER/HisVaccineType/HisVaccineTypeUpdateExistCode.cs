using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisVaccineType
{
    partial class HisVaccineTypeUpdate : BusinessBase
    {
		private List<HIS_VACCINE_TYPE> beforeUpdateHisVaccineTypes = new List<HIS_VACCINE_TYPE>();
		
        internal HisVaccineTypeUpdate()
            : base()
        {

        }

        internal HisVaccineTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_VACCINE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccineTypeCheck checker = new HisVaccineTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_VACCINE_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.VACCINE_TYPE_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisVaccineTypeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccineType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisVaccineType that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisVaccineTypes.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_VACCINE_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVaccineTypeCheck checker = new HisVaccineTypeCheck(param);
                List<HIS_VACCINE_TYPE> listRaw = new List<HIS_VACCINE_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.VACCINE_TYPE_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisVaccineTypeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccineType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisVaccineType that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisVaccineTypes.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisVaccineTypes))
            {
                if (!DAOWorker.HisVaccineTypeDAO.UpdateList(this.beforeUpdateHisVaccineTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisVaccineType that bai, can kiem tra lai." + LogUtil.TraceData("HisVaccineTypes", this.beforeUpdateHisVaccineTypes));
                }
				this.beforeUpdateHisVaccineTypes = null;
            }
        }
    }
}
