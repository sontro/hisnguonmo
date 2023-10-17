using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBodyPart
{
    partial class HisBodyPartTruncate : EntityBase
    {
        public HisBodyPartTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BODY_PART>();
        }

        private BridgeDAO<HIS_BODY_PART> bridgeDAO;

        public bool Truncate(HIS_BODY_PART data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_BODY_PART> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
