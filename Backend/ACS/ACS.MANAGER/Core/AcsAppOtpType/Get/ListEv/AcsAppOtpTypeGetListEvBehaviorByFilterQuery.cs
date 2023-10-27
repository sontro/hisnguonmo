using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsAppOtpType.Get.ListEv
{
    class AcsAppOtpTypeGetListEvBehaviorByFilterQuery : BeanObjectBase, IAcsAppOtpTypeGetListEv
    {
        AcsAppOtpTypeFilterQuery filterQuery;

        internal AcsAppOtpTypeGetListEvBehaviorByFilterQuery(CommonParam param, AcsAppOtpTypeFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<ACS_APP_OTP_TYPE> IAcsAppOtpTypeGetListEv.Run()
        {
            try
            {
                return DAOWorker.AcsAppOtpTypeDAO.Get(filterQuery.Query(), param);
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
