using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisPackingType
{
    partial class HisPackingTypeCheck : EntityBase
    {
        public HisPackingTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PACKING_TYPE>();
        }

        private BridgeDAO<HIS_PACKING_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
