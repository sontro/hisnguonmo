using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsOtpType.Get.Ev
{
    class AcsOtpTypeGetEvBehaviorByCode : BeanObjectBase, IAcsOtpTypeGetEv
    {
        string code;

        internal AcsOtpTypeGetEvBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        ACS_OTP_TYPE IAcsOtpTypeGetEv.Run()
        {
            try
            {
                return DAOWorker.AcsOtpTypeDAO.GetByCode(code, new AcsOtpTypeFilterQuery().Query());
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
