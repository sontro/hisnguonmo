using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaLicense.Get.V
{
    class SdaLicenseGetVBehaviorByCode : BeanObjectBase, ISdaLicenseGetV
    {
        string code;

        internal SdaLicenseGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_SDA_LICENSE ISdaLicenseGetV.Run()
        {
            try
            {
                return DAOWorker.SdaLicenseDAO.GetViewByCode(code, new SdaLicenseViewFilterQuery().Query());
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
