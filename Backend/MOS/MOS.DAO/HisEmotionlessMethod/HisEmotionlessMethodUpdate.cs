using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEmotionlessMethod
{
    partial class HisEmotionlessMethodUpdate : EntityBase
    {
        public HisEmotionlessMethodUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMOTIONLESS_METHOD>();
        }

        private BridgeDAO<HIS_EMOTIONLESS_METHOD> bridgeDAO;

        public bool Update(HIS_EMOTIONLESS_METHOD data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_EMOTIONLESS_METHOD> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
