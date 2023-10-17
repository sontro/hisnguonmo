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
		
		internal bool CreateList(List<HIS_ANTIBIOTIC_MICROBI> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAntibioticMicrobiCheck checker = new HisAntibioticMicrobiCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisAntibioticMicrobiDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAntibioticMicrobi_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAntibioticMicrobi that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisAntibioticMicrobis.AddRange(listData);
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
