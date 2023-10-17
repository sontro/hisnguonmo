using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMixedMedicine
{
    partial class HisMixedMedicineUpdate : BusinessBase
    {
		private List<HIS_MIXED_MEDICINE> beforeUpdateHisMixedMedicines = new List<HIS_MIXED_MEDICINE>();
		
        internal HisMixedMedicineUpdate()
            : base()
        {

        }

        internal HisMixedMedicineUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MIXED_MEDICINE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMixedMedicineCheck checker = new HisMixedMedicineCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MIXED_MEDICINE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisMixedMedicineDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMixedMedicine_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMixedMedicine that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisMixedMedicines.Add(raw);
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

        internal bool UpdateList(List<HIS_MIXED_MEDICINE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMixedMedicineCheck checker = new HisMixedMedicineCheck(param);
                List<HIS_MIXED_MEDICINE> listRaw = new List<HIS_MIXED_MEDICINE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisMixedMedicineDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMixedMedicine_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMixedMedicine that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisMixedMedicines.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMixedMedicines))
            {
                if (!DAOWorker.HisMixedMedicineDAO.UpdateList(this.beforeUpdateHisMixedMedicines))
                {
                    LogSystem.Warn("Rollback du lieu HisMixedMedicine that bai, can kiem tra lai." + LogUtil.TraceData("HisMixedMedicines", this.beforeUpdateHisMixedMedicines));
                }
				this.beforeUpdateHisMixedMedicines = null;
            }
        }
    }
}
