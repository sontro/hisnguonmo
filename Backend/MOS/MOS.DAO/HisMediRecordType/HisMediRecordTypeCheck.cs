using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMediRecordType
{
    partial class HisMediRecordTypeCheck : EntityBase
    {
        public HisMediRecordTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_RECORD_TYPE>();
        }

        private BridgeDAO<HIS_MEDI_RECORD_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
