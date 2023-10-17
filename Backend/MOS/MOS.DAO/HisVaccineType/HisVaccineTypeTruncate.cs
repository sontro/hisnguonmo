using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVaccineType
{
    partial class HisVaccineTypeTruncate : EntityBase
    {
        public HisVaccineTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACCINE_TYPE>();
        }

        private BridgeDAO<HIS_VACCINE_TYPE> bridgeDAO;

        public bool Truncate(HIS_VACCINE_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_VACCINE_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
