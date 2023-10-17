using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMediOrg
{
    partial class HisMediOrgCheck : EntityBase
    {
        public HisMediOrgCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_ORG>();
        }

        private BridgeDAO<HIS_MEDI_ORG> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
