using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisVaccReactType
{
    partial class HisVaccReactTypeCheck : EntityBase
    {
        public HisVaccReactTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACC_REACT_TYPE>();
        }

        private BridgeDAO<HIS_VACC_REACT_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
