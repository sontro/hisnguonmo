using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SDA.DAO.SdaLicense
{
    partial class SdaLicenseCreate : EntityBase
    {
        public SdaLicenseCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_LICENSE>();
        }

        private BridgeDAO<SDA_LICENSE> bridgeDAO;

        public bool Create(SDA_LICENSE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SDA_LICENSE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
