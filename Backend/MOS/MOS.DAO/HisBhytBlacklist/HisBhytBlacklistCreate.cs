using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisBhytBlacklist
{
    partial class HisBhytBlacklistCreate : EntityBase
    {
        public HisBhytBlacklistCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BHYT_BLACKLIST>();
        }

        private BridgeDAO<HIS_BHYT_BLACKLIST> bridgeDAO;

        public bool Create(HIS_BHYT_BLACKLIST data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_BHYT_BLACKLIST> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
