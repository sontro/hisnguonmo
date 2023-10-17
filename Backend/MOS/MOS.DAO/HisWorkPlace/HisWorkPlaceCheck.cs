using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisWorkPlace
{
    partial class HisWorkPlaceCheck : EntityBase
    {
        public HisWorkPlaceCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_WORK_PLACE>();
        }

        private BridgeDAO<HIS_WORK_PLACE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
