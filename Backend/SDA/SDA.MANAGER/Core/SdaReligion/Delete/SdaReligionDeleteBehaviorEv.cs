using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaReligion.Delete
{
    class SdaReligionDeleteBehaviorEv : BeanObjectBase, ISdaReligionDelete
    {
        SDA_RELIGION entity;

        internal SdaReligionDeleteBehaviorEv(CommonParam param, SDA_RELIGION data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaReligionDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaReligionDAO.Truncate(entity);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        bool Check()
        {
            bool result = true;
            try
            {
                result = result && SdaReligionCheckVerifyIsUnlock.Verify(param, entity.ID);
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
