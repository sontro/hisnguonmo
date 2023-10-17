using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAnticipateMety
{
    partial class HisAnticipateMetyCreate : EntityBase
    {
        public HisAnticipateMetyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTICIPATE_METY>();
        }

        private BridgeDAO<HIS_ANTICIPATE_METY> bridgeDAO;

        public bool Create(HIS_ANTICIPATE_METY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ANTICIPATE_METY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
