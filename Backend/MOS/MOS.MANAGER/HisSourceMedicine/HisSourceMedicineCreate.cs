using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSourceMedicine
{
    partial class HisSourceMedicineCreate : BusinessBase
    {
		private List<HIS_SOURCE_MEDICINE> recentHisSourceMedicines = new List<HIS_SOURCE_MEDICINE>();
		
        internal HisSourceMedicineCreate()
            : base()
        {

        }

        internal HisSourceMedicineCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SOURCE_MEDICINE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSourceMedicineCheck checker = new HisSourceMedicineCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.SOURCE_MEDICINE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisSourceMedicineDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSourceMedicine_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSourceMedicine that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisSourceMedicines.Add(data);
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
		
		internal bool CreateList(List<HIS_SOURCE_MEDICINE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSourceMedicineCheck checker = new HisSourceMedicineCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.SOURCE_MEDICINE_CODE, null);
                }
                if (valid)
                {
                    if (!DAOWorker.HisSourceMedicineDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSourceMedicine_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSourceMedicine that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisSourceMedicines.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisSourceMedicines))
            {
                if (!DAOWorker.HisSourceMedicineDAO.TruncateList(this.recentHisSourceMedicines))
                {
                    LogSystem.Warn("Rollback du lieu HisSourceMedicine that bai, can kiem tra lai." + LogUtil.TraceData("recentHisSourceMedicines", this.recentHisSourceMedicines));
                }
				this.recentHisSourceMedicines = null;
            }
        }
    }
}
