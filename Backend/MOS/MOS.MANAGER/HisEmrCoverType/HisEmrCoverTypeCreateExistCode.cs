using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmrCoverType
{
    partial class HisEmrCoverTypeCreate : BusinessBase
    {
		private List<HIS_EMR_COVER_TYPE> recentHisEmrCoverTypes = new List<HIS_EMR_COVER_TYPE>();
		
        internal HisEmrCoverTypeCreate()
            : base()
        {

        }

        internal HisEmrCoverTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EMR_COVER_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEmrCoverTypeCheck checker = new HisEmrCoverTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.EMR_COVER_TYPE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisEmrCoverTypeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEmrCoverType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisEmrCoverType that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisEmrCoverTypes.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisEmrCoverTypes))
            {
                if (!DAOWorker.HisEmrCoverTypeDAO.TruncateList(this.recentHisEmrCoverTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisEmrCoverType that bai, can kiem tra lai." + LogUtil.TraceData("recentHisEmrCoverTypes", this.recentHisEmrCoverTypes));
                }
				this.recentHisEmrCoverTypes = null;
            }
        }
    }
}
