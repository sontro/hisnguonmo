using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAccidentResult
{
    partial class HisAccidentResultTruncate : EntityBase
    {
        public HisAccidentResultTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_RESULT>();
        }

        private BridgeDAO<HIS_ACCIDENT_RESULT> bridgeDAO;

        public bool Truncate(HIS_ACCIDENT_RESULT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ACCIDENT_RESULT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
