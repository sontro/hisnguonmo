using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisUnlimitType
{
    partial class HisUnlimitTypeCreate : BusinessBase
    {
		private List<HIS_UNLIMIT_TYPE> recentHisUnlimitTypes = new List<HIS_UNLIMIT_TYPE>();
		
        internal HisUnlimitTypeCreate()
            : base()
        {

        }

        internal HisUnlimitTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_UNLIMIT_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisUnlimitTypeCheck checker = new HisUnlimitTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.UNLIMIT_TYPE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisUnlimitTypeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisUnlimitType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisUnlimitType that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisUnlimitTypes.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisUnlimitTypes))
            {
                if (!DAOWorker.HisUnlimitTypeDAO.TruncateList(this.recentHisUnlimitTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisUnlimitType that bai, can kiem tra lai." + LogUtil.TraceData("recentHisUnlimitTypes", this.recentHisUnlimitTypes));
                }
				this.recentHisUnlimitTypes = null;
            }
        }
    }
}
