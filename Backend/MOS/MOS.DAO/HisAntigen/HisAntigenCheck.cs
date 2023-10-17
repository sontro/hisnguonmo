using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisAntigen
{
    partial class HisAntigenCheck : EntityBase
    {
        public HisAntigenCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTIGEN>();
        }

        private BridgeDAO<HIS_ANTIGEN> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
