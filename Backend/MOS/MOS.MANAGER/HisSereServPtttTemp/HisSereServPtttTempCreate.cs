using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServPtttTemp
{
    partial class HisSereServPtttTempCreate : BusinessBase
    {
		private List<HIS_SERE_SERV_PTTT_TEMP> recentHisSereServPtttTemps = new List<HIS_SERE_SERV_PTTT_TEMP>();
		
        internal HisSereServPtttTempCreate()
            : base()
        {

        }

        internal HisSereServPtttTempCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SERE_SERV_PTTT_TEMP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServPtttTempCheck checker = new HisSereServPtttTempCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisSereServPtttTempDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServPtttTemp_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSereServPtttTemp that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisSereServPtttTemps.Add(data);
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
		
		internal bool CreateList(List<HIS_SERE_SERV_PTTT_TEMP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServPtttTempCheck checker = new HisSereServPtttTempCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisSereServPtttTempDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServPtttTemp_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSereServPtttTemp that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisSereServPtttTemps.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisSereServPtttTemps))
            {
                if (!DAOWorker.HisSereServPtttTempDAO.TruncateList(this.recentHisSereServPtttTemps))
                {
                    LogSystem.Warn("Rollback du lieu HisSereServPtttTemp that bai, can kiem tra lai." + LogUtil.TraceData("recentHisSereServPtttTemps", this.recentHisSereServPtttTemps));
                }
				this.recentHisSereServPtttTemps = null;
            }
        }
    }
}
