using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsControl.Update
{
    class AcsControlUpdateBehaviorEv : BeanObjectBase, IAcsControlUpdate
    {
        ACS_CONTROL entity;

        internal AcsControlUpdateBehaviorEv(CommonParam param, ACS_CONTROL data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsControlUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsControlDAO.Update(entity);
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
                result = result && AcsControlCheckVerifyValidData.Verify(param, entity);
                result = result && AcsControlCheckVerifyIsUnlock.Verify(param, entity.ID);
                result = result && AcsControlCheckVerifyExistsCode.Verify(param, entity.CONTROL_CODE, entity.ID);
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
