using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMaterialMaterial
{
    partial class HisMaterialMaterialCheck : EntityBase
    {
        public HisMaterialMaterialCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MATERIAL_MATERIAL>();
        }

        private BridgeDAO<HIS_MATERIAL_MATERIAL> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
