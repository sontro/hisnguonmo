using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SDA.DAO.SdaGroup
{
    partial class SdaGroupCheck : EntityBase
    {
        public SdaGroupCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_GROUP>();
        }

        private BridgeDAO<SDA_GROUP> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
