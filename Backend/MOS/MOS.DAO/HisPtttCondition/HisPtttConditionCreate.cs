using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisPtttCondition
{
    partial class HisPtttConditionCreate : EntityBase
    {
        public HisPtttConditionCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PTTT_CONDITION>();
        }

        private BridgeDAO<HIS_PTTT_CONDITION> bridgeDAO;

        public bool Create(HIS_PTTT_CONDITION data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_PTTT_CONDITION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
