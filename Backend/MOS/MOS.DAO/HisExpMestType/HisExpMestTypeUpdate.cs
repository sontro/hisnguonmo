using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExpMestType
{
    partial class HisExpMestTypeUpdate : EntityBase
    {
        public HisExpMestTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST_TYPE>();
        }

        private BridgeDAO<HIS_EXP_MEST_TYPE> bridgeDAO;

        public bool Update(HIS_EXP_MEST_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_EXP_MEST_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
