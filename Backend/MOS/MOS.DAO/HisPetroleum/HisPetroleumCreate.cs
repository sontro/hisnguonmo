using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisPetroleum
{
    partial class HisPetroleumCreate : EntityBase
    {
        public HisPetroleumCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PETROLEUM>();
        }

        private BridgeDAO<HIS_PETROLEUM> bridgeDAO;

        public bool Create(HIS_PETROLEUM data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_PETROLEUM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
