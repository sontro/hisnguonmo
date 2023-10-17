using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAnticipate
{
    partial class HisAnticipateUpdate : EntityBase
    {
        public HisAnticipateUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTICIPATE>();
        }

        private BridgeDAO<HIS_ANTICIPATE> bridgeDAO;

        public bool Update(HIS_ANTICIPATE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ANTICIPATE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
