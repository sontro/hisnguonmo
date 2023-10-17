using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCareType
{
    partial class HisCareTypeCreate : BusinessBase
    {
		private HIS_CARE_TYPE recentHisCareTypeDTO;
		
        internal HisCareTypeCreate()
            : base()
        {

        }

        internal HisCareTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_CARE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCareTypeCheck checker = new HisCareTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.CARE_TYPE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisCareTypeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCareType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisCareType that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisCareTypeDTO = data;
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
            if (this.recentHisCareTypeDTO != null)
            {
                if (!new HisCareTypeTruncate(param).Truncate(this.recentHisCareTypeDTO))
                {
                    LogSystem.Warn("Rollback du lieu HisCareType that bai, can kiem tra lai." + LogUtil.TraceData("HisCareType", this.recentHisCareTypeDTO));
                }
            }
        }
    }
}
