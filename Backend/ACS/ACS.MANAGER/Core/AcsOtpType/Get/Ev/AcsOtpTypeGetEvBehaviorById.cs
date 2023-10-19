using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsOtpType.Get.Ev
{
    class AcsOtpTypeGetEvBehaviorById : BeanObjectBase, IAcsOtpTypeGetEv
    {
        long id;

        internal AcsOtpTypeGetEvBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        ACS_OTP_TYPE IAcsOtpTypeGetEv.Run()
        {
            try
            {
                return DAOWorker.AcsOtpTypeDAO.GetById(id, new AcsOtpTypeFilterQuery().Query());
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
