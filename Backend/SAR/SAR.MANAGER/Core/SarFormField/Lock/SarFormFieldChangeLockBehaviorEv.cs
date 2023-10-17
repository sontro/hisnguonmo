using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using SAR.MANAGER.Core.SarFormField;

namespace SAR.MANAGER.Core.SarFormField.Lock
{
    class SarFormFieldChangeLockBehaviorEv : BeanObjectBase, ISarFormFieldChangeLock
    {
        SAR_FORM_FIELD entity;

        internal SarFormFieldChangeLockBehaviorEv(CommonParam param, SAR_FORM_FIELD data)
            : base(param)
        {
            entity = data;
        }

        bool ISarFormFieldChangeLock.Run()
        {
            bool result = false;
            try
            {
                SAR_FORM_FIELD raw = new SarFormFieldBO().Get<SAR_FORM_FIELD>(entity.ID);
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
                    result = DAOWorker.SarFormFieldDAO.Update(raw);
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
