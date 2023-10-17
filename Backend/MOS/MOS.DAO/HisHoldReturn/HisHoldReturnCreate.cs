using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisHoldReturn
{
    partial class HisHoldReturnCreate : EntityBase
    {
        public HisHoldReturnCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HOLD_RETURN>();
        }

        private BridgeDAO<HIS_HOLD_RETURN> bridgeDAO;

        public bool Create(HIS_HOLD_RETURN data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_HOLD_RETURN> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
