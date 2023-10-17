using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPrepare
{
    partial class HisPrepareCreate : BusinessBase
    {
		private List<HIS_PREPARE> recentHisPrepares = new List<HIS_PREPARE>();
		
        internal HisPrepareCreate()
            : base()
        {

        }

        internal HisPrepareCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_PREPARE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPrepareCheck checker = new HisPrepareCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (String.IsNullOrWhiteSpace(data.REQ_LOGINNAME))
                    {
                        data.REQ_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        data.REQ_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    }
					if (!DAOWorker.HisPrepareDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPrepare_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPrepare that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisPrepares.Add(data);
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
		
		internal bool CreateList(List<HIS_PREPARE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPrepareCheck checker = new HisPrepareCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisPrepareDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPrepare_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPrepare that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisPrepares.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisPrepares))
            {
                if (!DAOWorker.HisPrepareDAO.TruncateList(this.recentHisPrepares))
                {
                    LogSystem.Warn("Rollback du lieu HisPrepare that bai, can kiem tra lai." + LogUtil.TraceData("recentHisPrepares", this.recentHisPrepares));
                }
				this.recentHisPrepares = null;
            }
        }
    }
}
