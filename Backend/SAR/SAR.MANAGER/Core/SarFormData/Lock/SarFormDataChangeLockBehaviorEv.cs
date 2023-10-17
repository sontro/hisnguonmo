using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using SAR.MANAGER.Core.SarFormData;

namespace SAR.MANAGER.Core.SarFormData.Lock
{
    class SarFormDataChangeLockBehaviorEv : BeanObjectBase, ISarFormDataChangeLock
    {
        SAR_FORM_DATA entity;

        internal SarFormDataChangeLockBehaviorEv(CommonParam param, SAR_FORM_DATA data)
            : base(param)
        {
            entity = data;
        }

        bool ISarFormDataChangeLock.Run()
        {
            bool result = false;
            try
            {
                SAR_FORM_DATA raw = new SarFormDataBO().Get<SAR_FORM_DATA>(entity.ID);
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
                    result = DAOWorker.SarFormDataDAO.Update(raw);
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
