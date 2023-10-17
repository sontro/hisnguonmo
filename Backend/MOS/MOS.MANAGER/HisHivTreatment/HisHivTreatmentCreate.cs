using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHivTreatment
{
    partial class HisHivTreatmentCreate : BusinessBase
    {
		private List<HIS_HIV_TREATMENT> recentHisHivTreatments = new List<HIS_HIV_TREATMENT>();
		
        internal HisHivTreatmentCreate()
            : base()
        {

        }

        internal HisHivTreatmentCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_HIV_TREATMENT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisHivTreatmentCheck checker = new HisHivTreatmentCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisHivTreatmentDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHivTreatment_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisHivTreatment that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisHivTreatments.Add(data);
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
		
		internal bool CreateList(List<HIS_HIV_TREATMENT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisHivTreatmentCheck checker = new HisHivTreatmentCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisHivTreatmentDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHivTreatment_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisHivTreatment that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisHivTreatments.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisHivTreatments))
            {
                if (!DAOWorker.HisHivTreatmentDAO.TruncateList(this.recentHisHivTreatments))
                {
                    LogSystem.Warn("Rollback du lieu HisHivTreatment that bai, can kiem tra lai." + LogUtil.TraceData("recentHisHivTreatments", this.recentHisHivTreatments));
                }
				this.recentHisHivTreatments = null;
            }
        }
    }
}
