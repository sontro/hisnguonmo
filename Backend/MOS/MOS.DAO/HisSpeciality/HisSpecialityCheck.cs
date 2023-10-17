using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisSpeciality
{
    partial class HisSpecialityCheck : EntityBase
    {
        public HisSpecialityCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SPECIALITY>();
        }

        private BridgeDAO<HIS_SPECIALITY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
