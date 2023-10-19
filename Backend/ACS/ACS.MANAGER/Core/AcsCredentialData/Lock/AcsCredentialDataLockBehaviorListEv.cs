using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using ACS.MANAGER.Core.AcsCredentialData;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsCredentialData.Lock
{
    class AcsCredentialDataLockBehaviorListEv : BeanObjectBase, IAcsCredentialDataChangeLock
    {
        List<ACS_CREDENTIAL_DATA> entitys;

        internal AcsCredentialDataLockBehaviorListEv(CommonParam param, List<ACS_CREDENTIAL_DATA> data)
            : base(param)
        {
            entitys = data;
        }

        bool IAcsCredentialDataChangeLock.Run()
        {
            bool result = false;
            try
            {
                foreach (var entity in entitys)
                {
                    ACS_CREDENTIAL_DATA raw = new AcsCredentialDataBO().Get<ACS_CREDENTIAL_DATA>(entity.ID);
                    if (raw != null)
                    {
                        raw.IS_ACTIVE = IMSys.DbConfig.ACS_RS.COMMON.IS_ACTIVE__FALSE;
                        result = DAOWorker.AcsCredentialDataDAO.Update(raw);
                    }
                    else
                    {
                        BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.Common__KXDDDuLieuCanXuLy);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
