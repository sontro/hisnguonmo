using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVaccinationStt
{
    partial class HisVaccinationSttTruncate : EntityBase
    {
        public HisVaccinationSttTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACCINATION_STT>();
        }

        private BridgeDAO<HIS_VACCINATION_STT> bridgeDAO;

        public bool Truncate(HIS_VACCINATION_STT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_VACCINATION_STT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
