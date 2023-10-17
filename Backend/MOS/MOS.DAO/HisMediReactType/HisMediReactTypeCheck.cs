using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMediReactType
{
    partial class HisMediReactTypeCheck : EntityBase
    {
        public HisMediReactTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_REACT_TYPE>();
        }

        private BridgeDAO<HIS_MEDI_REACT_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
