using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsOtp
{
    partial class AcsOtpTruncate : EntityBase
    {
        public AcsOtpTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_OTP>();
        }

        private BridgeDAO<ACS_OTP> bridgeDAO;

        public bool Truncate(ACS_OTP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<ACS_OTP> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
