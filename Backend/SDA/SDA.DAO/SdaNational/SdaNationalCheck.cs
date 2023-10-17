using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SDA.DAO.SdaNational
{
    partial class SdaNationalCheck : EntityBase
    {
        public SdaNationalCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_NATIONAL>();
        }

        private BridgeDAO<SDA_NATIONAL> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
