using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMedicineMedicine
{
    partial class HisMedicineMedicineUpdate : BusinessBase
    {
		private List<HIS_MEDICINE_MEDICINE> beforeUpdateHisMedicineMedicines = new List<HIS_MEDICINE_MEDICINE>();
		
        internal HisMedicineMedicineUpdate()
            : base()
        {

        }

        internal HisMedicineMedicineUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEDICINE_MEDICINE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicineMedicineCheck checker = new HisMedicineMedicineCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MEDICINE_MEDICINE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisMedicineMedicineDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicineMedicine_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMedicineMedicine that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisMedicineMedicines.Add(raw);
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

        internal bool UpdateList(List<HIS_MEDICINE_MEDICINE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicineMedicineCheck checker = new HisMedicineMedicineCheck(param);
                List<HIS_MEDICINE_MEDICINE> listRaw = new List<HIS_MEDICINE_MEDICINE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisMedicineMedicineDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicineMedicine_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMedicineMedicine that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisMedicineMedicines.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMedicineMedicines))
            {
                if (!DAOWorker.HisMedicineMedicineDAO.UpdateList(this.beforeUpdateHisMedicineMedicines))
                {
                    LogSystem.Warn("Rollback du lieu HisMedicineMedicine that bai, can kiem tra lai." + LogUtil.TraceData("HisMedicineMedicines", this.beforeUpdateHisMedicineMedicines));
                }
				this.beforeUpdateHisMedicineMedicines = null;
            }
        }
    }
}
