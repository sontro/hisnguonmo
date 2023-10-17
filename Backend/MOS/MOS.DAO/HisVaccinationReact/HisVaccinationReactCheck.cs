using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisVaccinationReact
{
    partial class HisVaccinationReactCheck : EntityBase
    {
        public HisVaccinationReactCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACCINATION_REACT>();
        }

        private BridgeDAO<HIS_VACCINATION_REACT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
