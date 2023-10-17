using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisSuimIndexUnit
{
    partial class HisSuimIndexUnitCheck : EntityBase
    {
        public HisSuimIndexUnitCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SUIM_INDEX_UNIT>();
        }

        private BridgeDAO<HIS_SUIM_INDEX_UNIT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
