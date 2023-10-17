using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCareType
{
    partial class HisCareTypeUpdate : EntityBase
    {
        public HisCareTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARE_TYPE>();
        }

        private BridgeDAO<HIS_CARE_TYPE> bridgeDAO;

        public bool Update(HIS_CARE_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_CARE_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
