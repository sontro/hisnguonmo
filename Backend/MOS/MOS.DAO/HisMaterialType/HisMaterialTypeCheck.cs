using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMaterialType
{
    partial class HisMaterialTypeCheck : EntityBase
    {
        public HisMaterialTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MATERIAL_TYPE>();
        }

        private BridgeDAO<HIS_MATERIAL_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
