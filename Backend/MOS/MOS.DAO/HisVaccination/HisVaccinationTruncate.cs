using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVaccination
{
    partial class HisVaccinationTruncate : EntityBase
    {
        public HisVaccinationTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACCINATION>();
        }

        private BridgeDAO<HIS_VACCINATION> bridgeDAO;

        public bool Truncate(HIS_VACCINATION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_VACCINATION> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
