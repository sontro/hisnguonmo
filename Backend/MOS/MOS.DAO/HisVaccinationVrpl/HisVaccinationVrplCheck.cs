using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisVaccinationVrpl
{
    partial class HisVaccinationVrplCheck : EntityBase
    {
        public HisVaccinationVrplCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACCINATION_VRPL>();
        }

        private BridgeDAO<HIS_VACCINATION_VRPL> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
