using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineMedicine
{
    partial class HisMedicineMedicineCreate : BusinessBase
    {
		private List<HIS_MEDICINE_MEDICINE> recentHisMedicineMedicines = new List<HIS_MEDICINE_MEDICINE>();
		
        internal HisMedicineMedicineCreate()
            : base()
        {

        }

        internal HisMedicineMedicineCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEDICINE_MEDICINE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicineMedicineCheck checker = new HisMedicineMedicineCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisMedicineMedicineDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicineMedicine_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMedicineMedicine that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMedicineMedicines.Add(data);
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
		
		internal bool CreateList(List<HIS_MEDICINE_MEDICINE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicineMedicineCheck checker = new HisMedicineMedicineCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMedicineMedicineDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicineMedicine_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMedicineMedicine that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMedicineMedicines.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMedicineMedicines))
            {
                if (!DAOWorker.HisMedicineMedicineDAO.TruncateList(this.recentHisMedicineMedicines))
                {
                    LogSystem.Warn("Rollback du lieu HisMedicineMedicine that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMedicineMedicines", this.recentHisMedicineMedicines));
                }
				this.recentHisMedicineMedicines = null;
            }
        }
    }
}
