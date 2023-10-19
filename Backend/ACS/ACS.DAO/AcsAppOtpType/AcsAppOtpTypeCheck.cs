using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace ACS.DAO.AcsAppOtpType
{
    partial class AcsAppOtpTypeCheck : EntityBase
    {
        public AcsAppOtpTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_APP_OTP_TYPE>();
        }

        private BridgeDAO<ACS_APP_OTP_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
