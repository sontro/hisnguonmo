using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisLicenseClass
{
    partial class HisLicenseClassCheck : EntityBase
    {
        public HisLicenseClassCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_LICENSE_CLASS>();
        }

        private BridgeDAO<HIS_LICENSE_CLASS> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
