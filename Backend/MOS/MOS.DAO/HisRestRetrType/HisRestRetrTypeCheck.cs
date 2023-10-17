using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisRestRetrType
{
    partial class HisRestRetrTypeCheck : EntityBase
    {
        public HisRestRetrTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REST_RETR_TYPE>();
        }

        private BridgeDAO<HIS_REST_RETR_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
