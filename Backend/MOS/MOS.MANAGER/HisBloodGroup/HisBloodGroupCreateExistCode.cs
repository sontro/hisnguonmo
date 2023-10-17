using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBloodGroup
{
    partial class HisBloodGroupCreate : BusinessBase
    {
		private List<HIS_BLOOD_GROUP> recentHisBloodGroups = new List<HIS_BLOOD_GROUP>();
		
        internal HisBloodGroupCreate()
            : base()
        {

        }

        internal HisBloodGroupCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_BLOOD_GROUP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBloodGroupCheck checker = new HisBloodGroupCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.BLOOD_GROUP_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisBloodGroupDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBloodGroup_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBloodGroup that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisBloodGroups.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisBloodGroups))
            {
                if (!new HisBloodGroupTruncate(param).TruncateList(this.recentHisBloodGroups))
                {
                    LogSystem.Warn("Rollback du lieu HisBloodGroup that bai, can kiem tra lai." + LogUtil.TraceData("recentHisBloodGroups", this.recentHisBloodGroups));
                }
            }
        }
    }
}
