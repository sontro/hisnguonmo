using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisExpMestMatyReq
{
    partial class HisExpMestMatyReqCheck : EntityBase
    {
        public HisExpMestMatyReqCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST_MATY_REQ>();
        }

        private BridgeDAO<HIS_EXP_MEST_MATY_REQ> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
