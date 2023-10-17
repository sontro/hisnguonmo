using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisSubclinicalRsAdd
{
    partial class HisSubclinicalRsAddCheck : EntityBase
    {
        public HisSubclinicalRsAddCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SUBCLINICAL_RS_ADD>();
        }

        private BridgeDAO<HIS_SUBCLINICAL_RS_ADD> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
