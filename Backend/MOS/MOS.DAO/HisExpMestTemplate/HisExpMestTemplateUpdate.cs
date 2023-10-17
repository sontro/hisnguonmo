using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExpMestTemplate
{
    partial class HisExpMestTemplateUpdate : EntityBase
    {
        public HisExpMestTemplateUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST_TEMPLATE>();
        }

        private BridgeDAO<HIS_EXP_MEST_TEMPLATE> bridgeDAO;

        public bool Update(HIS_EXP_MEST_TEMPLATE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_EXP_MEST_TEMPLATE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
