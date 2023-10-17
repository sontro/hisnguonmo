using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisEmotionlessResult
{
    partial class HisEmotionlessResultCheck : EntityBase
    {
        public HisEmotionlessResultCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMOTIONLESS_RESULT>();
        }

        private BridgeDAO<HIS_EMOTIONLESS_RESULT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
