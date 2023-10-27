using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace ACS.DAO.AcsOtp
{
    partial class AcsOtpCreate : EntityBase
    {
        public AcsOtpCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_OTP>();
        }

        private BridgeDAO<ACS_OTP> bridgeDAO;

        public bool Create(ACS_OTP data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<ACS_OTP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
