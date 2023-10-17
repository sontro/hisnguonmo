using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarRetyFofi.Update
{
    class SarRetyFofiUpdateBehaviorEv : BeanObjectBase, ISarRetyFofiUpdate
    {
        SAR_RETY_FOFI entity;

        internal SarRetyFofiUpdateBehaviorEv(CommonParam param, SAR_RETY_FOFI data)
            : base(param)
        {
            entity = data;
        }

        bool ISarRetyFofiUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarRetyFofiDAO.Update(entity);
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
                result = result && SarRetyFofiCheckVerifyValidData.Verify(param, entity);
                result = result && SarRetyFofiCheckVerifyIsUnlock.Verify(param, entity.ID);
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
