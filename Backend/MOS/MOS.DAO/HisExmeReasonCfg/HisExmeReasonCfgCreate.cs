using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisExmeReasonCfg
{
    partial class HisExmeReasonCfgCreate : EntityBase
    {
        public HisExmeReasonCfgCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXME_REASON_CFG>();
        }

        private BridgeDAO<HIS_EXME_REASON_CFG> bridgeDAO;

        public bool Create(HIS_EXME_REASON_CFG data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EXME_REASON_CFG> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
