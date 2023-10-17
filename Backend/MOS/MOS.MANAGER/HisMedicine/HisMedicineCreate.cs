using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicine
{
    class HisMedicineCreate : BusinessBase
    {
        private List<HIS_MEDICINE> recentHisMedicines = new List<HIS_MEDICINE>();

        internal HisMedicineCreate()
            : base()
        {

        }

        internal HisMedicineCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEDICINE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicineCheck checker = new HisMedicineCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisMedicineDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicine_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMedicine that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMedicines.Add(data);
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

        internal bool CreateList(List<HIS_MEDICINE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicineCheck checker = new HisMedicineCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMedicineDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicine_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMedicine that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMedicines.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMedicines))
            {
                if (!DAOWorker.HisMedicineDAO.TruncateList(this.recentHisMedicines))
                {
                    LogSystem.Warn("Rollback du lieu HisMedicine that bai, can kiem tra lai." + LogUtil.TraceData("HisMedicines", this.recentHisMedicines));
                }
            }
        }
    }
}
