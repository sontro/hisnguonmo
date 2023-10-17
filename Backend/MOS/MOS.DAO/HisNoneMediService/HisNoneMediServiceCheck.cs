using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisNoneMediService
{
    partial class HisNoneMediServiceCheck : EntityBase
    {
        public HisNoneMediServiceCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_NONE_MEDI_SERVICE>();
        }

        private BridgeDAO<HIS_NONE_MEDI_SERVICE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
