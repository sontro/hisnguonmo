using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMediContractMety
{
    partial class HisMediContractMetyCheck : EntityBase
    {
        public HisMediContractMetyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_CONTRACT_METY>();
        }

        private BridgeDAO<HIS_MEDI_CONTRACT_METY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
