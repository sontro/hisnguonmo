using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisSereServDeposit
{
    partial class HisSereServDepositCreate : EntityBase
    {
        public HisSereServDepositCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_DEPOSIT>();
        }

        private BridgeDAO<HIS_SERE_SERV_DEPOSIT> bridgeDAO;

        public bool Create(HIS_SERE_SERV_DEPOSIT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERE_SERV_DEPOSIT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
