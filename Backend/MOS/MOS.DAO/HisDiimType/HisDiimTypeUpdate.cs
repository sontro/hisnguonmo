using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDiimType
{
    partial class HisDiimTypeUpdate : EntityBase
    {
        public HisDiimTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DIIM_TYPE>();
        }

        private BridgeDAO<HIS_DIIM_TYPE> bridgeDAO;

        public bool Update(HIS_DIIM_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_DIIM_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
