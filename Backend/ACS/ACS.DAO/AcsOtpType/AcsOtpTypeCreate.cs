using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace ACS.DAO.AcsOtpType
{
    partial class AcsOtpTypeCreate : EntityBase
    {
        public AcsOtpTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_OTP_TYPE>();
        }

        private BridgeDAO<ACS_OTP_TYPE> bridgeDAO;

        public bool Create(ACS_OTP_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<ACS_OTP_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
