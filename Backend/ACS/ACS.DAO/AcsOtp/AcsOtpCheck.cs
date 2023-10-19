using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace ACS.DAO.AcsOtp
{
    partial class AcsOtpCheck : EntityBase
    {
        public AcsOtpCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_OTP>();
        }

        private BridgeDAO<ACS_OTP> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
