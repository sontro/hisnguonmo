using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisIcd
{
    partial class HisIcdTruncate : EntityBase
    {
        public HisIcdTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ICD>();
        }

        private BridgeDAO<HIS_ICD> bridgeDAO;

        public bool Truncate(HIS_ICD data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ICD> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
