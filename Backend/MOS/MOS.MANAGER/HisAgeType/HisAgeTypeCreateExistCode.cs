using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAgeType
{
    partial class HisAgeTypeCreate : BusinessBase
    {
		private List<HIS_AGE_TYPE> recentHisAgeTypes = new List<HIS_AGE_TYPE>();
		
        internal HisAgeTypeCreate()
            : base()
        {

        }

        internal HisAgeTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_AGE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAgeTypeCheck checker = new HisAgeTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.AGE_TYPE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisAgeTypeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAgeType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAgeType that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisAgeTypes.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisAgeTypes))
            {
                if (!DAOWorker.HisAgeTypeDAO.TruncateList(this.recentHisAgeTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisAgeType that bai, can kiem tra lai." + LogUtil.TraceData("recentHisAgeTypes", this.recentHisAgeTypes));
                }
				this.recentHisAgeTypes = null;
            }
        }
    }
}
