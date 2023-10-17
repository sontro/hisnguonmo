using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAnticipate
{
    partial class HisAnticipateCreate : BusinessBase
    {
        private List<HIS_ANTICIPATE> recentHisAnticipates = new List<HIS_ANTICIPATE>();

        internal HisAnticipateCreate()
            : base()
        {

        }

        internal HisAnticipateCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ANTICIPATE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAnticipateCheck checker = new HisAnticipateCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyValidData(data);
                if (valid)
                {
                    data.REQUEST_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    data.REQUEST_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();

                    if (!DAOWorker.HisAnticipateDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAnticipate_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAnticipate that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisAnticipates.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisAnticipates))
            {
                if (!new HisAnticipateTruncate(param).TruncateList(this.recentHisAnticipates))
                {
                    LogSystem.Warn("Rollback du lieu HisAnticipate that bai, can kiem tra lai." + LogUtil.TraceData("recentHisAnticipates", this.recentHisAnticipates));
                }
            }
        }
    }
}
