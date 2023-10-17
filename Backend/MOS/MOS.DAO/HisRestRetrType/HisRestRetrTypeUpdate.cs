using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRestRetrType
{
    partial class HisRestRetrTypeUpdate : EntityBase
    {
        public HisRestRetrTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REST_RETR_TYPE>();
        }

        private BridgeDAO<HIS_REST_RETR_TYPE> bridgeDAO;

        public bool Update(HIS_REST_RETR_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_REST_RETR_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
