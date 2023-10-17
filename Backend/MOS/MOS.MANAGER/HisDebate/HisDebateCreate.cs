using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisDebateUser;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebate
{
    partial class HisDebateCreate : BusinessBase
    {
        private HIS_DEBATE recentHisDebate;
        private HisDebateUserCreate hisDebateUserCreate;

        internal HisDebateCreate()
            : base()
        {
            this.hisDebateUserCreate = new HisDebateUserCreate(param);
        }

        internal HisDebateCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisDebateUserCreate = new HisDebateUserCreate(param);
        }

        internal bool Create(HIS_DEBATE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDebateCheck checker = new HisDebateCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsValidDebateUser(data.HIS_DEBATE_USER);
                valid = valid && checker.IsValidDebateInviteUser(data.HIS_DEBATE_INVITE_USER);
                valid = valid && checker.IsValidDebateEkipUser(data.HIS_DEBATE_EKIP_USER);
                valid = valid && checker.IsValidContentType(data);
                if (valid)
                {
                    if (!DAOWorker.HisDebateDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDebate_ThemMoiThatBai);
                        return false;
                    }
                    this.recentHisDebate = data;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                this.RollbackData();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal void RollbackData()
        {
            if (this.recentHisDebate != null)
            {
                if (!DAOWorker.HisDebateDAO.Truncate(this.recentHisDebate))
                {
                    LogSystem.Warn("Rollback du lieu HisDebate that bai, can kiem tra lai." + LogUtil.TraceData("HisDebate", this.recentHisDebate));
                }
            }
        }
    }
}
