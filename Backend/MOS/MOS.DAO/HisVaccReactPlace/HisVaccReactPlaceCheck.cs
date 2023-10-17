using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisVaccReactPlace
{
    partial class HisVaccReactPlaceCheck : EntityBase
    {
        public HisVaccReactPlaceCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACC_REACT_PLACE>();
        }

        private BridgeDAO<HIS_VACC_REACT_PLACE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
