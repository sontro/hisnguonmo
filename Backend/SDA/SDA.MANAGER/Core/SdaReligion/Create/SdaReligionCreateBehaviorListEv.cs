using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaReligion.Create
{
    class SdaReligionCreateBehaviorListEv : BeanObjectBase, ISdaReligionCreate
    {
        List<SDA_RELIGION> entities;

        internal SdaReligionCreateBehaviorListEv(CommonParam param, List<SDA_RELIGION> datas)
            : base(param)
        {
            entities = datas;
        }

        bool ISdaReligionCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaReligionDAO.CreateList(entities);
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
                result = result && SdaReligionCheckVerifyValidData.Verify(param, entities);
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
