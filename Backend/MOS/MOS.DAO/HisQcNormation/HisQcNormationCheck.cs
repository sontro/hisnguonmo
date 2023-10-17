using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisQcNormation
{
    partial class HisQcNormationCheck : EntityBase
    {
        public HisQcNormationCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_QC_NORMATION>();
        }

        private BridgeDAO<HIS_QC_NORMATION> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
