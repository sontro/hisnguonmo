using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SDA.DAO.SdaConfigApp
{
    partial class SdaConfigAppCheck : EntityBase
    {
        public SdaConfigAppCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_CONFIG_APP>();
        }

        private BridgeDAO<SDA_CONFIG_APP> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
