using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntibioticRequest
{
    partial class HisAntibioticRequestCreate : BusinessBase
    {
		private List<HIS_ANTIBIOTIC_REQUEST> recentHisAntibioticRequests = new List<HIS_ANTIBIOTIC_REQUEST>();
		
        internal HisAntibioticRequestCreate()
            : base()
        {

        }

        internal HisAntibioticRequestCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ANTIBIOTIC_REQUEST data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAntibioticRequestCheck checker = new HisAntibioticRequestCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.ANTIBIOTIC_REQUEST_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisAntibioticRequestDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAntibioticRequest_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAntibioticRequest that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisAntibioticRequests.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisAntibioticRequests))
            {
                if (!DAOWorker.HisAntibioticRequestDAO.TruncateList(this.recentHisAntibioticRequests))
                {
                    LogSystem.Warn("Rollback du lieu HisAntibioticRequest that bai, can kiem tra lai." + LogUtil.TraceData("recentHisAntibioticRequests", this.recentHisAntibioticRequests));
                }
				this.recentHisAntibioticRequests = null;
            }
        }
    }
}
