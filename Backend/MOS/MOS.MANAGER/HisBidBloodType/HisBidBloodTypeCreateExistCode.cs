using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBidBloodType
{
    partial class HisBidBloodTypeCreate : BusinessBase
    {
		private List<HIS_BID_BLOOD_TYPE> recentHisBidBloodTypes = new List<HIS_BID_BLOOD_TYPE>();
		
        internal HisBidBloodTypeCreate()
            : base()
        {

        }

        internal HisBidBloodTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_BID_BLOOD_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBidBloodTypeCheck checker = new HisBidBloodTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.BID_BLOOD_TYPE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisBidBloodTypeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBidBloodType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBidBloodType that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisBidBloodTypes.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisBidBloodTypes))
            {
                if (!new HisBidBloodTypeTruncate(param).TruncateList(this.recentHisBidBloodTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisBidBloodType that bai, can kiem tra lai." + LogUtil.TraceData("recentHisBidBloodTypes", this.recentHisBidBloodTypes));
                }
            }
        }
    }
}
