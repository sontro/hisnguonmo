using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisAntibioticNewReg
{
    partial class HisAntibioticNewRegCheck : EntityBase
    {
        public HisAntibioticNewRegCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTIBIOTIC_NEW_REG>();
        }

        private BridgeDAO<HIS_ANTIBIOTIC_NEW_REG> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
