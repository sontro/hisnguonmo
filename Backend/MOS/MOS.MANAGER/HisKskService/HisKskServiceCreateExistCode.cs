using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskService
{
    partial class HisKskServiceCreate : BusinessBase
    {
		private List<HIS_KSK_SERVICE> recentHisKskServices = new List<HIS_KSK_SERVICE>();
		
        internal HisKskServiceCreate()
            : base()
        {

        }

        internal HisKskServiceCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_KSK_SERVICE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskServiceCheck checker = new HisKskServiceCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.KSK_SERVICE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisKskServiceDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskService_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisKskService that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisKskServices.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisKskServices))
            {
                if (!DAOWorker.HisKskServiceDAO.TruncateList(this.recentHisKskServices))
                {
                    LogSystem.Warn("Rollback du lieu HisKskService that bai, can kiem tra lai." + LogUtil.TraceData("recentHisKskServices", this.recentHisKskServices));
                }
				this.recentHisKskServices = null;
            }
        }
    }
}
