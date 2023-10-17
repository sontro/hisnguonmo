using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAccidentHelmet
{
    partial class HisAccidentHelmetCreate : BusinessBase
    {
		private List<HIS_ACCIDENT_HELMET> recentHisAccidentHelmets = new List<HIS_ACCIDENT_HELMET>();
		
        internal HisAccidentHelmetCreate()
            : base()
        {

        }

        internal HisAccidentHelmetCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ACCIDENT_HELMET data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAccidentHelmetCheck checker = new HisAccidentHelmetCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.ACCIDENT_HELMET_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisAccidentHelmetDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAccidentHelmet_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAccidentHelmet that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisAccidentHelmets.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisAccidentHelmets))
            {
                if (!new HisAccidentHelmetTruncate(param).TruncateList(this.recentHisAccidentHelmets))
                {
                    LogSystem.Warn("Rollback du lieu HisAccidentHelmet that bai, can kiem tra lai." + LogUtil.TraceData("recentHisAccidentHelmets", this.recentHisAccidentHelmets));
                }
            }
        }
    }
}
