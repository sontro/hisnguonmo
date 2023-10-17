using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTestType
{
    partial class HisTestTypeCreate : BusinessBase
    {
		private List<HIS_TEST_TYPE> recentHisTestTypes = new List<HIS_TEST_TYPE>();
		
        internal HisTestTypeCreate()
            : base()
        {

        }

        internal HisTestTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_TEST_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTestTypeCheck checker = new HisTestTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.TEST_TYPE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisTestTypeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTestType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisTestType that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisTestTypes.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisTestTypes))
            {
                if (!DAOWorker.HisTestTypeDAO.TruncateList(this.recentHisTestTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisTestType that bai, can kiem tra lai." + LogUtil.TraceData("recentHisTestTypes", this.recentHisTestTypes));
                }
				this.recentHisTestTypes = null;
            }
        }
    }
}
