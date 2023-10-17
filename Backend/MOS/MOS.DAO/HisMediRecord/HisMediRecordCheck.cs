using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMediRecord
{
    partial class HisMediRecordCheck : EntityBase
    {
        public HisMediRecordCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_RECORD>();
        }

        private BridgeDAO<HIS_MEDI_RECORD> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
