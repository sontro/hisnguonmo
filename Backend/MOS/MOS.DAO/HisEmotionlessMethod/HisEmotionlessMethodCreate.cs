using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisEmotionlessMethod
{
    partial class HisEmotionlessMethodCreate : EntityBase
    {
        public HisEmotionlessMethodCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMOTIONLESS_METHOD>();
        }

        private BridgeDAO<HIS_EMOTIONLESS_METHOD> bridgeDAO;

        public bool Create(HIS_EMOTIONLESS_METHOD data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EMOTIONLESS_METHOD> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
