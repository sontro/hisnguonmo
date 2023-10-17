using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisService
{
    partial class HisServiceCreate : EntityBase
    {
        public HisServiceCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE>();
        }

        private BridgeDAO<HIS_SERVICE> bridgeDAO;

        public bool Create(HIS_SERVICE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERVICE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
