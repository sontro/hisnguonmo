using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisExpMestTemplate
{
    partial class HisExpMestTemplateCheck : EntityBase
    {
        public HisExpMestTemplateCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST_TEMPLATE>();
        }

        private BridgeDAO<HIS_EXP_MEST_TEMPLATE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
