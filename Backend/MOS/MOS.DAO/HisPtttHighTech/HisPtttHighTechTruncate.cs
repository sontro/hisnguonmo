using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPtttHighTech
{
    partial class HisPtttHighTechTruncate : EntityBase
    {
        public HisPtttHighTechTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PTTT_HIGH_TECH>();
        }

        private BridgeDAO<HIS_PTTT_HIGH_TECH> bridgeDAO;

        public bool Truncate(HIS_PTTT_HIGH_TECH data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_PTTT_HIGH_TECH> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
