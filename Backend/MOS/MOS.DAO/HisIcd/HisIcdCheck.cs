using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisIcd
{
    partial class HisIcdCheck : EntityBase
    {
        public HisIcdCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ICD>();
        }

        private BridgeDAO<HIS_ICD> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
