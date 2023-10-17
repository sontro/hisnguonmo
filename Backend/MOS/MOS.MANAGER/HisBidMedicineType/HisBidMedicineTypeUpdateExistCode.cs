using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBidMedicineType
{
    partial class HisBidMedicineTypeUpdate : BusinessBase
    {
		private List<HIS_BID_MEDICINE_TYPE> beforeUpdateHisBidMedicineTypes = new List<HIS_BID_MEDICINE_TYPE>();
		
        internal HisBidMedicineTypeUpdate()
            : base()
        {

        }

        internal HisBidMedicineTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_BID_MEDICINE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBidMedicineTypeCheck checker = new HisBidMedicineTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_BID_MEDICINE_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.BID_MEDICINE_TYPE_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisBidMedicineTypes.Add(raw);
					if (!DAOWorker.HisBidMedicineTypeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBidMedicineType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBidMedicineType that bai." + LogUtil.TraceData("data", data));
                    }
                    
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

        internal bool UpdateList(List<HIS_BID_MEDICINE_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBidMedicineTypeCheck checker = new HisBidMedicineTypeCheck(param);
                List<HIS_BID_MEDICINE_TYPE> listRaw = new List<HIS_BID_MEDICINE_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.BID_MEDICINE_TYPE_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisBidMedicineTypes.AddRange(listRaw);
					if (!DAOWorker.HisBidMedicineTypeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBidMedicineType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBidMedicineType that bai." + LogUtil.TraceData("listData", listData));
                    }
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisBidMedicineTypes))
            {
                if (!new HisBidMedicineTypeUpdate(param).UpdateList(this.beforeUpdateHisBidMedicineTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisBidMedicineType that bai, can kiem tra lai." + LogUtil.TraceData("HisBidMedicineTypes", this.beforeUpdateHisBidMedicineTypes));
                }
            }
        }
    }
}
