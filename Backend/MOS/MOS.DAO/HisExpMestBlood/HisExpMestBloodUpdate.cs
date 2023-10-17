using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExpMestBlood
{
    partial class HisExpMestBloodUpdate : EntityBase
    {
        public HisExpMestBloodUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST_BLOOD>();
        }

        private BridgeDAO<HIS_EXP_MEST_BLOOD> bridgeDAO;

        public bool Update(HIS_EXP_MEST_BLOOD data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_EXP_MEST_BLOOD> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
