using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisSurgRemuDetail
{
    partial class HisSurgRemuDetailCheck : EntityBase
    {
        public HisSurgRemuDetailCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SURG_REMU_DETAIL>();
        }

        private BridgeDAO<HIS_SURG_REMU_DETAIL> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
