using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisPrepare
{
    partial class HisPrepareCreate : EntityBase
    {
        public HisPrepareCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PREPARE>();
        }

        private BridgeDAO<HIS_PREPARE> bridgeDAO;

        public bool Create(HIS_PREPARE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_PREPARE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
