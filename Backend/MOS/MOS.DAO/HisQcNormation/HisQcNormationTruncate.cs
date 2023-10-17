using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisQcNormation
{
    partial class HisQcNormationTruncate : EntityBase
    {
        public HisQcNormationTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_QC_NORMATION>();
        }

        private BridgeDAO<HIS_QC_NORMATION> bridgeDAO;

        public bool Truncate(HIS_QC_NORMATION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_QC_NORMATION> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
