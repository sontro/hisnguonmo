using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAntibioticRequest
{
    partial class HisAntibioticRequestTruncate : EntityBase
    {
        public HisAntibioticRequestTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTIBIOTIC_REQUEST>();
        }

        private BridgeDAO<HIS_ANTIBIOTIC_REQUEST> bridgeDAO;

        public bool Truncate(HIS_ANTIBIOTIC_REQUEST data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ANTIBIOTIC_REQUEST> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
