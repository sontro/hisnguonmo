using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisPtttPriority
{
    partial class HisPtttPriorityCreate : EntityBase
    {
        public HisPtttPriorityCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PTTT_PRIORITY>();
        }

        private BridgeDAO<HIS_PTTT_PRIORITY> bridgeDAO;

        public bool Create(HIS_PTTT_PRIORITY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_PTTT_PRIORITY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
