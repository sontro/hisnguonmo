using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisEmrForm
{
    partial class HisEmrFormUpdate : BusinessBase
    {
		private List<HIS_EMR_FORM> beforeUpdateHisEmrForms = new List<HIS_EMR_FORM>();
		
        internal HisEmrFormUpdate()
            : base()
        {

        }

        internal HisEmrFormUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EMR_FORM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEmrFormCheck checker = new HisEmrFormCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_EMR_FORM raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.EMR_FORM_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisEmrFormDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEmrForm_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisEmrForm that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisEmrForms.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_EMR_FORM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEmrFormCheck checker = new HisEmrFormCheck(param);
                List<HIS_EMR_FORM> listRaw = new List<HIS_EMR_FORM>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.EMR_FORM_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisEmrFormDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEmrForm_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisEmrForm that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisEmrForms.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisEmrForms))
            {
                if (!DAOWorker.HisEmrFormDAO.UpdateList(this.beforeUpdateHisEmrForms))
                {
                    LogSystem.Warn("Rollback du lieu HisEmrForm that bai, can kiem tra lai." + LogUtil.TraceData("HisEmrForms", this.beforeUpdateHisEmrForms));
                }
				this.beforeUpdateHisEmrForms = null;
            }
        }
    }
}
