using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisBltyService
{
    partial class HisBltyServiceCreate : EntityBase
    {
        public HisBltyServiceCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BLTY_SERVICE>();
        }

        private BridgeDAO<HIS_BLTY_SERVICE> bridgeDAO;

        public bool Create(HIS_BLTY_SERVICE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_BLTY_SERVICE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
