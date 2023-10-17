using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMestInveUser
{
    partial class HisMestInveUserCreate : EntityBase
    {
        public HisMestInveUserCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_INVE_USER>();
        }

        private BridgeDAO<HIS_MEST_INVE_USER> bridgeDAO;

        public bool Create(HIS_MEST_INVE_USER data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEST_INVE_USER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
