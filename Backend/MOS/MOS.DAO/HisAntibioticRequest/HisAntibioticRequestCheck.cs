using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisAntibioticRequest
{
    partial class HisAntibioticRequestCheck : EntityBase
    {
        public HisAntibioticRequestCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTIBIOTIC_REQUEST>();
        }

        private BridgeDAO<HIS_ANTIBIOTIC_REQUEST> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
