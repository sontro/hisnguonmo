using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineInteractive
{
    partial class HisMedicineInteractiveCreate : BusinessBase
    {
		private List<HIS_MEDICINE_INTERACTIVE> recentHisMedicineInteractives = new List<HIS_MEDICINE_INTERACTIVE>();
		
        internal HisMedicineInteractiveCreate()
            : base()
        {

        }

        internal HisMedicineInteractiveCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEDICINE_INTERACTIVE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicineInteractiveCheck checker = new HisMedicineInteractiveCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisMedicineInteractiveDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicineInteractive_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMedicineInteractive that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMedicineInteractives.Add(data);
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
		
		internal bool CreateList(List<HIS_MEDICINE_INTERACTIVE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicineInteractiveCheck checker = new HisMedicineInteractiveCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMedicineInteractiveDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicineInteractive_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMedicineInteractive that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMedicineInteractives.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMedicineInteractives))
            {
                if (!DAOWorker.HisMedicineInteractiveDAO.TruncateList(this.recentHisMedicineInteractives))
                {
                    LogSystem.Warn("Rollback du lieu HisMedicineInteractive that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMedicineInteractives", this.recentHisMedicineInteractives));
                }
				this.recentHisMedicineInteractives = null;
            }
        }
    }
}
