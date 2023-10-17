using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExpMestStt
{
    partial class HisExpMestSttUpdate : EntityBase
    {
        public HisExpMestSttUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST_STT>();
        }

        private BridgeDAO<HIS_EXP_MEST_STT> bridgeDAO;

        public bool Update(HIS_EXP_MEST_STT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_EXP_MEST_STT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
