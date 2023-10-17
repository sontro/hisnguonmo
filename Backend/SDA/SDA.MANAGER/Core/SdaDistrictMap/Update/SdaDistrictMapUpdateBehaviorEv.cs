using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaDistrictMap.Update
{
    class SdaDistrictMapUpdateBehaviorEv : BeanObjectBase, ISdaDistrictMapUpdate
    {
        SDA_DISTRICT_MAP current;
        SDA_DISTRICT_MAP entity;

        internal SdaDistrictMapUpdateBehaviorEv(CommonParam param, SDA_DISTRICT_MAP data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaDistrictMapUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaDistrictMapDAO.Update(entity);
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
                result = result && SdaDistrictMapCheckVerifyValidData.Verify(param, entity);
                result = result && SdaDistrictMapCheckVerifyIsUnlock.Verify(param, entity.ID, ref current);
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
