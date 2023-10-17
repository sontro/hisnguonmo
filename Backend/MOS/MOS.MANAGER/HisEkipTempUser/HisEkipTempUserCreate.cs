using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEkipTempUser
{
    partial class HisEkipTempUserCreate : BusinessBase
    {
		private List<HIS_EKIP_TEMP_USER> recentHisEkipTempUsers = new List<HIS_EKIP_TEMP_USER>();
		
        internal HisEkipTempUserCreate()
            : base()
        {

        }

        internal HisEkipTempUserCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EKIP_TEMP_USER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEkipTempUserCheck checker = new HisEkipTempUserCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisEkipTempUserDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEkipTempUser_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisEkipTempUser that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisEkipTempUsers.Add(data);
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
		
		internal bool CreateList(List<HIS_EKIP_TEMP_USER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEkipTempUserCheck checker = new HisEkipTempUserCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisEkipTempUserDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEkipTempUser_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisEkipTempUser that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisEkipTempUsers.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisEkipTempUsers))
            {
                if (!DAOWorker.HisEkipTempUserDAO.TruncateList(this.recentHisEkipTempUsers))
                {
                    LogSystem.Warn("Rollback du lieu HisEkipTempUser that bai, can kiem tra lai." + LogUtil.TraceData("recentHisEkipTempUsers", this.recentHisEkipTempUsers));
                }
            }
        }
    }
}
