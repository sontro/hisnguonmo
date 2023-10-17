using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEkipUser
{
    partial class HisEkipUserCreate : BusinessBase
    {
		private List<HIS_EKIP_USER> recentHisEkipUsers = new List<HIS_EKIP_USER>();
		
        internal HisEkipUserCreate()
            : base()
        {

        }

        internal HisEkipUserCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EKIP_USER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEkipUserCheck checker = new HisEkipUserCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisEkipUserDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEkipUser_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisEkipUser that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisEkipUsers.Add(data);
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

        internal bool CreateList(List<HIS_EKIP_USER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEkipUserCheck checker = new HisEkipUserCheck(param);
                valid = IsNotNullOrEmpty(listData);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }

                if (valid)
                {
                    if (!DAOWorker.HisEkipUserDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEkipUser_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisEkipUser that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisEkipUsers.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisEkipUsers))
            {
                if (!new HisEkipUserTruncate(param).TruncateList(this.recentHisEkipUsers))
                {
                    LogSystem.Warn("Rollback du lieu HisEkipUser that bai, can kiem tra lai." + LogUtil.TraceData("recentHisEkipUsers", this.recentHisEkipUsers));
                }
            }
        }
    }
}
