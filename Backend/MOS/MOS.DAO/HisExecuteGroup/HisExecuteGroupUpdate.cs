using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExecuteGroup
{
    partial class HisExecuteGroupUpdate : EntityBase
    {
        public HisExecuteGroupUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXECUTE_GROUP>();
        }

        private BridgeDAO<HIS_EXECUTE_GROUP> bridgeDAO;

        public bool Update(HIS_EXECUTE_GROUP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_EXECUTE_GROUP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
