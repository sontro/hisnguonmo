using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVaccinationVrty
{
    partial class HisVaccinationVrtyTruncate : EntityBase
    {
        public HisVaccinationVrtyTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACCINATION_VRTY>();
        }

        private BridgeDAO<HIS_VACCINATION_VRTY> bridgeDAO;

        public bool Truncate(HIS_VACCINATION_VRTY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_VACCINATION_VRTY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
