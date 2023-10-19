using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsApplication.Create
{
    class AcsApplicationCreateBehaviorListEv : BeanObjectBase, IAcsApplicationCreate
    {
        List<ACS_APPLICATION> entities;

        internal AcsApplicationCreateBehaviorListEv(CommonParam param, List<ACS_APPLICATION> datas)
            : base(param)
        {
            entities = datas;
        }

        bool IAcsApplicationCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsApplicationDAO.CreateList(entities);
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
                result = result && AcsApplicationCheckVerifyValidData.Verify(param, entities);
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
