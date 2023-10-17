using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaTrouble.Delete
{
    class SdaTroubleDeleteBehaviorEv : BeanObjectBase, ISdaTroubleDelete
    {
        SDA_TROUBLE entity;

        internal SdaTroubleDeleteBehaviorEv(CommonParam param, SDA_TROUBLE data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaTroubleDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaTroubleDAO.Truncate(entity);
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
                result = result && SdaTroubleCheckVerifyIsUnlock.Verify(param, entity.ID);
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
