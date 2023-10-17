using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPatyTrty
{
    partial class HisMestPatyTrtyCreate : BusinessBase
    {
		private List<HIS_MEST_PATY_TRTY> recentHisMestPatyTrtys = new List<HIS_MEST_PATY_TRTY>();
		
        internal HisMestPatyTrtyCreate()
            : base()
        {

        }

        internal HisMestPatyTrtyCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEST_PATY_TRTY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestPatyTrtyCheck checker = new HisMestPatyTrtyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsNotExists(data);
                if (valid)
                {
					if (!DAOWorker.HisMestPatyTrtyDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestPatyTrty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMestPatyTrty that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMestPatyTrtys.Add(data);
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
		
		internal bool CreateList(List<HIS_MEST_PATY_TRTY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMestPatyTrtyCheck checker = new HisMestPatyTrtyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.IsNotExists(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMestPatyTrtyDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestPatyTrty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMestPatyTrty that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMestPatyTrtys.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMestPatyTrtys))
            {
                if (!DAOWorker.HisMestPatyTrtyDAO.TruncateList(this.recentHisMestPatyTrtys))
                {
                    LogSystem.Warn("Rollback du lieu HisMestPatyTrty that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMestPatyTrtys", this.recentHisMestPatyTrtys));
                }
				this.recentHisMestPatyTrtys = null;
            }
        }
    }
}
