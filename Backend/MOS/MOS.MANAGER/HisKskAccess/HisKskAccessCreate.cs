using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskAccess
{
    partial class HisKskAccessCreate : BusinessBase
    {
		private List<HIS_KSK_ACCESS> recentHisKskAccesss = new List<HIS_KSK_ACCESS>();
		
        internal HisKskAccessCreate()
            : base()
        {

        }

        internal HisKskAccessCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_KSK_ACCESS data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskAccessCheck checker = new HisKskAccessCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisKskAccessDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskAccess_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisKskAccess that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisKskAccesss.Add(data);
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
		
		internal bool CreateList(List<HIS_KSK_ACCESS> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisKskAccessCheck checker = new HisKskAccessCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisKskAccessDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskAccess_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisKskAccess that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisKskAccesss.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisKskAccesss))
            {
                if (!DAOWorker.HisKskAccessDAO.TruncateList(this.recentHisKskAccesss))
                {
                    LogSystem.Warn("Rollback du lieu HisKskAccess that bai, can kiem tra lai." + LogUtil.TraceData("recentHisKskAccesss", this.recentHisKskAccesss));
                }
				this.recentHisKskAccesss = null;
            }
        }
    }
}
