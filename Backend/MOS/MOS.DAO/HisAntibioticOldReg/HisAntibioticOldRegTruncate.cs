using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAntibioticOldReg
{
    partial class HisAntibioticOldRegTruncate : EntityBase
    {
        public HisAntibioticOldRegTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTIBIOTIC_OLD_REG>();
        }

        private BridgeDAO<HIS_ANTIBIOTIC_OLD_REG> bridgeDAO;

        public bool Truncate(HIS_ANTIBIOTIC_OLD_REG data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ANTIBIOTIC_OLD_REG> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
