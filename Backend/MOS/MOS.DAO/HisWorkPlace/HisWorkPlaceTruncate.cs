using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisWorkPlace
{
    partial class HisWorkPlaceTruncate : EntityBase
    {
        public HisWorkPlaceTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_WORK_PLACE>();
        }

        private BridgeDAO<HIS_WORK_PLACE> bridgeDAO;

        public bool Truncate(HIS_WORK_PLACE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_WORK_PLACE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
