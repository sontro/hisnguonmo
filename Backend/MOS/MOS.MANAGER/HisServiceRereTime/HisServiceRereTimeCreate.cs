using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceRereTime
{
    partial class HisServiceRereTimeCreate : BusinessBase
    {
		private List<HIS_SERVICE_RERE_TIME> recentHisServiceRereTimes = new List<HIS_SERVICE_RERE_TIME>();
		
        internal HisServiceRereTimeCreate()
            : base()
        {

        }

        internal HisServiceRereTimeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SERVICE_RERE_TIME data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceRereTimeCheck checker = new HisServiceRereTimeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisServiceRereTimeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceRereTime_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisServiceRereTime that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisServiceRereTimes.Add(data);
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
		
		internal bool CreateList(List<HIS_SERVICE_RERE_TIME> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceRereTimeCheck checker = new HisServiceRereTimeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisServiceRereTimeDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceRereTime_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisServiceRereTime that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisServiceRereTimes.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisServiceRereTimes))
            {
                if (!DAOWorker.HisServiceRereTimeDAO.TruncateList(this.recentHisServiceRereTimes))
                {
                    LogSystem.Warn("Rollback du lieu HisServiceRereTime that bai, can kiem tra lai." + LogUtil.TraceData("recentHisServiceRereTimes", this.recentHisServiceRereTimes));
                }
				this.recentHisServiceRereTimes = null;
            }
        }
    }
}
