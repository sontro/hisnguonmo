using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisWelfareType
{
    partial class HisWelfareTypeCreate : BusinessBase
    {
		private List<HIS_WELFARE_TYPE> recentHisWelfareTypes = new List<HIS_WELFARE_TYPE>();
		
        internal HisWelfareTypeCreate()
            : base()
        {

        }

        internal HisWelfareTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_WELFARE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisWelfareTypeCheck checker = new HisWelfareTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.WELFARE_TYPE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisWelfareTypeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisWelfareType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisWelfareType that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisWelfareTypes.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisWelfareTypes))
            {
                if (!DAOWorker.HisWelfareTypeDAO.TruncateList(this.recentHisWelfareTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisWelfareType that bai, can kiem tra lai." + LogUtil.TraceData("recentHisWelfareTypes", this.recentHisWelfareTypes));
                }
				this.recentHisWelfareTypes = null;
            }
        }
    }
}
