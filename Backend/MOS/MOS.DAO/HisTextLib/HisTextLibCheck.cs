using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisTextLib
{
    partial class HisTextLibCheck : EntityBase
    {
        public HisTextLibCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TEXT_LIB>();
        }

        private BridgeDAO<HIS_TEXT_LIB> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
