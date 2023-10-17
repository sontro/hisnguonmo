using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using SAR.MANAGER.Core.SarFormType;

namespace SAR.MANAGER.Core.SarFormType.Lock
{
    class SarFormTypeChangeLockBehaviorEv : BeanObjectBase, ISarFormTypeChangeLock
    {
        SAR_FORM_TYPE entity;

        internal SarFormTypeChangeLockBehaviorEv(CommonParam param, SAR_FORM_TYPE data)
            : base(param)
        {
            entity = data;
        }

        bool ISarFormTypeChangeLock.Run()
        {
            bool result = false;
            try
            {
                SAR_FORM_TYPE raw = new SarFormTypeBO().Get<SAR_FORM_TYPE>(entity.ID);
                if (raw != null)
                {
                    if (raw.IS_ACTIVE.HasValue && raw.IS_ACTIVE == IMSys.DbConfig.SAR_RS.COMMON.IS_ACTIVE__TRUE)
                    {
                        raw.IS_ACTIVE = IMSys.DbConfig.SAR_RS.COMMON.IS_ACTIVE__FALSE;
                    }
                    else
                    {
                        raw.IS_ACTIVE = IMSys.DbConfig.SAR_RS.COMMON.IS_ACTIVE__TRUE;
                    }
                    result = DAOWorker.SarFormTypeDAO.Update(raw);
                    if (result) entity.IS_ACTIVE = raw.IS_ACTIVE;
                }
                else
                {
                    BugUtil.SetBugCode(param, SAR.LibraryBug.Bug.Enum.Common__KXDDDuLieuCanXuLy);
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
