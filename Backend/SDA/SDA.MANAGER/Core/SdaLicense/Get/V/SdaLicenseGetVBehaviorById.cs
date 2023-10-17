using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaLicense.Get.V
{
    class SdaLicenseGetVBehaviorById : BeanObjectBase, ISdaLicenseGetV
    {
        long id;

        internal SdaLicenseGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_SDA_LICENSE ISdaLicenseGetV.Run()
        {
            try
            {
                return DAOWorker.SdaLicenseDAO.GetViewById(id, new SdaLicenseViewFilterQuery().Query());
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
