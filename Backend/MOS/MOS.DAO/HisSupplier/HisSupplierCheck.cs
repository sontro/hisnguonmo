using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisSupplier
{
    partial class HisSupplierCheck : EntityBase
    {
        public HisSupplierCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SUPPLIER>();
        }

        private BridgeDAO<HIS_SUPPLIER> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
