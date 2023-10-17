using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDosageForm
{
    partial class HisDosageFormCreate : BusinessBase
    {
		private List<HIS_DOSAGE_FORM> recentHisDosageForms = new List<HIS_DOSAGE_FORM>();
		
        internal HisDosageFormCreate()
            : base()
        {

        }

        internal HisDosageFormCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_DOSAGE_FORM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDosageFormCheck checker = new HisDosageFormCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.DOSAGE_FORM_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisDosageFormDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDosageForm_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisDosageForm that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisDosageForms.Add(data);
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
		
		internal bool CreateList(List<HIS_DOSAGE_FORM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDosageFormCheck checker = new HisDosageFormCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.DOSAGE_FORM_CODE, null);
                }
                if (valid)
                {
                    if (!DAOWorker.HisDosageFormDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDosageForm_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisDosageForm that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisDosageForms.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisDosageForms))
            {
                if (!DAOWorker.HisDosageFormDAO.TruncateList(this.recentHisDosageForms))
                {
                    LogSystem.Warn("Rollback du lieu HisDosageForm that bai, can kiem tra lai." + LogUtil.TraceData("recentHisDosageForms", this.recentHisDosageForms));
                }
				this.recentHisDosageForms = null;
            }
        }
    }
}
