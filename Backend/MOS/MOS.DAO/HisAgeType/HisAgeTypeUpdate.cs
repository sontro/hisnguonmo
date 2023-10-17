using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAgeType
{
    partial class HisAgeTypeUpdate : EntityBase
    {
        public HisAgeTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_AGE_TYPE>();
        }

        private BridgeDAO<HIS_AGE_TYPE> bridgeDAO;

        public bool Update(HIS_AGE_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_AGE_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
