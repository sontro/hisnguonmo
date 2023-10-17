using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SDA.DAO.SdaCommune
{
    partial class SdaCommuneCheck : EntityBase
    {
        public SdaCommuneCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_COMMUNE>();
        }

        private BridgeDAO<SDA_COMMUNE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
