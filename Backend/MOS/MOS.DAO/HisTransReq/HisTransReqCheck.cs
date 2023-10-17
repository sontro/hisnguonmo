using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisTransReq
{
    partial class HisTransReqCheck : EntityBase
    {
        public HisTransReqCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRANS_REQ>();
        }

        private BridgeDAO<HIS_TRANS_REQ> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
