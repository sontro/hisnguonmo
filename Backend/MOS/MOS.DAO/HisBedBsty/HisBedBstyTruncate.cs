using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBedBsty
{
    partial class HisBedBstyTruncate : EntityBase
    {
        public HisBedBstyTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BED_BSTY>();
        }

        private BridgeDAO<HIS_BED_BSTY> bridgeDAO;

        public bool Truncate(HIS_BED_BSTY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_BED_BSTY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
