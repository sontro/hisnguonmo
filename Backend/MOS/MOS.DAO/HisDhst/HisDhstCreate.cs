using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisDhst
{
    partial class HisDhstCreate : EntityBase
    {
        public HisDhstCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DHST>();
        }

        private BridgeDAO<HIS_DHST> bridgeDAO;

        public bool Create(HIS_DHST data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_DHST> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
