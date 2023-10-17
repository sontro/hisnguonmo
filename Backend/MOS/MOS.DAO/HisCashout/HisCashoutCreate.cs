using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisCashout
{
    partial class HisCashoutCreate : EntityBase
    {
        public HisCashoutCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CASHOUT>();
        }

        private BridgeDAO<HIS_CASHOUT> bridgeDAO;

        public bool Create(HIS_CASHOUT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_CASHOUT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
