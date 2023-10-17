using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisNextTreaIntr
{
    partial class HisNextTreaIntrCreate : EntityBase
    {
        public HisNextTreaIntrCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_NEXT_TREA_INTR>();
        }

        private BridgeDAO<HIS_NEXT_TREA_INTR> bridgeDAO;

        public bool Create(HIS_NEXT_TREA_INTR data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_NEXT_TREA_INTR> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
