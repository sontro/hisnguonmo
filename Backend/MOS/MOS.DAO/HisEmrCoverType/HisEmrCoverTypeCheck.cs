using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisEmrCoverType
{
    partial class HisEmrCoverTypeCheck : EntityBase
    {
        public HisEmrCoverTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMR_COVER_TYPE>();
        }

        private BridgeDAO<HIS_EMR_COVER_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
