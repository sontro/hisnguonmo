using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaCommuneMap.Delete
{
    class SdaCommuneMapDeleteBehaviorEv : BeanObjectBase, ISdaCommuneMapDelete
    {
        SDA_COMMUNE_MAP entity;

        internal SdaCommuneMapDeleteBehaviorEv(CommonParam param, SDA_COMMUNE_MAP data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaCommuneMapDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaCommuneMapDAO.Truncate(entity);
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
                result = result && SdaCommuneMapCheckVerifyIsUnlock.Verify(param, entity.ID, ref entity);
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
