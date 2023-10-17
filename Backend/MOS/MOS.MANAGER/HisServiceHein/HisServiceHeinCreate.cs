using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceHein
{
    partial class HisServiceHeinCreate : BusinessBase
    {
		private List<HIS_SERVICE_HEIN> recentHisServiceHeins = new List<HIS_SERVICE_HEIN>();
		
        internal HisServiceHeinCreate()
            : base()
        {

        }

        internal HisServiceHeinCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SERVICE_HEIN data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceHeinCheck checker = new HisServiceHeinCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisServiceHeinDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceHein_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisServiceHein that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisServiceHeins.Add(data);
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
		
		internal bool CreateList(List<HIS_SERVICE_HEIN> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceHeinCheck checker = new HisServiceHeinCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisServiceHeinDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceHein_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisServiceHein that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisServiceHeins.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisServiceHeins))
            {
                if (!DAOWorker.HisServiceHeinDAO.TruncateList(this.recentHisServiceHeins))
                {
                    LogSystem.Warn("Rollback du lieu HisServiceHein that bai, can kiem tra lai." + LogUtil.TraceData("recentHisServiceHeins", this.recentHisServiceHeins));
                }
				this.recentHisServiceHeins = null;
            }
        }
    }
}
