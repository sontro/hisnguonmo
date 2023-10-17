using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBidType
{
    partial class HisBidTypeCreate : BusinessBase
    {
		private List<HIS_BID_TYPE> recentHisBidTypes = new List<HIS_BID_TYPE>();
		
        internal HisBidTypeCreate()
            : base()
        {

        }

        internal HisBidTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_BID_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBidTypeCheck checker = new HisBidTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.BID_TYPE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisBidTypeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBidType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBidType that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisBidTypes.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisBidTypes))
            {
                if (!new HisBidTypeTruncate(param).TruncateList(this.recentHisBidTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisBidType that bai, can kiem tra lai." + LogUtil.TraceData("recentHisBidTypes", this.recentHisBidTypes));
                }
            }
        }
    }
}
