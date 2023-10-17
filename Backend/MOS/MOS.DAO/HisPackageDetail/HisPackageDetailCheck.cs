using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisPackageDetail
{
    partial class HisPackageDetailCheck : EntityBase
    {
        public HisPackageDetailCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PACKAGE_DETAIL>();
        }

        private BridgeDAO<HIS_PACKAGE_DETAIL> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
