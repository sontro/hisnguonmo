using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisSeseTransReq
{
    partial class HisSeseTransReqCheck : EntityBase
    {
        public HisSeseTransReqCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SESE_TRANS_REQ>();
        }

        private BridgeDAO<HIS_SESE_TRANS_REQ> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
