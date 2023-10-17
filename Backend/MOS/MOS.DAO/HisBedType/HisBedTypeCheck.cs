using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisBedType
{
    partial class HisBedTypeCheck : EntityBase
    {
        public HisBedTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BED_TYPE>();
        }

        private BridgeDAO<HIS_BED_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
