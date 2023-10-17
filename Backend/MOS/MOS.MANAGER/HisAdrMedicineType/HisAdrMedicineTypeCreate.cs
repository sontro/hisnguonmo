using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAdrMedicineType
{
    partial class HisAdrMedicineTypeCreate : BusinessBase
    {
		private List<HIS_ADR_MEDICINE_TYPE> recentHisAdrMedicineTypes = new List<HIS_ADR_MEDICINE_TYPE>();
		
        internal HisAdrMedicineTypeCreate()
            : base()
        {

        }

        internal HisAdrMedicineTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ADR_MEDICINE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAdrMedicineTypeCheck checker = new HisAdrMedicineTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisAdrMedicineTypeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAdrMedicineType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAdrMedicineType that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisAdrMedicineTypes.Add(data);
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
		
		internal bool CreateList(List<HIS_ADR_MEDICINE_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAdrMedicineTypeCheck checker = new HisAdrMedicineTypeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisAdrMedicineTypeDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAdrMedicineType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAdrMedicineType that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisAdrMedicineTypes.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisAdrMedicineTypes))
            {
                if (!DAOWorker.HisAdrMedicineTypeDAO.TruncateList(this.recentHisAdrMedicineTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisAdrMedicineType that bai, can kiem tra lai." + LogUtil.TraceData("recentHisAdrMedicineTypes", this.recentHisAdrMedicineTypes));
                }
				this.recentHisAdrMedicineTypes = null;
            }
        }
    }
}
