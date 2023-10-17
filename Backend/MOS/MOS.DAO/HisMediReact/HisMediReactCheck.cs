using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMediReact
{
    partial class HisMediReactCheck : EntityBase
    {
        public HisMediReactCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_REACT>();
        }

        private BridgeDAO<HIS_MEDI_REACT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
