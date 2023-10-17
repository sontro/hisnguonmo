using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaexVaer
{
    partial class HisVaexVaerCreate : BusinessBase
    {
		private List<HIS_VAEX_VAER> recentHisVaexVaers = new List<HIS_VAEX_VAER>();
		
        internal HisVaexVaerCreate()
            : base()
        {

        }

        internal HisVaexVaerCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_VAEX_VAER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaexVaerCheck checker = new HisVaexVaerCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisVaexVaerDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaexVaer_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisVaexVaer that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisVaexVaers.Add(data);
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
		
		internal bool CreateList(List<HIS_VAEX_VAER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVaexVaerCheck checker = new HisVaexVaerCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisVaexVaerDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaexVaer_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisVaexVaer that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisVaexVaers.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisVaexVaers))
            {
                if (!DAOWorker.HisVaexVaerDAO.TruncateList(this.recentHisVaexVaers))
                {
                    LogSystem.Warn("Rollback du lieu HisVaexVaer that bai, can kiem tra lai." + LogUtil.TraceData("recentHisVaexVaers", this.recentHisVaexVaers));
                }
				this.recentHisVaexVaers = null;
            }
        }
    }
}
