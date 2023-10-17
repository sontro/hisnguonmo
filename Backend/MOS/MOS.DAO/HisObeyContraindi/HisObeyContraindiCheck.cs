using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisObeyContraindi
{
    partial class HisObeyContraindiCheck : EntityBase
    {
        public HisObeyContraindiCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_OBEY_CONTRAINDI>();
        }

        private BridgeDAO<HIS_OBEY_CONTRAINDI> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
