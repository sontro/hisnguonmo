using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisWorkPlace
{
    partial class HisWorkPlaceUpdate : EntityBase
    {
        public HisWorkPlaceUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_WORK_PLACE>();
        }

        private BridgeDAO<HIS_WORK_PLACE> bridgeDAO;

        public bool Update(HIS_WORK_PLACE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_WORK_PLACE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
