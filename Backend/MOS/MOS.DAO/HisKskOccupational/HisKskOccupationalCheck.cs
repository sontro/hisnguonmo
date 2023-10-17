using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisKskOccupational
{
    partial class HisKskOccupationalCheck : EntityBase
    {
        public HisKskOccupationalCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_OCCUPATIONAL>();
        }

        private BridgeDAO<HIS_KSK_OCCUPATIONAL> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
