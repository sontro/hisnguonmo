using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceSame
{
    partial class HisServiceSameCreate : EntityBase
    {
        public HisServiceSameCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_SAME>();
        }

        private BridgeDAO<HIS_SERVICE_SAME> bridgeDAO;

        public bool Create(HIS_SERVICE_SAME data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERVICE_SAME> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
