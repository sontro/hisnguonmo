using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSeseTransReq
{
    partial class HisSeseTransReqUpdate : EntityBase
    {
        public HisSeseTransReqUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SESE_TRANS_REQ>();
        }

        private BridgeDAO<HIS_SESE_TRANS_REQ> bridgeDAO;

        public bool Update(HIS_SESE_TRANS_REQ data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SESE_TRANS_REQ> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
