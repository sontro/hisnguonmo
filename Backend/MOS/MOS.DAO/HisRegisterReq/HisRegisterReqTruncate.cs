using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRegisterReq
{
    partial class HisRegisterReqTruncate : EntityBase
    {
        public HisRegisterReqTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REGISTER_REQ>();
        }

        private BridgeDAO<HIS_REGISTER_REQ> bridgeDAO;

        public bool Truncate(HIS_REGISTER_REQ data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_REGISTER_REQ> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
