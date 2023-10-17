using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisProcessingMethod
{
    partial class HisProcessingMethodCheck : EntityBase
    {
        public HisProcessingMethodCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PROCESSING_METHOD>();
        }

        private BridgeDAO<HIS_PROCESSING_METHOD> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
