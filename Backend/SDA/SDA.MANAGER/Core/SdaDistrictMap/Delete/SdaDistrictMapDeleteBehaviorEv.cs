using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaDistrictMap.Delete
{
    class SdaDistrictMapDeleteBehaviorEv : BeanObjectBase, ISdaDistrictMapDelete
    {
        SDA_DISTRICT_MAP entity;

        internal SdaDistrictMapDeleteBehaviorEv(CommonParam param, SDA_DISTRICT_MAP data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaDistrictMapDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaDistrictMapDAO.Truncate(entity);
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
                result = result && SdaDistrictMapCheckVerifyIsUnlock.Verify(param, entity.ID, ref entity);
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
