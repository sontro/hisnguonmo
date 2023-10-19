using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace ACS.DAO.AcsAppOtpType
{
    partial class AcsAppOtpTypeCreate : EntityBase
    {
        public AcsAppOtpTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_APP_OTP_TYPE>();
        }

        private BridgeDAO<ACS_APP_OTP_TYPE> bridgeDAO;

        public bool Create(ACS_APP_OTP_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<ACS_APP_OTP_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
