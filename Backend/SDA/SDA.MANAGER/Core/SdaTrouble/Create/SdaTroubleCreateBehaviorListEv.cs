using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaTrouble.Create
{
    class SdaTroubleCreateBehaviorListEv : BeanObjectBase, ISdaTroubleCreate
    {
        List<SDA_TROUBLE> entities;

        internal SdaTroubleCreateBehaviorListEv(CommonParam param, List<SDA_TROUBLE> datas)
            : base(param)
        {
            entities = datas;
        }

        bool ISdaTroubleCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaTroubleDAO.CreateList(entities);
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
                result = result && SdaTroubleCheckVerifyValidData.Verify(param, entities);
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
