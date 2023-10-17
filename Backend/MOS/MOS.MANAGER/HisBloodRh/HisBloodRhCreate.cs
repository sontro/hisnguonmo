using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBloodRh
{
    partial class HisBloodRhCreate : BusinessBase
    {
		private HIS_BLOOD_RH recentHisBloodRhDTO;
		
        internal HisBloodRhCreate()
            : base()
        {

        }

        internal HisBloodRhCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_BLOOD_RH data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBloodRhCheck checker = new HisBloodRhCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.BLOOD_RH_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisBloodRhDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBloodRh_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBloodRh that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisBloodRhDTO = data;
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
            if (this.recentHisBloodRhDTO != null)
            {
                if (!new HisBloodRhTruncate(param).Truncate(this.recentHisBloodRhDTO))
                {
                    LogSystem.Warn("Rollback du lieu HisBloodRh that bai, can kiem tra lai." + LogUtil.TraceData("HisBloodRhDTO", this.recentHisBloodRhDTO));
                }
            }
        }
    }
}
