using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaCommuneMap.Update
{
    class SdaCommuneMapUpdateBehaviorEv : BeanObjectBase, ISdaCommuneMapUpdate
    {
        SDA_COMMUNE_MAP current;
        SDA_COMMUNE_MAP entity;

        internal SdaCommuneMapUpdateBehaviorEv(CommonParam param, SDA_COMMUNE_MAP data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaCommuneMapUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaCommuneMapDAO.Update(entity);
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
                result = result && SdaCommuneMapCheckVerifyIsUnlock.Verify(param, entity.ID, ref current);
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
