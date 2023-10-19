using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsOtp
{
    partial class AcsOtpUpdate : EntityBase
    {
        public AcsOtpUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_OTP>();
        }

        private BridgeDAO<ACS_OTP> bridgeDAO;

        public bool Update(ACS_OTP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<ACS_OTP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
