using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisExpMestStt
{
    partial class HisExpMestSttCheck : EntityBase
    {
        public HisExpMestSttCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST_STT>();
        }

        private BridgeDAO<HIS_EXP_MEST_STT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
