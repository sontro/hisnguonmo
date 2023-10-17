using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSurgRemuDetail
{
    partial class HisSurgRemuDetailCreate : BusinessBase
    {
		private List<HIS_SURG_REMU_DETAIL> recentHisSurgRemuDetails = new List<HIS_SURG_REMU_DETAIL>();
		
        internal HisSurgRemuDetailCreate()
            : base()
        {

        }

        internal HisSurgRemuDetailCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SURG_REMU_DETAIL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSurgRemuDetailCheck checker = new HisSurgRemuDetailCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsNotExist(data);
                if (valid)
                {
					if (!DAOWorker.HisSurgRemuDetailDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSurgRemuDetail_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSurgRemuDetail that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisSurgRemuDetails.Add(data);
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
		
		internal bool CreateList(List<HIS_SURG_REMU_DETAIL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSurgRemuDetailCheck checker = new HisSurgRemuDetailCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.IsNotExist(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisSurgRemuDetailDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSurgRemuDetail_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSurgRemuDetail that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisSurgRemuDetails.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisSurgRemuDetails))
            {
                if (!DAOWorker.HisSurgRemuDetailDAO.TruncateList(this.recentHisSurgRemuDetails))
                {
                    LogSystem.Warn("Rollback du lieu HisSurgRemuDetail that bai, can kiem tra lai." + LogUtil.TraceData("recentHisSurgRemuDetails", this.recentHisSurgRemuDetails));
                }
				this.recentHisSurgRemuDetails = null;
            }
        }
    }
}
