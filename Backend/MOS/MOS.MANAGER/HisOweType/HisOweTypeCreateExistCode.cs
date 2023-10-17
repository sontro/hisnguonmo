using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisOweType
{
    partial class HisOweTypeCreate : BusinessBase
    {
		private List<HIS_OWE_TYPE> recentHisOweTypes = new List<HIS_OWE_TYPE>();
		
        internal HisOweTypeCreate()
            : base()
        {

        }

        internal HisOweTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_OWE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisOweTypeCheck checker = new HisOweTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.OWE_TYPE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisOweTypeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisOweType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisOweType that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisOweTypes.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisOweTypes))
            {
                if (!new HisOweTypeTruncate(param).TruncateList(this.recentHisOweTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisOweType that bai, can kiem tra lai." + LogUtil.TraceData("recentHisOweTypes", this.recentHisOweTypes));
                }
            }
        }
    }
}
