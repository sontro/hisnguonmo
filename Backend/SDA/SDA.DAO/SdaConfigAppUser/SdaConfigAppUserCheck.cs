using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SDA.DAO.SdaConfigAppUser
{
    partial class SdaConfigAppUserCheck : EntityBase
    {
        public SdaConfigAppUserCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_CONFIG_APP_USER>();
        }

        private BridgeDAO<SDA_CONFIG_APP_USER> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
