using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineBean
{
    class HisMedicineBeanCreate : BusinessBase
    {
        private List<HIS_MEDICINE_BEAN> recentHisMedicineBeans = new List<HIS_MEDICINE_BEAN>();

        internal HisMedicineBeanCreate()
            : base()
        {

        }

        internal HisMedicineBeanCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEDICINE_BEAN data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicineBeanCheck checker = new HisMedicineBeanCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisMedicineBeanDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicineBean_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMedicineBean that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMedicineBeans.Add(data);
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

        internal bool CreateList(List<HIS_MEDICINE_BEAN> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicineBeanCheck checker = new HisMedicineBeanCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMedicineBeanDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicineBean_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMedicineBean that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMedicineBeans.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMedicineBeans))
            {
                if (!new HisMedicineBeanTruncate(param).TruncateList(this.recentHisMedicineBeans))
                {
                    LogSystem.Warn("Rollback du lieu HisMedicineBean that bai, can kiem tra lai." + LogUtil.TraceData("HisMedicineBeans", this.recentHisMedicineBeans));
                }
            }
        }
    }
}
