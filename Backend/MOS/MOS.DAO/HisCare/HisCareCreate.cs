using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisCare
{
    partial class HisCareCreate : EntityBase
    {
        public HisCareCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARE>();
        }

        private BridgeDAO<HIS_CARE> bridgeDAO;

        public bool Create(HIS_CARE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_CARE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
