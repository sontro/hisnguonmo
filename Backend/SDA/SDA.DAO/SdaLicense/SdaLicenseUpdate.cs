using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaLicense
{
    partial class SdaLicenseUpdate : EntityBase
    {
        public SdaLicenseUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_LICENSE>();
        }

        private BridgeDAO<SDA_LICENSE> bridgeDAO;

        public bool Update(SDA_LICENSE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SDA_LICENSE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
