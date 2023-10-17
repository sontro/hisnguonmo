using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisWarningFeeCfg
{
    partial class HisWarningFeeCfgCreate : EntityBase
    {
        public HisWarningFeeCfgCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_WARNING_FEE_CFG>();
        }

        private BridgeDAO<HIS_WARNING_FEE_CFG> bridgeDAO;

        public bool Create(HIS_WARNING_FEE_CFG data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_WARNING_FEE_CFG> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
