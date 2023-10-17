using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMedicineService
{
    partial class HisMedicineServiceUpdate : BusinessBase
    {
		private List<HIS_MEDICINE_SERVICE> beforeUpdateHisMedicineServices = new List<HIS_MEDICINE_SERVICE>();
		
        internal HisMedicineServiceUpdate()
            : base()
        {

        }

        internal HisMedicineServiceUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEDICINE_SERVICE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicineServiceCheck checker = new HisMedicineServiceCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MEDICINE_SERVICE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisMedicineServiceDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicineService_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMedicineService that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisMedicineServices.Add(raw);
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

        internal bool UpdateList(List<HIS_MEDICINE_SERVICE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicineServiceCheck checker = new HisMedicineServiceCheck(param);
                List<HIS_MEDICINE_SERVICE> listRaw = new List<HIS_MEDICINE_SERVICE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                valid = valid && checker.IsValidDataTypeAndTestIndexId(listData);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisMedicineServiceDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicineService_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMedicineService that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisMedicineServices.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMedicineServices))
            {
                if (!DAOWorker.HisMedicineServiceDAO.UpdateList(this.beforeUpdateHisMedicineServices))
                {
                    LogSystem.Warn("Rollback du lieu HisMedicineService that bai, can kiem tra lai." + LogUtil.TraceData("HisMedicineServices", this.beforeUpdateHisMedicineServices));
                }
				this.beforeUpdateHisMedicineServices = null;
            }
        }
    }
}
