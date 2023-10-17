using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMetyProduct
{
    partial class HisMetyProductCheck : EntityBase
    {
        public HisMetyProductCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_METY_PRODUCT>();
        }

        private BridgeDAO<HIS_METY_PRODUCT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
