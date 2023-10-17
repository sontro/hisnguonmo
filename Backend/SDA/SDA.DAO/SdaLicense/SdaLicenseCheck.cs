using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SDA.DAO.SdaLicense
{
    partial class SdaLicenseCheck : EntityBase
    {
        public SdaLicenseCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_LICENSE>();
        }

        private BridgeDAO<SDA_LICENSE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
