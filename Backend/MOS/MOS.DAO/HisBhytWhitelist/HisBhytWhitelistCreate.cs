using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisBhytWhitelist
{
    partial class HisBhytWhitelistCreate : EntityBase
    {
        public HisBhytWhitelistCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BHYT_WHITELIST>();
        }

        private BridgeDAO<HIS_BHYT_WHITELIST> bridgeDAO;

        public bool Create(HIS_BHYT_WHITELIST data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_BHYT_WHITELIST> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
