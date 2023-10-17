using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExpMestTemplate
{
    partial class HisExpMestTemplateTruncate : EntityBase
    {
        public HisExpMestTemplateTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST_TEMPLATE>();
        }

        private BridgeDAO<HIS_EXP_MEST_TEMPLATE> bridgeDAO;

        public bool Truncate(HIS_EXP_MEST_TEMPLATE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_EXP_MEST_TEMPLATE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
