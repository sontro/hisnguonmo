using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAnticipateMety
{
    partial class HisAnticipateMetyUpdate : EntityBase
    {
        public HisAnticipateMetyUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTICIPATE_METY>();
        }

        private BridgeDAO<HIS_ANTICIPATE_METY> bridgeDAO;

        public bool Update(HIS_ANTICIPATE_METY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ANTICIPATE_METY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
