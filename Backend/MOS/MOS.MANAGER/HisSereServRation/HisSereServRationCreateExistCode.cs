using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServRation
{
    partial class HisSereServRationCreate : BusinessBase
    {
		private List<HIS_SERE_SERV_RATION> recentHisSereServRations = new List<HIS_SERE_SERV_RATION>();
		
        internal HisSereServRationCreate()
            : base()
        {

        }

        internal HisSereServRationCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SERE_SERV_RATION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServRationCheck checker = new HisSereServRationCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.SERE_SERV_RATION_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisSereServRationDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServRation_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSereServRation that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisSereServRations.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisSereServRations))
            {
                if (!DAOWorker.HisSereServRationDAO.TruncateList(this.recentHisSereServRations))
                {
                    LogSystem.Warn("Rollback du lieu HisSereServRation that bai, can kiem tra lai." + LogUtil.TraceData("recentHisSereServRations", this.recentHisSereServRations));
                }
				this.recentHisSereServRations = null;
            }
        }
    }
}
