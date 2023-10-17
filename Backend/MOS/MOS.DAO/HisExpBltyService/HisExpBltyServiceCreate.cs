using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisExpBltyService
{
    partial class HisExpBltyServiceCreate : EntityBase
    {
        public HisExpBltyServiceCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_BLTY_SERVICE>();
        }

        private BridgeDAO<HIS_EXP_BLTY_SERVICE> bridgeDAO;

        public bool Create(HIS_EXP_BLTY_SERVICE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EXP_BLTY_SERVICE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
