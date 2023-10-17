using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaCustomizeUi.Delete
{
    class SdaCustomizeUiDeleteBehaviorEv : BeanObjectBase, ISdaCustomizeUiDelete
    {
        SDA_CUSTOMIZE_UI entity;

        internal SdaCustomizeUiDeleteBehaviorEv(CommonParam param, SDA_CUSTOMIZE_UI data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaCustomizeUiDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaCustomizeUiDAO.Truncate(entity);
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
                result = result && SdaCustomizeUiCheckVerifyIsUnlock.Verify(param, entity.ID, ref entity);
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
