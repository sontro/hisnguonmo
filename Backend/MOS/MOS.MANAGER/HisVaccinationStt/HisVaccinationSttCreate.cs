using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccinationStt
{
    partial class HisVaccinationSttCreate : BusinessBase
    {
		private List<HIS_VACCINATION_STT> recentHisVaccinationStts = new List<HIS_VACCINATION_STT>();
		
        internal HisVaccinationSttCreate()
            : base()
        {

        }

        internal HisVaccinationSttCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_VACCINATION_STT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccinationSttCheck checker = new HisVaccinationSttCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisVaccinationSttDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccinationStt_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisVaccinationStt that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisVaccinationStts.Add(data);
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
		
		internal bool CreateList(List<HIS_VACCINATION_STT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVaccinationSttCheck checker = new HisVaccinationSttCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisVaccinationSttDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccinationStt_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisVaccinationStt that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisVaccinationStts.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisVaccinationStts))
            {
                if (!DAOWorker.HisVaccinationSttDAO.TruncateList(this.recentHisVaccinationStts))
                {
                    LogSystem.Warn("Rollback du lieu HisVaccinationStt that bai, can kiem tra lai." + LogUtil.TraceData("recentHisVaccinationStts", this.recentHisVaccinationStts));
                }
				this.recentHisVaccinationStts = null;
            }
        }
    }
}
