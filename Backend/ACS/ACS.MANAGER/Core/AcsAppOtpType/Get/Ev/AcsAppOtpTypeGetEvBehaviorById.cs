using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsAppOtpType.Get.Ev
{
    class AcsAppOtpTypeGetEvBehaviorById : BeanObjectBase, IAcsAppOtpTypeGetEv
    {
        long id;

        internal AcsAppOtpTypeGetEvBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        ACS_APP_OTP_TYPE IAcsAppOtpTypeGetEv.Run()
        {
            try
            {
                return DAOWorker.AcsAppOtpTypeDAO.GetById(id, new AcsAppOtpTypeFilterQuery().Query());
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
