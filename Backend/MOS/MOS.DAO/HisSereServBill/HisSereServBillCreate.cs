using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisSereServBill
{
    partial class HisSereServBillCreate : EntityBase
    {
        public HisSereServBillCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_BILL>();
        }

        private BridgeDAO<HIS_SERE_SERV_BILL> bridgeDAO;

        public bool Create(HIS_SERE_SERV_BILL data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERE_SERV_BILL> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
