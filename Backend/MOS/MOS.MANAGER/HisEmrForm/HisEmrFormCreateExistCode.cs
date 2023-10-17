using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmrForm
{
    partial class HisEmrFormCreate : BusinessBase
    {
		private List<HIS_EMR_FORM> recentHisEmrForms = new List<HIS_EMR_FORM>();
		
        internal HisEmrFormCreate()
            : base()
        {

        }

        internal HisEmrFormCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EMR_FORM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEmrFormCheck checker = new HisEmrFormCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.EMR_FORM_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisEmrFormDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEmrForm_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisEmrForm that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisEmrForms.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisEmrForms))
            {
                if (!DAOWorker.HisEmrFormDAO.TruncateList(this.recentHisEmrForms))
                {
                    LogSystem.Warn("Rollback du lieu HisEmrForm that bai, can kiem tra lai." + LogUtil.TraceData("recentHisEmrForms", this.recentHisEmrForms));
                }
				this.recentHisEmrForms = null;
            }
        }
    }
}
