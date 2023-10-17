using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDiimType
{
    partial class HisDiimTypeCreate : BusinessBase
    {
		private List<HIS_DIIM_TYPE> recentHisDiimTypes = new List<HIS_DIIM_TYPE>();
		
        internal HisDiimTypeCreate()
            : base()
        {

        }

        internal HisDiimTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_DIIM_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDiimTypeCheck checker = new HisDiimTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.DIIM_TYPE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisDiimTypeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDiimType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisDiimType that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisDiimTypes.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisDiimTypes))
            {
                if (!DAOWorker.HisDiimTypeDAO.TruncateList(this.recentHisDiimTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisDiimType that bai, can kiem tra lai." + LogUtil.TraceData("recentHisDiimTypes", this.recentHisDiimTypes));
                }
				this.recentHisDiimTypes = null;
            }
        }
    }
}
