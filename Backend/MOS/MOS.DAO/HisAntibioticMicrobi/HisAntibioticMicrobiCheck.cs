using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisAntibioticMicrobi
{
    partial class HisAntibioticMicrobiCheck : EntityBase
    {
        public HisAntibioticMicrobiCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTIBIOTIC_MICROBI>();
        }

        private BridgeDAO<HIS_ANTIBIOTIC_MICROBI> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
