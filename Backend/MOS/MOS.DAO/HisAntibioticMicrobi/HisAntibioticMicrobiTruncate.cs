using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAntibioticMicrobi
{
    partial class HisAntibioticMicrobiTruncate : EntityBase
    {
        public HisAntibioticMicrobiTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTIBIOTIC_MICROBI>();
        }

        private BridgeDAO<HIS_ANTIBIOTIC_MICROBI> bridgeDAO;

        public bool Truncate(HIS_ANTIBIOTIC_MICROBI data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ANTIBIOTIC_MICROBI> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
