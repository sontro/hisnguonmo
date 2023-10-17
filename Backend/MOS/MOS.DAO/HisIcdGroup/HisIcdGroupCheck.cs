using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisIcdGroup
{
    partial class HisIcdGroupCheck : EntityBase
    {
        public HisIcdGroupCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ICD_GROUP>();
        }

        private BridgeDAO<HIS_ICD_GROUP> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
