using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisUnlimitReason
{
    partial class HisUnlimitReasonCreate : EntityBase
    {
        public HisUnlimitReasonCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_UNLIMIT_REASON>();
        }

        private BridgeDAO<HIS_UNLIMIT_REASON> bridgeDAO;

        public bool Create(HIS_UNLIMIT_REASON data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_UNLIMIT_REASON> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
