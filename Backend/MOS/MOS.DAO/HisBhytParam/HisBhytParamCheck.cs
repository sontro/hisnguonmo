using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisBhytParam
{
    partial class HisBhytParamCheck : EntityBase
    {
        public HisBhytParamCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BHYT_PARAM>();
        }

        private BridgeDAO<HIS_BHYT_PARAM> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
