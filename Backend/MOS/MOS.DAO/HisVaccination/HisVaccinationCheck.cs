using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisVaccination
{
    partial class HisVaccinationCheck : EntityBase
    {
        public HisVaccinationCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACCINATION>();
        }

        private BridgeDAO<HIS_VACCINATION> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
