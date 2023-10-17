using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAccidentCare
{
    partial class HisAccidentCareCreate : BusinessBase
    {
		private List<HIS_ACCIDENT_CARE> recentHisAccidentCares = new List<HIS_ACCIDENT_CARE>();
		
        internal HisAccidentCareCreate()
            : base()
        {

        }

        internal HisAccidentCareCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ACCIDENT_CARE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAccidentCareCheck checker = new HisAccidentCareCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.ACCIDENT_CARE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisAccidentCareDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAccidentCare_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAccidentCare that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisAccidentCares.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisAccidentCares))
            {
                if (!new HisAccidentCareTruncate(param).TruncateList(this.recentHisAccidentCares))
                {
                    LogSystem.Warn("Rollback du lieu HisAccidentCare that bai, can kiem tra lai." + LogUtil.TraceData("recentHisAccidentCares", this.recentHisAccidentCares));
                }
            }
        }
    }
}
