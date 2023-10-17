using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using SDA.MANAGER.Core.SdaMetadata;

namespace SDA.MANAGER.Core.SdaMetadata.Lock
{
    class SdaMetadataChangeLockBehaviorEv : BeanObjectBase, ISdaMetadataChangeLock
    {
        SDA_METADATA entity;

        internal SdaMetadataChangeLockBehaviorEv(CommonParam param, SDA_METADATA data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaMetadataChangeLock.Run()
        {
            bool result = false;
            try
            {
                SDA_METADATA raw = new SdaMetadataBO().Get<SDA_METADATA>(entity.ID);
                if (raw != null)
                {
                    if (raw.IS_ACTIVE.HasValue && raw.IS_ACTIVE == IMSys.DbConfig.SDA_RS.COMMON.IS_ACTIVE__TRUE)
                    {
                        raw.IS_ACTIVE = IMSys.DbConfig.SDA_RS.COMMON.IS_ACTIVE__FALSE;
                    }
                    else
                    {
                        raw.IS_ACTIVE = IMSys.DbConfig.SDA_RS.COMMON.IS_ACTIVE__TRUE;
                    }
                    result = DAOWorker.SdaMetadataDAO.Update(raw);
                    if (result) entity.IS_ACTIVE = raw.IS_ACTIVE;
                }
                else
                {
                    BugUtil.SetBugCode(param, SDA.LibraryBug.Bug.Enum.Common__KXDDDuLieuCanXuLy);
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
