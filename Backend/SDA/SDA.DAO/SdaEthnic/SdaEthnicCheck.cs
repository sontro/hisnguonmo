using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SDA.DAO.SdaEthnic
{
    partial class SdaEthnicCheck : EntityBase
    {
        public SdaEthnicCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_ETHNIC>();
        }

        private BridgeDAO<SDA_ETHNIC> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
