using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisPackage
{
    partial class HisPackageCheck : EntityBase
    {
        public HisPackageCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PACKAGE>();
        }

        private BridgeDAO<HIS_PACKAGE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
