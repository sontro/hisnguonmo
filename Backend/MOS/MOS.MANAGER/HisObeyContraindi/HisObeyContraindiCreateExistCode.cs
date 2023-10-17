using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisObeyContraindi
{
    partial class HisObeyContraindiCreate : BusinessBase
    {
		private List<HIS_OBEY_CONTRAINDI> recentHisObeyContraindis = new List<HIS_OBEY_CONTRAINDI>();
		
        internal HisObeyContraindiCreate()
            : base()
        {

        }

        internal HisObeyContraindiCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_OBEY_CONTRAINDI data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisObeyContraindiCheck checker = new HisObeyContraindiCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.OBEY_CONTRAINDI_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisObeyContraindiDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisObeyContraindi_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisObeyContraindi that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisObeyContraindis.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisObeyContraindis))
            {
                if (!DAOWorker.HisObeyContraindiDAO.TruncateList(this.recentHisObeyContraindis))
                {
                    LogSystem.Warn("Rollback du lieu HisObeyContraindi that bai, can kiem tra lai." + LogUtil.TraceData("recentHisObeyContraindis", this.recentHisObeyContraindis));
                }
				this.recentHisObeyContraindis = null;
            }
        }
    }
}
