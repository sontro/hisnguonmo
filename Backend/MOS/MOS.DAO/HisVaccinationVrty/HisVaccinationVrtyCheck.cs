using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisVaccinationVrty
{
    partial class HisVaccinationVrtyCheck : EntityBase
    {
        public HisVaccinationVrtyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACCINATION_VRTY>();
        }

        private BridgeDAO<HIS_VACCINATION_VRTY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
