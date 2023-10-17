using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAdrMedicineType
{
    partial class HisAdrMedicineTypeUpdate : BusinessBase
    {
		private List<HIS_ADR_MEDICINE_TYPE> beforeUpdateHisAdrMedicineTypes = new List<HIS_ADR_MEDICINE_TYPE>();
		
        internal HisAdrMedicineTypeUpdate()
            : base()
        {

        }

        internal HisAdrMedicineTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_ADR_MEDICINE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAdrMedicineTypeCheck checker = new HisAdrMedicineTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_ADR_MEDICINE_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisAdrMedicineTypeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAdrMedicineType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAdrMedicineType that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisAdrMedicineTypes.Add(raw);
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

        internal bool UpdateList(List<HIS_ADR_MEDICINE_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAdrMedicineTypeCheck checker = new HisAdrMedicineTypeCheck(param);
                List<HIS_ADR_MEDICINE_TYPE> listRaw = new List<HIS_ADR_MEDICINE_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisAdrMedicineTypeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAdrMedicineType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAdrMedicineType that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisAdrMedicineTypes.AddRange(listRaw);
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

        internal bool UpdateList(List<HIS_ADR_MEDICINE_TYPE> listData, List<HIS_ADR_MEDICINE_TYPE> befores)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAdrMedicineTypeCheck checker = new HisAdrMedicineTypeCheck(param);
                valid = valid && checker.IsUnLock(befores);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisAdrMedicineTypeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAdrMedicineType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAdrMedicineType that bai." + LogUtil.TraceData("listData", listData));
                    }

                    this.beforeUpdateHisAdrMedicineTypes.AddRange(befores);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisAdrMedicineTypes))
            {
                if (!DAOWorker.HisAdrMedicineTypeDAO.UpdateList(this.beforeUpdateHisAdrMedicineTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisAdrMedicineType that bai, can kiem tra lai." + LogUtil.TraceData("HisAdrMedicineTypes", this.beforeUpdateHisAdrMedicineTypes));
                }
				this.beforeUpdateHisAdrMedicineTypes = null;
            }
        }
    }
}
