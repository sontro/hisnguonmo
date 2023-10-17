using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisServicePackage
{
    partial class HisServicePackageCheck : EntityBase
    {
        public HisServicePackageCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_PACKAGE>();
        }

        private BridgeDAO<HIS_SERVICE_PACKAGE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
