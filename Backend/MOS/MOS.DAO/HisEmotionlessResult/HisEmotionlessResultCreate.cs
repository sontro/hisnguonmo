using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisEmotionlessResult
{
    partial class HisEmotionlessResultCreate : EntityBase
    {
        public HisEmotionlessResultCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMOTIONLESS_RESULT>();
        }

        private BridgeDAO<HIS_EMOTIONLESS_RESULT> bridgeDAO;

        public bool Create(HIS_EMOTIONLESS_RESULT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EMOTIONLESS_RESULT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
