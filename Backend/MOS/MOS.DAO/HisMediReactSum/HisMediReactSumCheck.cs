using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMediReactSum
{
    partial class HisMediReactSumCheck : EntityBase
    {
        public HisMediReactSumCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_REACT_SUM>();
        }

        private BridgeDAO<HIS_MEDI_REACT_SUM> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
