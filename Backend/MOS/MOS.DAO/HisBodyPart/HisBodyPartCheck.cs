using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisBodyPart
{
    partial class HisBodyPartCheck : EntityBase
    {
        public HisBodyPartCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BODY_PART>();
        }

        private BridgeDAO<HIS_BODY_PART> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
