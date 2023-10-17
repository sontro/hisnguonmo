using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisVaccinationResult
{
    partial class HisVaccinationResultCheck : EntityBase
    {
        public HisVaccinationResultCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACCINATION_RESULT>();
        }

        private BridgeDAO<HIS_VACCINATION_RESULT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
