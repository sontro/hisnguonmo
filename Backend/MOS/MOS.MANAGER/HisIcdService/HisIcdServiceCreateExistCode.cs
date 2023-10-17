using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisIcdService
{
    partial class HisIcdServiceCreate : BusinessBase
    {
		private List<HIS_ICD_SERVICE> recentHisIcdServices = new List<HIS_ICD_SERVICE>();
		
        internal HisIcdServiceCreate()
            : base()
        {

        }

        internal HisIcdServiceCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ICD_SERVICE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisIcdServiceCheck checker = new HisIcdServiceCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.ICD_SERVICE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisIcdServiceDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisIcdService_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisIcdService that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisIcdServices.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisIcdServices))
            {
                if (!DAOWorker.HisIcdServiceDAO.TruncateList(this.recentHisIcdServices))
                {
                    LogSystem.Warn("Rollback du lieu HisIcdService that bai, can kiem tra lai." + LogUtil.TraceData("recentHisIcdServices", this.recentHisIcdServices));
                }
				this.recentHisIcdServices = null;
            }
        }
    }
}
