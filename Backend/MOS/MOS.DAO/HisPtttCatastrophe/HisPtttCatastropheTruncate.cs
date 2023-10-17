using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPtttCatastrophe
{
    partial class HisPtttCatastropheTruncate : EntityBase
    {
        public HisPtttCatastropheTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PTTT_CATASTROPHE>();
        }

        private BridgeDAO<HIS_PTTT_CATASTROPHE> bridgeDAO;

        public bool Truncate(HIS_PTTT_CATASTROPHE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_PTTT_CATASTROPHE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
