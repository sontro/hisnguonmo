using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceMety
{
    partial class HisServiceMetyCreate : EntityBase
    {
        public HisServiceMetyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_METY>();
        }

        private BridgeDAO<HIS_SERVICE_METY> bridgeDAO;

        public bool Create(HIS_SERVICE_METY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERVICE_METY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
