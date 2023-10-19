using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsControl.Create
{
    class AcsControlCreateBehaviorListEv : BeanObjectBase, IAcsControlCreate
    {
        List<ACS_CONTROL> entities;

        internal AcsControlCreateBehaviorListEv(CommonParam param, List<ACS_CONTROL> datas)
            : base(param)
        {
            entities = datas;
        }

        bool IAcsControlCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsControlDAO.CreateList(entities);
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
                result = result && AcsControlCheckVerifyValidData.Verify(param, entities);
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
