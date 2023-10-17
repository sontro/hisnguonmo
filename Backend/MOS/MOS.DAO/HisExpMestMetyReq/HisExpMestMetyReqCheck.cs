using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisExpMestMetyReq
{
    partial class HisExpMestMetyReqCheck : EntityBase
    {
        public HisExpMestMetyReqCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST_METY_REQ>();
        }

        private BridgeDAO<HIS_EXP_MEST_METY_REQ> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
