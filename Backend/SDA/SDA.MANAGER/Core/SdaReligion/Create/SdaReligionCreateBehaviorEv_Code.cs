using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaReligion.Create
{
    class SdaReligionCreateBehaviorEv : BeanObjectBase, ISdaReligionCreate
    {
        SDA_RELIGION entity;

        internal SdaReligionCreateBehaviorEv(CommonParam param, SDA_RELIGION data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaReligionCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaReligionDAO.Create(entity);
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
                result = result && SdaReligionCheckVerifyExistsCode.Verify(param, entity.RELIGION_CODE, null);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
