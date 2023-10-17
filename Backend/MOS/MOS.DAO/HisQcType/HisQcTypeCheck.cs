using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisQcType
{
    partial class HisQcTypeCheck : EntityBase
    {
        public HisQcTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_QC_TYPE>();
        }

        private BridgeDAO<HIS_QC_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
