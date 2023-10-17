using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisManufacturer
{
    partial class HisManufacturerCheck : EntityBase
    {
        public HisManufacturerCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MANUFACTURER>();
        }

        private BridgeDAO<HIS_MANUFACTURER> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
