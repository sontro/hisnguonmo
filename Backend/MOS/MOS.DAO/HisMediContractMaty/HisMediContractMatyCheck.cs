using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMediContractMaty
{
    partial class HisMediContractMatyCheck : EntityBase
    {
        public HisMediContractMatyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_CONTRACT_MATY>();
        }

        private BridgeDAO<HIS_MEDI_CONTRACT_MATY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
