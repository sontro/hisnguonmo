using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisImpMestMaterial
{
    partial class HisImpMestMaterialCheck : EntityBase
    {
        public HisImpMestMaterialCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_MEST_MATERIAL>();
        }

        private BridgeDAO<HIS_IMP_MEST_MATERIAL> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
