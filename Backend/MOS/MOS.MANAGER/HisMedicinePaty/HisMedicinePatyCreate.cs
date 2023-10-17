using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicinePaty
{
    class HisMedicinePatyCreate : BusinessBase
    {
        private List<HIS_MEDICINE_PATY> recentHisMedicinePatyDTOs;

        internal HisMedicinePatyCreate()
            : base()
        {

        }

        internal HisMedicinePatyCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEDICINE_PATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicinePatyCheck checker = new HisMedicinePatyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsNotExists(data);
                if (valid)
                {
                    if (!DAOWorker.HisMedicinePatyDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicinePaty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMedicinePaty that bai." + LogUtil.TraceData("data", data));
                    }
                    if (this.recentHisMedicinePatyDTOs == null)
                    {
                        this.recentHisMedicinePatyDTOs = new List<HIS_MEDICINE_PATY>();
                    }
                    this.recentHisMedicinePatyDTOs.Add(data);
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

        internal bool CreateList(List<HIS_MEDICINE_PATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicinePatyCheck checker = new HisMedicinePatyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.IsNotExists(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMedicinePatyDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicinePaty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMedicinePaty that bai." + LogUtil.TraceData("listData", listData));
                    }
                    if (this.recentHisMedicinePatyDTOs == null)
                    {
                        this.recentHisMedicinePatyDTOs = new List<HIS_MEDICINE_PATY>();
                    }
                    this.recentHisMedicinePatyDTOs.AddRange(listData);
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
            if (this.recentHisMedicinePatyDTOs != null)
            {
                if (!new HisMedicinePatyTruncate(param).TruncateList(this.recentHisMedicinePatyDTOs))
                {
                    LogSystem.Warn("Rollback du lieu HisMedicinePaty that bai, can kiem tra lai." + LogUtil.TraceData("HisMedicinePatyDTOs", this.recentHisMedicinePatyDTOs));
                }
            }
        }
    }
}
