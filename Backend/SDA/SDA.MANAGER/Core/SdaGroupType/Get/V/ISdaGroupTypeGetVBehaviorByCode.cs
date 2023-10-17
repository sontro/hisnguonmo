using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaGroupType.Get.V
{
    class SdaGroupTypeGetVBehaviorByCode : BeanObjectBase, ISdaGroupTypeGetV
    {
        string code;

        internal SdaGroupTypeGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_SDA_GROUP_TYPE ISdaGroupTypeGetV.Run()
        {
            try
            {
                return DAOWorker.SdaGroupTypeDAO.GetViewByCode(code, new SdaGroupTypeViewFilterQuery().Query());
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
