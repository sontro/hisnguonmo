using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBidMedicineType
{
    partial class HisBidMedicineTypeCreate : BusinessBase
    {
		private List<HIS_BID_MEDICINE_TYPE> recentHisBidMedicineTypes = new List<HIS_BID_MEDICINE_TYPE>();
		
        internal HisBidMedicineTypeCreate()
            : base()
        {

        }

        internal HisBidMedicineTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_BID_MEDICINE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBidMedicineTypeCheck checker = new HisBidMedicineTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisBidMedicineTypeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBidMedicineType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBidMedicineType that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisBidMedicineTypes.Add(data);
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

        internal bool CreateList(List<HIS_BID_MEDICINE_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBidMedicineTypeCheck checker = new HisBidMedicineTypeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisBidMedicineTypeDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBidMedicineType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBidMedicineType that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisBidMedicineTypes.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisBidMedicineTypes))
            {
                if (!new HisBidMedicineTypeTruncate(param).TruncateList(this.recentHisBidMedicineTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisBidMedicineType that bai, can kiem tra lai." + LogUtil.TraceData("recentHisBidMedicineTypes", this.recentHisBidMedicineTypes));
                }
            }
        }
    }
}
