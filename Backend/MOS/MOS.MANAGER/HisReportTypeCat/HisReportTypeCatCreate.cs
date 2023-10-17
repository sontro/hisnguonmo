using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisReportTypeCat
{
    partial class HisReportTypeCatCreate : BusinessBase
    {
		private HIS_REPORT_TYPE_CAT recentHisReportTypeCat;
		
        internal HisReportTypeCatCreate()
            : base()
        {

        }

        internal HisReportTypeCatCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_REPORT_TYPE_CAT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisReportTypeCatCheck checker = new HisReportTypeCatCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsNoExisted(data);
                if (valid)
                {
					if (!DAOWorker.HisReportTypeCatDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisReportTypeCat_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisReportTypeCat that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisReportTypeCat = data;
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
            if (this.recentHisReportTypeCat != null)
            {
                if (!new HisReportTypeCatTruncate(param).Truncate(this.recentHisReportTypeCat))
                {
                    LogSystem.Warn("Rollback du lieu HisReportTypeCat that bai, can kiem tra lai." + LogUtil.TraceData("HisReportTypeCat", this.recentHisReportTypeCat));
                }
            }
        }
    }
}
