using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SDA.DAO.SdaCommuneMap
{
    partial class SdaCommuneMapCheck : EntityBase
    {
        public SdaCommuneMapCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_COMMUNE_MAP>();
        }

        private BridgeDAO<SDA_COMMUNE_MAP> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
