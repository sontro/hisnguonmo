using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVitaminA
{
    partial class HisVitaminACreate : BusinessBase
    {
		private List<HIS_VITAMIN_A> recentHisVitaminAs = new List<HIS_VITAMIN_A>();
		
        internal HisVitaminACreate()
            : base()
        {

        }

        internal HisVitaminACreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_VITAMIN_A data, HIS_PATIENT patient)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVitaminACheck checker = new HisVitaminACheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.CheckCaseType(data, patient);
                valid = valid && checker.ExistsCode(data.VITAMIN_A_CODE, null);
                if (valid)
                {
                    HisVitaminAUtil.SetTdl(data, patient);
                    if (!DAOWorker.HisVitaminADAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVitaminA_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisVitaminA that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisVitaminAs.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisVitaminAs))
            {
                if (!DAOWorker.HisVitaminADAO.TruncateList(this.recentHisVitaminAs))
                {
                    LogSystem.Warn("Rollback du lieu HisVitaminA that bai, can kiem tra lai." + LogUtil.TraceData("recentHisVitaminAs", this.recentHisVitaminAs));
                }
				this.recentHisVitaminAs = null;
            }
        }
    }
}
