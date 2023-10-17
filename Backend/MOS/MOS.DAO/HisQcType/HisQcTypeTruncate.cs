using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisQcType
{
    partial class HisQcTypeTruncate : EntityBase
    {
        public HisQcTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_QC_TYPE>();
        }

        private BridgeDAO<HIS_QC_TYPE> bridgeDAO;

        public bool Truncate(HIS_QC_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_QC_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
