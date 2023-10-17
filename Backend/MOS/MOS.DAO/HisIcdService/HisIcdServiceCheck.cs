using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisIcdService
{
    partial class HisIcdServiceCheck : EntityBase
    {
        public HisIcdServiceCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ICD_SERVICE>();
        }

        private BridgeDAO<HIS_ICD_SERVICE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
