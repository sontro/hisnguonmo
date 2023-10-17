using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDispenseType
{
    partial class HisDispenseTypeCreate : BusinessBase
    {
		private List<HIS_DISPENSE_TYPE> recentHisDispenseTypes = new List<HIS_DISPENSE_TYPE>();
		
        internal HisDispenseTypeCreate()
            : base()
        {

        }

        internal HisDispenseTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_DISPENSE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDispenseTypeCheck checker = new HisDispenseTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.DISPENSE_TYPE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisDispenseTypeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDispenseType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisDispenseType that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisDispenseTypes.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisDispenseTypes))
            {
                if (!DAOWorker.HisDispenseTypeDAO.TruncateList(this.recentHisDispenseTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisDispenseType that bai, can kiem tra lai." + LogUtil.TraceData("recentHisDispenseTypes", this.recentHisDispenseTypes));
                }
				this.recentHisDispenseTypes = null;
            }
        }
    }
}
