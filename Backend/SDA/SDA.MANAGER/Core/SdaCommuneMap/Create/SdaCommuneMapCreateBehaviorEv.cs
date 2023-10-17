using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaCommuneMap.Create
{
    class SdaCommuneMapCreateBehaviorEv : BeanObjectBase, ISdaCommuneMapCreate
    {
        SDA_COMMUNE_MAP entity;

        internal SdaCommuneMapCreateBehaviorEv(CommonParam param, SDA_COMMUNE_MAP data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaCommuneMapCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaCommuneMapDAO.Create(entity);
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
                result = result && SdaCommuneMapCheckVerifyValidData.Verify(param, entity);
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
