using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVaccinationVrpl
{
    partial class HisVaccinationVrplTruncate : EntityBase
    {
        public HisVaccinationVrplTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACCINATION_VRPL>();
        }

        private BridgeDAO<HIS_VACCINATION_VRPL> bridgeDAO;

        public bool Truncate(HIS_VACCINATION_VRPL data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_VACCINATION_VRPL> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
