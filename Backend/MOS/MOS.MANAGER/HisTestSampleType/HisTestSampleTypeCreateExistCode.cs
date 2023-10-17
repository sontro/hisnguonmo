using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTestSampleType
{
    partial class HisTestSampleTypeCreate : BusinessBase
    {
		private List<HIS_TEST_SAMPLE_TYPE> recentHisTestSampleTypes = new List<HIS_TEST_SAMPLE_TYPE>();
		
        internal HisTestSampleTypeCreate()
            : base()
        {

        }

        internal HisTestSampleTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_TEST_SAMPLE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTestSampleTypeCheck checker = new HisTestSampleTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.TEST_SAMPLE_TYPE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisTestSampleTypeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTestSampleType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisTestSampleType that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisTestSampleTypes.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisTestSampleTypes))
            {
                if (!DAOWorker.HisTestSampleTypeDAO.TruncateList(this.recentHisTestSampleTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisTestSampleType that bai, can kiem tra lai." + LogUtil.TraceData("recentHisTestSampleTypes", this.recentHisTestSampleTypes));
                }
				this.recentHisTestSampleTypes = null;
            }
        }
    }
}
