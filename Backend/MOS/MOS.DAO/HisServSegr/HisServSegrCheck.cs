using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisServSegr
{
    partial class HisServSegrCheck : EntityBase
    {
        public HisServSegrCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERV_SEGR>();
        }

        private BridgeDAO<HIS_SERV_SEGR> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
