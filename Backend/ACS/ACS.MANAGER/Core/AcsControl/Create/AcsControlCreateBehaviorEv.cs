using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsControl.Create
{
    class AcsControlCreateBehaviorEv : BeanObjectBase, IAcsControlCreate
    {
        ACS_CONTROL entity;

        internal AcsControlCreateBehaviorEv(CommonParam param, ACS_CONTROL data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsControlCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsControlDAO.Create(entity);
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
