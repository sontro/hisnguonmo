using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTreatmentEndTypeExt
{
    partial class HisTreatmentEndTypeExtUpdate : BusinessBase
    {
		private List<HIS_TREATMENT_END_TYPE_EXT> beforeUpdateHisTreatmentEndTypeExts = new List<HIS_TREATMENT_END_TYPE_EXT>();
		
        internal HisTreatmentEndTypeExtUpdate()
            : base()
        {

        }

        internal HisTreatmentEndTypeExtUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_TREATMENT_END_TYPE_EXT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentEndTypeExtCheck checker = new HisTreatmentEndTypeExtCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_TREATMENT_END_TYPE_EXT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.TREATMENT_END_TYPE_EXT_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisTreatmentEndTypeExtDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatmentEndTypeExt_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTreatmentEndTypeExt that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisTreatmentEndTypeExts.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_TREATMENT_END_TYPE_EXT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTreatmentEndTypeExtCheck checker = new HisTreatmentEndTypeExtCheck(param);
                List<HIS_TREATMENT_END_TYPE_EXT> listRaw = new List<HIS_TREATMENT_END_TYPE_EXT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.TREATMENT_END_TYPE_EXT_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisTreatmentEndTypeExtDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatmentEndTypeExt_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTreatmentEndTypeExt that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisTreatmentEndTypeExts.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisTreatmentEndTypeExts))
            {
                if (!DAOWorker.HisTreatmentEndTypeExtDAO.UpdateList(this.beforeUpdateHisTreatmentEndTypeExts))
                {
                    LogSystem.Warn("Rollback du lieu HisTreatmentEndTypeExt that bai, can kiem tra lai." + LogUtil.TraceData("HisTreatmentEndTypeExts", this.beforeUpdateHisTreatmentEndTypeExts));
                }
				this.beforeUpdateHisTreatmentEndTypeExts = null;
            }
        }
    }
}
