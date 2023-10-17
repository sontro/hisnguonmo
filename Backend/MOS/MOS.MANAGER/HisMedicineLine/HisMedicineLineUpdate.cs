using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMedicineLine
{
    partial class HisMedicineLineUpdate : BusinessBase
    {
		private List<HIS_MEDICINE_LINE> beforeUpdateHisMedicineLineDTOs = new List<HIS_MEDICINE_LINE>();
		
        internal HisMedicineLineUpdate()
            : base()
        {

        }

        internal HisMedicineLineUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEDICINE_LINE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicineLineCheck checker = new HisMedicineLineCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MEDICINE_LINE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.MEDICINE_LINE_CODE, data.ID);
                valid = valid && this.IsAllowUpdate(raw, data);
                if (valid)
                {
					this.beforeUpdateHisMedicineLineDTOs.Add(raw);
					if (!DAOWorker.HisMedicineLineDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicineLine_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMedicineLine that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_MEDICINE_LINE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicineLineCheck checker = new HisMedicineLineCheck(param);
                List<HIS_MEDICINE_LINE> listRaw = new List<HIS_MEDICINE_LINE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    HIS_MEDICINE_LINE raw = listRaw.Where(o => o.ID == data.ID).FirstOrDefault();
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.MEDICINE_LINE_CODE, data.ID);
                    valid = valid && this.IsAllowUpdate(raw, data);
                }
                if (valid)
                {
					this.beforeUpdateHisMedicineLineDTOs.AddRange(listRaw);
					if (!DAOWorker.HisMedicineLineDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicineLine_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMedicineLine that bai." + LogUtil.TraceData("listData", listData));
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

        private bool IsAllowUpdate(HIS_MEDICINE_LINE oldData, HIS_MEDICINE_LINE newData)
        {
            //Ko cho sua code, name
            return oldData != null
                && newData != null
                && oldData.MEDICINE_LINE_CODE == newData.MEDICINE_LINE_CODE
                && oldData.MEDICINE_LINE_NAME == newData.MEDICINE_LINE_NAME;
        }
		
		internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.beforeUpdateHisMedicineLineDTOs))
            {
                if (!DAOWorker.HisMedicineLineDAO.UpdateList(this.beforeUpdateHisMedicineLineDTOs))
                {
                    LogSystem.Warn("Rollback du lieu HisMedicineLine that bai, can kiem tra lai." + LogUtil.TraceData("HisMedicineLineDTOs", this.beforeUpdateHisMedicineLineDTOs));
                }
            }
        }
    }
}
