using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisVaccinationStt
{
    partial class HisVaccinationSttCheck : EntityBase
    {
        public HisVaccinationSttCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACCINATION_STT>();
        }

        private BridgeDAO<HIS_VACCINATION_STT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
