using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDocHoldType
{
    partial class HisDocHoldTypeCreate : BusinessBase
    {
		private List<HIS_DOC_HOLD_TYPE> recentHisDocHoldTypes = new List<HIS_DOC_HOLD_TYPE>();
		
        internal HisDocHoldTypeCreate()
            : base()
        {

        }

        internal HisDocHoldTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_DOC_HOLD_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDocHoldTypeCheck checker = new HisDocHoldTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.DOC_HOLD_TYPE_CODE, null);
                valid = valid && checker.IsExistedHeinCard(data.IS_HEIN_CARD);
                if (valid)
                {
					if (!DAOWorker.HisDocHoldTypeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDocHoldType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisDocHoldType that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisDocHoldTypes.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisDocHoldTypes))
            {
                if (!DAOWorker.HisDocHoldTypeDAO.TruncateList(this.recentHisDocHoldTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisDocHoldType that bai, can kiem tra lai." + LogUtil.TraceData("recentHisDocHoldTypes", this.recentHisDocHoldTypes));
                }
				this.recentHisDocHoldTypes = null;
            }
        }
    }
}
