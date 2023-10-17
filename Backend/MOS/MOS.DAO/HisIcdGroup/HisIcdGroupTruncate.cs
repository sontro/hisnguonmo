using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisIcdGroup
{
    partial class HisIcdGroupTruncate : EntityBase
    {
        public HisIcdGroupTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ICD_GROUP>();
        }

        private BridgeDAO<HIS_ICD_GROUP> bridgeDAO;

        public bool Truncate(HIS_ICD_GROUP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ICD_GROUP> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
