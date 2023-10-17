using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisSereServDebt
{
    partial class HisSereServDebtCreate : EntityBase
    {
        public HisSereServDebtCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_DEBT>();
        }

        private BridgeDAO<HIS_SERE_SERV_DEBT> bridgeDAO;

        public bool Create(HIS_SERE_SERV_DEBT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERE_SERV_DEBT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
