using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisAntibioticOldReg
{
    partial class HisAntibioticOldRegCheck : EntityBase
    {
        public HisAntibioticOldRegCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTIBIOTIC_OLD_REG>();
        }

        private BridgeDAO<HIS_ANTIBIOTIC_OLD_REG> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
