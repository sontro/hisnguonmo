using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisServicePaty
{
    partial class HisServicePatyCreate : EntityBase
    {
        public HisServicePatyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_PATY>();
        }

        private BridgeDAO<HIS_SERVICE_PATY> bridgeDAO;

        public bool Create(HIS_SERVICE_PATY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERVICE_PATY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
