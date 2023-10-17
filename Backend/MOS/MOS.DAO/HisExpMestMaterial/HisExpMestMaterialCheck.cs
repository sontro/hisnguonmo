using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisExpMestMaterial
{
    partial class HisExpMestMaterialCheck : EntityBase
    {
        public HisExpMestMaterialCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST_MATERIAL>();
        }

        private BridgeDAO<HIS_EXP_MEST_MATERIAL> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
