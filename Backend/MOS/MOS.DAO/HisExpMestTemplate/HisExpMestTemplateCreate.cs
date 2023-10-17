using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisExpMestTemplate
{
    partial class HisExpMestTemplateCreate : EntityBase
    {
        public HisExpMestTemplateCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST_TEMPLATE>();
        }

        private BridgeDAO<HIS_EXP_MEST_TEMPLATE> bridgeDAO;

        public bool Create(HIS_EXP_MEST_TEMPLATE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EXP_MEST_TEMPLATE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
