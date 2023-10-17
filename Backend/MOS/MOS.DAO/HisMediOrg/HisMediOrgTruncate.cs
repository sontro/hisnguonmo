using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMediOrg
{
    partial class HisMediOrgTruncate : EntityBase
    {
        public HisMediOrgTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_ORG>();
        }

        private BridgeDAO<HIS_MEDI_ORG> bridgeDAO;

        public bool Truncate(HIS_MEDI_ORG data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEDI_ORG> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
