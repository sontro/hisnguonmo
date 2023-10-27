using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsAppOtpType.Get.Ev
{
    class AcsAppOtpTypeGetEvBehaviorByCode : BeanObjectBase, IAcsAppOtpTypeGetEv
    {
        string code;

        internal AcsAppOtpTypeGetEvBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        ACS_APP_OTP_TYPE IAcsAppOtpTypeGetEv.Run()
        {
            try
            {
                return DAOWorker.AcsAppOtpTypeDAO.GetByCode(code, new AcsAppOtpTypeFilterQuery().Query());
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
