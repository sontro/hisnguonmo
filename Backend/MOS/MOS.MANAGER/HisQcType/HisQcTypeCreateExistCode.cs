using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisQcType
{
    partial class HisQcTypeCreate : BusinessBase
    {
		private List<HIS_QC_TYPE> recentHisQcTypes = new List<HIS_QC_TYPE>();
		
        internal HisQcTypeCreate()
            : base()
        {

        }

        internal HisQcTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_QC_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisQcTypeCheck checker = new HisQcTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.QC_TYPE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisQcTypeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisQcType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisQcType that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisQcTypes.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisQcTypes))
            {
                if (!DAOWorker.HisQcTypeDAO.TruncateList(this.recentHisQcTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisQcType that bai, can kiem tra lai." + LogUtil.TraceData("recentHisQcTypes", this.recentHisQcTypes));
                }
				this.recentHisQcTypes = null;
            }
        }
    }
}
