using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SDA.DAO.SdaConfig
{
    partial class SdaConfigCheck : EntityBase
    {
        public SdaConfigCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_CONFIG>();
        }

        private BridgeDAO<SDA_CONFIG> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
