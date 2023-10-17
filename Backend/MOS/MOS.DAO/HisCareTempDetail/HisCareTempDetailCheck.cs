using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisCareTempDetail
{
    partial class HisCareTempDetailCheck : EntityBase
    {
        public HisCareTempDetailCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARE_TEMP_DETAIL>();
        }

        private BridgeDAO<HIS_CARE_TEMP_DETAIL> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
