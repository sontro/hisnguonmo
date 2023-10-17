using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisVaccineType
{
    partial class HisVaccineTypeCheck : EntityBase
    {
        public HisVaccineTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACCINE_TYPE>();
        }

        private BridgeDAO<HIS_VACCINE_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
