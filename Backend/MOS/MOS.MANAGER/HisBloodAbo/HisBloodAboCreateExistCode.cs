using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBloodAbo
{
    partial class HisBloodAboCreate : BusinessBase
    {
		private List<HIS_BLOOD_ABO> recentHisBloodAbos = new List<HIS_BLOOD_ABO>();
		
        internal HisBloodAboCreate()
            : base()
        {

        }

        internal HisBloodAboCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_BLOOD_ABO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBloodAboCheck checker = new HisBloodAboCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.BLOOD_ABO_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisBloodAboDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBloodAbo_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBloodAbo that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisBloodAbos.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisBloodAbos))
            {
                if (!new HisBloodAboTruncate(param).TruncateList(this.recentHisBloodAbos))
                {
                    LogSystem.Warn("Rollback du lieu HisBloodAbo that bai, can kiem tra lai." + LogUtil.TraceData("recentHisBloodAbos", this.recentHisBloodAbos));
                }
            }
        }
    }
}
