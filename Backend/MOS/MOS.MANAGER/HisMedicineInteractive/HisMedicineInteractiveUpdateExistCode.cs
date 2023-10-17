using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMedicineInteractive
{
    partial class HisMedicineInteractiveUpdate : BusinessBase
    {
		private List<HIS_MEDICINE_INTERACTIVE> beforeUpdateHisMedicineInteractives = new List<HIS_MEDICINE_INTERACTIVE>();
		
        internal HisMedicineInteractiveUpdate()
            : base()
        {

        }

        internal HisMedicineInteractiveUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEDICINE_INTERACTIVE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicineInteractiveCheck checker = new HisMedicineInteractiveCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MEDICINE_INTERACTIVE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.MEDICINE_INTERACTIVE_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisMedicineInteractiveDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicineInteractive_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMedicineInteractive that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisMedicineInteractives.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_MEDICINE_INTERACTIVE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicineInteractiveCheck checker = new HisMedicineInteractiveCheck(param);
                List<HIS_MEDICINE_INTERACTIVE> listRaw = new List<HIS_MEDICINE_INTERACTIVE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.MEDICINE_INTERACTIVE_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisMedicineInteractiveDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicineInteractive_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMedicineInteractive that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisMedicineInteractives.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMedicineInteractives))
            {
                if (!DAOWorker.HisMedicineInteractiveDAO.UpdateList(this.beforeUpdateHisMedicineInteractives))
                {
                    LogSystem.Warn("Rollback du lieu HisMedicineInteractive that bai, can kiem tra lai." + LogUtil.TraceData("HisMedicineInteractives", this.beforeUpdateHisMedicineInteractives));
                }
				this.beforeUpdateHisMedicineInteractives = null;
            }
        }
    }
}
