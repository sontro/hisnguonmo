using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisBidMaterialType
{
    partial class HisBidMaterialTypeCheck : EntityBase
    {
        public HisBidMaterialTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BID_MATERIAL_TYPE>();
        }

        private BridgeDAO<HIS_BID_MATERIAL_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
