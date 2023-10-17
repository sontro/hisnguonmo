using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisEmrCoverConfig
{
    partial class HisEmrCoverConfigCheck : EntityBase
    {
        public HisEmrCoverConfigCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMR_COVER_CONFIG>();
        }

        private BridgeDAO<HIS_EMR_COVER_CONFIG> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
