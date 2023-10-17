using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEmotionlessResult
{
    partial class HisEmotionlessResultUpdate : EntityBase
    {
        public HisEmotionlessResultUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMOTIONLESS_RESULT>();
        }

        private BridgeDAO<HIS_EMOTIONLESS_RESULT> bridgeDAO;

        public bool Update(HIS_EMOTIONLESS_RESULT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_EMOTIONLESS_RESULT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
