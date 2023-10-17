using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMetyMety
{
    partial class HisMetyMetyCreate : EntityBase
    {
        public HisMetyMetyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_METY_METY>();
        }

        private BridgeDAO<HIS_METY_METY> bridgeDAO;

        public bool Create(HIS_METY_METY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_METY_METY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
