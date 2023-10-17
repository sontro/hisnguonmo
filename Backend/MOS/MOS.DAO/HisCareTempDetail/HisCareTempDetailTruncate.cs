using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCareTempDetail
{
    partial class HisCareTempDetailTruncate : EntityBase
    {
        public HisCareTempDetailTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARE_TEMP_DETAIL>();
        }

        private BridgeDAO<HIS_CARE_TEMP_DETAIL> bridgeDAO;

        public bool Truncate(HIS_CARE_TEMP_DETAIL data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_CARE_TEMP_DETAIL> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
