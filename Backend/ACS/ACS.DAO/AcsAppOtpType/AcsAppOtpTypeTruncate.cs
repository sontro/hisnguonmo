using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsAppOtpType
{
    partial class AcsAppOtpTypeTruncate : EntityBase
    {
        public AcsAppOtpTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_APP_OTP_TYPE>();
        }

        private BridgeDAO<ACS_APP_OTP_TYPE> bridgeDAO;

        public bool Truncate(ACS_APP_OTP_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<ACS_APP_OTP_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
