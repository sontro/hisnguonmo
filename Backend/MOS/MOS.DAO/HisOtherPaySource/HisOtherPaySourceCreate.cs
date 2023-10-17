using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisOtherPaySource
{
    partial class HisOtherPaySourceCreate : EntityBase
    {
        public HisOtherPaySourceCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_OTHER_PAY_SOURCE>();
        }

        private BridgeDAO<HIS_OTHER_PAY_SOURCE> bridgeDAO;

        public bool Create(HIS_OTHER_PAY_SOURCE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_OTHER_PAY_SOURCE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
