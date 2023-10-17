using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCareTempDetail
{
    partial class HisCareTempDetailUpdate : EntityBase
    {
        public HisCareTempDetailUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARE_TEMP_DETAIL>();
        }

        private BridgeDAO<HIS_CARE_TEMP_DETAIL> bridgeDAO;

        public bool Update(HIS_CARE_TEMP_DETAIL data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_CARE_TEMP_DETAIL> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
