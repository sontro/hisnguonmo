using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDosageForm
{
    partial class HisDosageFormUpdate : BusinessBase
    {
		private List<HIS_DOSAGE_FORM> beforeUpdateHisDosageForms = new List<HIS_DOSAGE_FORM>();
		
        internal HisDosageFormUpdate()
            : base()
        {

        }

        internal HisDosageFormUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_DOSAGE_FORM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDosageFormCheck checker = new HisDosageFormCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_DOSAGE_FORM raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.DOSAGE_FORM_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisDosageFormDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDosageForm_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDosageForm that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisDosageForms.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_DOSAGE_FORM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDosageFormCheck checker = new HisDosageFormCheck(param);
                List<HIS_DOSAGE_FORM> listRaw = new List<HIS_DOSAGE_FORM>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.DOSAGE_FORM_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisDosageFormDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDosageForm_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDosageForm that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisDosageForms.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisDosageForms))
            {
                if (!DAOWorker.HisDosageFormDAO.UpdateList(this.beforeUpdateHisDosageForms))
                {
                    LogSystem.Warn("Rollback du lieu HisDosageForm that bai, can kiem tra lai." + LogUtil.TraceData("HisDosageForms", this.beforeUpdateHisDosageForms));
                }
				this.beforeUpdateHisDosageForms = null;
            }
        }
    }
}
