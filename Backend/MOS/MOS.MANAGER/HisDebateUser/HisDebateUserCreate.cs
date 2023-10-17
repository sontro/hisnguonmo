using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebateUser
{
    partial class HisDebateUserCreate : BusinessBase
    {
        private HIS_DEBATE_USER recentHisDebateUserDTO;

        internal HisDebateUserCreate()
            : base()
        {

        }

        internal HisDebateUserCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_DEBATE_USER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDebateUserCheck checker = new HisDebateUserCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisDebateUserDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDebateUser_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisDebateUser that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisDebateUserDTO = data;
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

        internal bool CreateList(List<HIS_DEBATE_USER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDebateUserCheck checker = new HisDebateUserCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    result = DAOWorker.HisDebateUserDAO.CreateList(listData);
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
            if (this.recentHisDebateUserDTO != null)
            {
                if (!new HisDebateUserTruncate(param).Truncate(this.recentHisDebateUserDTO))
                {
                    LogSystem.Warn("Rollback du lieu HisDebateUser that bai, can kiem tra lai." + LogUtil.TraceData("HisDebateUser", this.recentHisDebateUserDTO));
                }
            }
        }
    }
}
