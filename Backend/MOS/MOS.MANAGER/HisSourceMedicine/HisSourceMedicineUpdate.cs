using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSourceMedicine
{
    partial class HisSourceMedicineUpdate : BusinessBase
    {
		private List<HIS_SOURCE_MEDICINE> beforeUpdateHisSourceMedicines = new List<HIS_SOURCE_MEDICINE>();
		
        internal HisSourceMedicineUpdate()
            : base()
        {

        }

        internal HisSourceMedicineUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SOURCE_MEDICINE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSourceMedicineCheck checker = new HisSourceMedicineCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SOURCE_MEDICINE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.SOURCE_MEDICINE_CODE, data.ID);
                if (valid)
                {                    
					if (!DAOWorker.HisSourceMedicineDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSourceMedicine_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSourceMedicine that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisSourceMedicines.Add(raw);
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

        internal bool UpdateList(List<HIS_SOURCE_MEDICINE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSourceMedicineCheck checker = new HisSourceMedicineCheck(param);
                List<HIS_SOURCE_MEDICINE> listRaw = new List<HIS_SOURCE_MEDICINE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.SOURCE_MEDICINE_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisSourceMedicineDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSourceMedicine_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSourceMedicine that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisSourceMedicines.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisSourceMedicines))
            {
                if (!DAOWorker.HisSourceMedicineDAO.UpdateList(this.beforeUpdateHisSourceMedicines))
                {
                    LogSystem.Warn("Rollback du lieu HisSourceMedicine that bai, can kiem tra lai." + LogUtil.TraceData("HisSourceMedicines", this.beforeUpdateHisSourceMedicines));
                }
				this.beforeUpdateHisSourceMedicines = null;
            }
        }
    }
}
