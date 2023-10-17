using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTreatmentType
{
    partial class HisTreatmentTypeUpdate : BusinessBase
    {
		private List<HIS_TREATMENT_TYPE> beforeUpdateHisTreatmentTypeDTOs = new List<HIS_TREATMENT_TYPE>();
		
        internal HisTreatmentTypeUpdate()
            : base()
        {

        }

        internal HisTreatmentTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_TREATMENT_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentTypeCheck checker = new HisTreatmentTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_TREATMENT_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.TREATMENT_TYPE_CODE, data.ID);
                if (valid)
                {
					this.beforeUpdateHisTreatmentTypeDTOs.Add(raw);
					if (!DAOWorker.HisTreatmentTypeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatmentType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTreatmentType that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_TREATMENT_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTreatmentTypeCheck checker = new HisTreatmentTypeCheck(param);
                List<HIS_TREATMENT_TYPE> listRaw = new List<HIS_TREATMENT_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.TREATMENT_TYPE_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisTreatmentTypeDTOs.AddRange(listRaw);
					if (!DAOWorker.HisTreatmentTypeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatmentType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTreatmentType that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisTreatmentTypeDTOs))
            {
                if (!new HisTreatmentTypeUpdate(param).UpdateList(this.beforeUpdateHisTreatmentTypeDTOs))
                {
                    LogSystem.Warn("Rollback du lieu HisTreatmentType that bai, can kiem tra lai." + LogUtil.TraceData("HisTreatmentTypeDTOs", this.beforeUpdateHisTreatmentTypeDTOs));
                }
            }
        }
    }
}
