using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisEmteMaterialType
{
    partial class HisEmteMaterialTypeCheck : EntityBase
    {
        public HisEmteMaterialTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMTE_MATERIAL_TYPE>();
        }

        private BridgeDAO<HIS_EMTE_MATERIAL_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
