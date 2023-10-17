using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisExpMestBltyReq
{
    partial class HisExpMestBltyReqCheck : EntityBase
    {
        public HisExpMestBltyReqCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST_BLTY_REQ>();
        }

        private BridgeDAO<HIS_EXP_MEST_BLTY_REQ> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
