using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisEmotionlessMethod
{
    partial class HisEmotionlessMethodCheck : EntityBase
    {
        public HisEmotionlessMethodCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMOTIONLESS_METHOD>();
        }

        private BridgeDAO<HIS_EMOTIONLESS_METHOD> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
