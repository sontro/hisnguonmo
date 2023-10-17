using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaEventLog.Create
{
    class SdaEventLogCreateBehaviorEv : BeanObjectBase, ISdaEventLogCreate
    {
        SDA_EVENT_LOG entity;

        internal SdaEventLogCreateBehaviorEv(CommonParam param, SDA_EVENT_LOG data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaEventLogCreate.Run()
        {
            bool result = false;
            try
            {
                if (Inventec.Common.String.CountVi.Count(entity.DESCRIPTION) > 4000)
                {
                    entity.DESCRIPTION = Inventec.Common.String.CountVi.SubStringVi(entity.DESCRIPTION, 4000 - 4) + "...";
                }
                result = Check() && DAOWorker.SdaEventLogDAO.Create(entity);
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
                result = result && SdaEventLogCheckVerifyValidData.Verify(param, entity);
                //result = result && SdaEventLogCheckVerifyExistsCode.Verify(param, entity.EVENT_LOG_CODE, null);
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
