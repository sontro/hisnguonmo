using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAnticipateMaty
{
    partial class HisAnticipateMatyCreate : EntityBase
    {
        public HisAnticipateMatyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTICIPATE_MATY>();
        }

        private BridgeDAO<HIS_ANTICIPATE_MATY> bridgeDAO;

        public bool Create(HIS_ANTICIPATE_MATY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ANTICIPATE_MATY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
