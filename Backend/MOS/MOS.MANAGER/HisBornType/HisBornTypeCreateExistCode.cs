using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBornType
{
    partial class HisBornTypeCreate : BusinessBase
    {
		private List<HIS_BORN_TYPE> recentHisBornTypes = new List<HIS_BORN_TYPE>();
		
        internal HisBornTypeCreate()
            : base()
        {

        }

        internal HisBornTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_BORN_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBornTypeCheck checker = new HisBornTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.BORN_TYPE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisBornTypeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBornType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBornType that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisBornTypes.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisBornTypes))
            {
                if (!new HisBornTypeTruncate(param).TruncateList(this.recentHisBornTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisBornType that bai, can kiem tra lai." + LogUtil.TraceData("recentHisBornTypes", this.recentHisBornTypes));
                }
            }
        }
    }
}
