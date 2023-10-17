using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisHoreDhty
{
    partial class HisHoreDhtyCheck : EntityBase
    {
        public HisHoreDhtyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HORE_DHTY>();
        }

        private BridgeDAO<HIS_HORE_DHTY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
