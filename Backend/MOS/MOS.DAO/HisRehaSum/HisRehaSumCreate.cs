using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisRehaSum
{
    partial class HisRehaSumCreate : EntityBase
    {
        public HisRehaSumCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REHA_SUM>();
        }

        private BridgeDAO<HIS_REHA_SUM> bridgeDAO;

        public bool Create(HIS_REHA_SUM data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_REHA_SUM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
