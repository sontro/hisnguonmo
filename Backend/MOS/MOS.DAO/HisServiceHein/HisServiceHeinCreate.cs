using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceHein
{
    partial class HisServiceHeinCreate : EntityBase
    {
        public HisServiceHeinCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_HEIN>();
        }

        private BridgeDAO<HIS_SERVICE_HEIN> bridgeDAO;

        public bool Create(HIS_SERVICE_HEIN data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERVICE_HEIN> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
