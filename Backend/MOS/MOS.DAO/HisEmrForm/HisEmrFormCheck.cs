using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisEmrForm
{
    partial class HisEmrFormCheck : EntityBase
    {
        public HisEmrFormCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMR_FORM>();
        }

        private BridgeDAO<HIS_EMR_FORM> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
