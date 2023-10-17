using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMixedMedicine
{
    partial class HisMixedMedicineCreate : BusinessBase
    {
		private List<HIS_MIXED_MEDICINE> recentHisMixedMedicines = new List<HIS_MIXED_MEDICINE>();
		
        internal HisMixedMedicineCreate()
            : base()
        {

        }

        internal HisMixedMedicineCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MIXED_MEDICINE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMixedMedicineCheck checker = new HisMixedMedicineCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisMixedMedicineDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMixedMedicine_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMixedMedicine that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMixedMedicines.Add(data);
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
		
		internal bool CreateList(List<HIS_MIXED_MEDICINE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMixedMedicineCheck checker = new HisMixedMedicineCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMixedMedicineDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMixedMedicine_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMixedMedicine that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMixedMedicines.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMixedMedicines))
            {
                if (!DAOWorker.HisMixedMedicineDAO.TruncateList(this.recentHisMixedMedicines))
                {
                    LogSystem.Warn("Rollback du lieu HisMixedMedicine that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMixedMedicines", this.recentHisMixedMedicines));
                }
				this.recentHisMixedMedicines = null;
            }
        }
    }
}
