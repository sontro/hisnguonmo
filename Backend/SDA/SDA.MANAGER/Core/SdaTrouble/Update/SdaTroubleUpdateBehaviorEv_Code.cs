using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaTrouble.Update
{
    class SdaTroubleUpdateBehaviorEv : BeanObjectBase, ISdaTroubleUpdate
    {
        SDA_TROUBLE entity;

        internal SdaTroubleUpdateBehaviorEv(CommonParam param, SDA_TROUBLE data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaTroubleUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaTroubleDAO.Update(entity);
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
                result = result && SdaTroubleCheckVerifyIsUnlock.Verify(param, entity.ID);
                result = result && SdaTroubleCheckVerifyExistsCode.Verify(param, entity.TROUBLE_CODE, entity.ID);
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
