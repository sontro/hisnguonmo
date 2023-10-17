using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAnticipateMaty
{
    partial class HisAnticipateMatyUpdate : EntityBase
    {
        public HisAnticipateMatyUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTICIPATE_MATY>();
        }

        private BridgeDAO<HIS_ANTICIPATE_MATY> bridgeDAO;

        public bool Update(HIS_ANTICIPATE_MATY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ANTICIPATE_MATY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
