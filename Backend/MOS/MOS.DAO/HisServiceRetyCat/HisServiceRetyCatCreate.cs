using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceRetyCat
{
    partial class HisServiceRetyCatCreate : EntityBase
    {
        public HisServiceRetyCatCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_RETY_CAT>();
        }

        private BridgeDAO<HIS_SERVICE_RETY_CAT> bridgeDAO;

        public bool Create(HIS_SERVICE_RETY_CAT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERVICE_RETY_CAT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
