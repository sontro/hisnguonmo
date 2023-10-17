using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineService
{
    partial class HisMedicineServiceCreate : BusinessBase
    {
		private List<HIS_MEDICINE_SERVICE> recentHisMedicineServices = new List<HIS_MEDICINE_SERVICE>();
		
        internal HisMedicineServiceCreate()
            : base()
        {

        }

        internal HisMedicineServiceCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEDICINE_SERVICE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicineServiceCheck checker = new HisMedicineServiceCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.MEDICINE_SERVICE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisMedicineServiceDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicineService_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMedicineService that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMedicineServices.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisMedicineServices))
            {
                if (!DAOWorker.HisMedicineServiceDAO.TruncateList(this.recentHisMedicineServices))
                {
                    LogSystem.Warn("Rollback du lieu HisMedicineService that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMedicineServices", this.recentHisMedicineServices));
                }
				this.recentHisMedicineServices = null;
            }
        }
    }
}
