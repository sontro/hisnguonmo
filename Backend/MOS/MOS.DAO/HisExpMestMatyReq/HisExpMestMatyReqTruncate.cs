using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExpMestMatyReq
{
    partial class HisExpMestMatyReqTruncate : EntityBase
    {
        public HisExpMestMatyReqTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST_MATY_REQ>();
        }

        private BridgeDAO<HIS_EXP_MEST_MATY_REQ> bridgeDAO;

        public bool Truncate(HIS_EXP_MEST_MATY_REQ data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_EXP_MEST_MATY_REQ> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
