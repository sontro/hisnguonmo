using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRegisterReq
{
    partial class HisRegisterReqUpdate : EntityBase
    {
        public HisRegisterReqUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REGISTER_REQ>();
        }

        private BridgeDAO<HIS_REGISTER_REQ> bridgeDAO;

        public bool Update(HIS_REGISTER_REQ data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_REGISTER_REQ> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
