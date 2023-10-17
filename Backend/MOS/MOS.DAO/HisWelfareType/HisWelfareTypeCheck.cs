using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisWelfareType
{
    partial class HisWelfareTypeCheck : EntityBase
    {
        public HisWelfareTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_WELFARE_TYPE>();
        }

        private BridgeDAO<HIS_WELFARE_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
