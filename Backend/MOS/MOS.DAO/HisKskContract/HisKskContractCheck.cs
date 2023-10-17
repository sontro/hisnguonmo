using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisKskContract
{
    partial class HisKskContractCheck : EntityBase
    {
        public HisKskContractCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_CONTRACT>();
        }

        private BridgeDAO<HIS_KSK_CONTRACT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
