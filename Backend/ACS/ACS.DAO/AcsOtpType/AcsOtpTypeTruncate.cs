using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsOtpType
{
    partial class AcsOtpTypeTruncate : EntityBase
    {
        public AcsOtpTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_OTP_TYPE>();
        }

        private BridgeDAO<ACS_OTP_TYPE> bridgeDAO;

        public bool Truncate(ACS_OTP_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<ACS_OTP_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
