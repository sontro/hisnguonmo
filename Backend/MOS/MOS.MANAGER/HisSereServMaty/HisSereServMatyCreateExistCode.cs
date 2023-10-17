using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServMaty
{
    partial class HisSereServMatyCreate : BusinessBase
    {
		private List<HIS_SERE_SERV_MATY> recentHisSereServMatys = new List<HIS_SERE_SERV_MATY>();
		
        internal HisSereServMatyCreate()
            : base()
        {

        }

        internal HisSereServMatyCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SERE_SERV_MATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServMatyCheck checker = new HisSereServMatyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.SERE_SERV_MATY_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisSereServMatyDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServMaty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSereServMaty that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisSereServMatys.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisSereServMatys))
            {
                if (!new HisSereServMatyTruncate(param).TruncateList(this.recentHisSereServMatys))
                {
                    LogSystem.Warn("Rollback du lieu HisSereServMaty that bai, can kiem tra lai." + LogUtil.TraceData("recentHisSereServMatys", this.recentHisSereServMatys));
                }
            }
        }
    }
}
