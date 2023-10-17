using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaTrouble.Create
{
    class SdaTroubleCreateBehaviorEv : BeanObjectBase, ISdaTroubleCreate
    {
        SDA_TROUBLE entity;

        internal SdaTroubleCreateBehaviorEv(CommonParam param, SDA_TROUBLE data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaTroubleCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaTroubleDAO.Create(entity);
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
                result = result && SdaTroubleCheckVerifyValidData.Verify(param, entity);
                result = result && SdaTroubleCheckVerifyExistsCode.Verify(param, entity.TROUBLE_CODE, null);
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
