using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisFuexType
{
    partial class HisFuexTypeCreate : BusinessBase
    {
		private List<HIS_FUEX_TYPE> recentHisFuexTypes = new List<HIS_FUEX_TYPE>();
		
        internal HisFuexTypeCreate()
            : base()
        {

        }

        internal HisFuexTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_FUEX_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisFuexTypeCheck checker = new HisFuexTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.FUEX_TYPE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisFuexTypeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisFuexType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisFuexType that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisFuexTypes.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisFuexTypes))
            {
                if (!DAOWorker.HisFuexTypeDAO.TruncateList(this.recentHisFuexTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisFuexType that bai, can kiem tra lai." + LogUtil.TraceData("recentHisFuexTypes", this.recentHisFuexTypes));
                }
				this.recentHisFuexTypes = null;
            }
        }
    }
}
