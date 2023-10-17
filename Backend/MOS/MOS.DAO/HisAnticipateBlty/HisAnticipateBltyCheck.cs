using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisAnticipateBlty
{
    partial class HisAnticipateBltyCheck : EntityBase
    {
        public HisAnticipateBltyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTICIPATE_BLTY>();
        }

        private BridgeDAO<HIS_ANTICIPATE_BLTY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
