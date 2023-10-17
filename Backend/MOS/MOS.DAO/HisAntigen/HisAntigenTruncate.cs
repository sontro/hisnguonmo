using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAntigen
{
    partial class HisAntigenTruncate : EntityBase
    {
        public HisAntigenTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTIGEN>();
        }

        private BridgeDAO<HIS_ANTIGEN> bridgeDAO;

        public bool Truncate(HIS_ANTIGEN data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ANTIGEN> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
