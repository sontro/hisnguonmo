using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMilitaryRank
{
    partial class HisMilitaryRankCreate : EntityBase
    {
        public HisMilitaryRankCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MILITARY_RANK>();
        }

        private BridgeDAO<HIS_MILITARY_RANK> bridgeDAO;

        public bool Create(HIS_MILITARY_RANK data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MILITARY_RANK> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
