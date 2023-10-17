using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisContraindication
{
    partial class HisContraindicationCreate : BusinessBase
    {
		private List<HIS_CONTRAINDICATION> recentHisContraindications = new List<HIS_CONTRAINDICATION>();
		
        internal HisContraindicationCreate()
            : base()
        {

        }

        internal HisContraindicationCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_CONTRAINDICATION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisContraindicationCheck checker = new HisContraindicationCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.CONTRAINDICATION_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisContraindicationDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisContraindication_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisContraindication that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisContraindications.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisContraindications))
            {
                if (!DAOWorker.HisContraindicationDAO.TruncateList(this.recentHisContraindications))
                {
                    LogSystem.Warn("Rollback du lieu HisContraindication that bai, can kiem tra lai." + LogUtil.TraceData("recentHisContraindications", this.recentHisContraindications));
                }
				this.recentHisContraindications = null;
            }
        }
    }
}
