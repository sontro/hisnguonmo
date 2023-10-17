using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEmotionlessResult
{
    partial class HisEmotionlessResultTruncate : EntityBase
    {
        public HisEmotionlessResultTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMOTIONLESS_RESULT>();
        }

        private BridgeDAO<HIS_EMOTIONLESS_RESULT> bridgeDAO;

        public bool Truncate(HIS_EMOTIONLESS_RESULT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_EMOTIONLESS_RESULT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
