using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisRegisterReq
{
    partial class HisRegisterReqCheck : EntityBase
    {
        public HisRegisterReqCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REGISTER_REQ>();
        }

        private BridgeDAO<HIS_REGISTER_REQ> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
