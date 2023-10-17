using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAntigenMety
{
    partial class HisAntigenMetyTruncate : EntityBase
    {
        public HisAntigenMetyTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTIGEN_METY>();
        }

        private BridgeDAO<HIS_ANTIGEN_METY> bridgeDAO;

        public bool Truncate(HIS_ANTIGEN_METY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ANTIGEN_METY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
