using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsOtpType.Get.ListEv
{
    class AcsOtpTypeGetListEvBehaviorByFilterQuery : BeanObjectBase, IAcsOtpTypeGetListEv
    {
        AcsOtpTypeFilterQuery filterQuery;

        internal AcsOtpTypeGetListEvBehaviorByFilterQuery(CommonParam param, AcsOtpTypeFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<ACS_OTP_TYPE> IAcsOtpTypeGetListEv.Run()
        {
            try
            {
                return DAOWorker.AcsOtpTypeDAO.Get(filterQuery.Query(), param);
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
