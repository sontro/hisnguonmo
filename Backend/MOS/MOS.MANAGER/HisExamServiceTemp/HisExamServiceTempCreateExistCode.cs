using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExamServiceTemp
{
    partial class HisExamServiceTempCreate : BusinessBase
    {
		private List<HIS_EXAM_SERVICE_TEMP> recentHisExamServiceTemps = new List<HIS_EXAM_SERVICE_TEMP>();
		
        internal HisExamServiceTempCreate()
            : base()
        {

        }

        internal HisExamServiceTempCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EXAM_SERVICE_TEMP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExamServiceTempCheck checker = new HisExamServiceTempCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.EXAM_SERVICE_TEMP_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisExamServiceTempDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExamServiceTemp_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisExamServiceTemp that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisExamServiceTemps.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisExamServiceTemps))
            {
                if (!new HisExamServiceTempTruncate(param).TruncateList(this.recentHisExamServiceTemps))
                {
                    LogSystem.Warn("Rollback du lieu HisExamServiceTemp that bai, can kiem tra lai." + LogUtil.TraceData("recentHisExamServiceTemps", this.recentHisExamServiceTemps));
                }
            }
        }
    }
}
