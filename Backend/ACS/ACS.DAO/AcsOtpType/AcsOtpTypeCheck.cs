using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace ACS.DAO.AcsOtpType
{
    partial class AcsOtpTypeCheck : EntityBase
    {
        public AcsOtpTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_OTP_TYPE>();
        }

        private BridgeDAO<ACS_OTP_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
