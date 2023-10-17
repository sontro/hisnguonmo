using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisPtttGroupBest
{
    partial class HisPtttGroupBestCreate : EntityBase
    {
        public HisPtttGroupBestCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PTTT_GROUP_BEST>();
        }

        private BridgeDAO<HIS_PTTT_GROUP_BEST> bridgeDAO;

        public bool Create(HIS_PTTT_GROUP_BEST data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_PTTT_GROUP_BEST> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
