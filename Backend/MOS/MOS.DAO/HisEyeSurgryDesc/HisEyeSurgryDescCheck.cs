using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisEyeSurgryDesc
{
    partial class HisEyeSurgryDescCheck : EntityBase
    {
        public HisEyeSurgryDescCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EYE_SURGRY_DESC>();
        }

        private BridgeDAO<HIS_EYE_SURGRY_DESC> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
