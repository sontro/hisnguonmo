using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisExpiredDateCfg
{
    partial class HisExpiredDateCfgCreate : EntityBase
    {
        public HisExpiredDateCfgCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXPIRED_DATE_CFG>();
        }

        private BridgeDAO<HIS_EXPIRED_DATE_CFG> bridgeDAO;

        public bool Create(HIS_EXPIRED_DATE_CFG data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EXPIRED_DATE_CFG> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
