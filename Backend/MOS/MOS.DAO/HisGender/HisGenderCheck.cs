using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisGender
{
    partial class HisGenderCheck : EntityBase
    {
        public HisGenderCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_GENDER>();
        }

        private BridgeDAO<HIS_GENDER> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
