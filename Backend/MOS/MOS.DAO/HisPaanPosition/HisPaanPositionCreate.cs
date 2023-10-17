using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisPaanPosition
{
    partial class HisPaanPositionCreate : EntityBase
    {
        public HisPaanPositionCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PAAN_POSITION>();
        }

        private BridgeDAO<HIS_PAAN_POSITION> bridgeDAO;

        public bool Create(HIS_PAAN_POSITION data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_PAAN_POSITION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
