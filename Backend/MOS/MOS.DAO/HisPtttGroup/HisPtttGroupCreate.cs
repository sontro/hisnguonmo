using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisPtttGroup
{
    partial class HisPtttGroupCreate : EntityBase
    {
        public HisPtttGroupCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PTTT_GROUP>();
        }

        private BridgeDAO<HIS_PTTT_GROUP> bridgeDAO;

        public bool Create(HIS_PTTT_GROUP data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_PTTT_GROUP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
