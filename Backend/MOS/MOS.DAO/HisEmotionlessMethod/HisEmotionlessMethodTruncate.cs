using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEmotionlessMethod
{
    partial class HisEmotionlessMethodTruncate : EntityBase
    {
        public HisEmotionlessMethodTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMOTIONLESS_METHOD>();
        }

        private BridgeDAO<HIS_EMOTIONLESS_METHOD> bridgeDAO;

        public bool Truncate(HIS_EMOTIONLESS_METHOD data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_EMOTIONLESS_METHOD> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
