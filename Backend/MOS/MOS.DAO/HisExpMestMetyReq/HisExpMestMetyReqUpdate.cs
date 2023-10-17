using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExpMestMetyReq
{
    partial class HisExpMestMetyReqUpdate : EntityBase
    {
        public HisExpMestMetyReqUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST_METY_REQ>();
        }

        private BridgeDAO<HIS_EXP_MEST_METY_REQ> bridgeDAO;

        public bool Update(HIS_EXP_MEST_METY_REQ data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_EXP_MEST_METY_REQ> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
