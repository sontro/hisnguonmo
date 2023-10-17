using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntibioticMicrobi
{
    partial class HisAntibioticMicrobiCreate : BusinessBase
    {
		private List<HIS_ANTIBIOTIC_MICROBI> recentHisAntibioticMicrobis = new List<HIS_ANTIBIOTIC_MICROBI>();
		
        internal HisAntibioticMicrobiCreate()
            : base()
        {

        }

        internal HisAntibioticMicrobiCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ANTIBIOTIC_MICROBI data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAntibioticMicrobiCheck checker = new HisAntibioticMicrobiCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.ANTIBIOTIC_MICROBI_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisAntibioticMicrobiDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAntibioticMicrobi_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAntibioticMicrobi that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisAntibioticMicrobis.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisAntibioticMicrobis))
            {
                if (!DAOWorker.HisAntibioticMicrobiDAO.TruncateList(this.recentHisAntibioticMicrobis))
                {
                    LogSystem.Warn("Rollback du lieu HisAntibioticMicrobi that bai, can kiem tra lai." + LogUtil.TraceData("recentHisAntibioticMicrobis", this.recentHisAntibioticMicrobis));
                }
				this.recentHisAntibioticMicrobis = null;
            }
        }
    }
}
