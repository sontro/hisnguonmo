using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMaterialTypeMap
{
    partial class HisMaterialTypeMapCheck : EntityBase
    {
        public HisMaterialTypeMapCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MATERIAL_TYPE_MAP>();
        }

        private BridgeDAO<HIS_MATERIAL_TYPE_MAP> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
