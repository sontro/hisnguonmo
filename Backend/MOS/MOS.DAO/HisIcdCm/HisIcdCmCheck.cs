using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisIcdCm
{
    partial class HisIcdCmCheck : EntityBase
    {
        public HisIcdCmCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ICD_CM>();
        }

        private BridgeDAO<HIS_ICD_CM> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
