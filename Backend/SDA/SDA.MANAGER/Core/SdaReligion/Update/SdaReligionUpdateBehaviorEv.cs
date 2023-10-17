using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaReligion.Update
{
    class SdaReligionUpdateBehaviorEv : BeanObjectBase, ISdaReligionUpdate
    {
        SDA_RELIGION entity;

        internal SdaReligionUpdateBehaviorEv(CommonParam param, SDA_RELIGION data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaReligionUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaReligionDAO.Update(entity);
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
                result = result && SdaReligionCheckVerifyValidData.Verify(param, entity);
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
