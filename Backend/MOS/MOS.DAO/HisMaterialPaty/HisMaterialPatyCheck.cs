using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMaterialPaty
{
    partial class HisMaterialPatyCheck : EntityBase
    {
        public HisMaterialPatyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MATERIAL_PATY>();
        }

        private BridgeDAO<HIS_MATERIAL_PATY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
