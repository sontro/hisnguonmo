using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaCommune.Create
{
    class SdaCommuneCreateBehaviorListEv : BeanObjectBase, ISdaCommuneCreate
    {
        List<SDA_COMMUNE> entities;

        internal SdaCommuneCreateBehaviorListEv(CommonParam param, List<SDA_COMMUNE> datas)
            : base(param)
        {
            entities = datas;
        }

        bool ISdaCommuneCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaCommuneDAO.CreateList(entities);
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
                result = result && SdaCommuneCheckVerifyValidData.Verify(param, entities);
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
