using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaGroupType.Get.V
{
    class SdaGroupTypeGetVBehaviorById : BeanObjectBase, ISdaGroupTypeGetV
    {
        long id;

        internal SdaGroupTypeGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_SDA_GROUP_TYPE ISdaGroupTypeGetV.Run()
        {
            try
            {
                return DAOWorker.SdaGroupTypeDAO.GetViewById(id, new SdaGroupTypeViewFilterQuery().Query());
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
