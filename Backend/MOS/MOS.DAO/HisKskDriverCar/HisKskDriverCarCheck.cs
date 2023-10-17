using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisKskDriverCar
{
    partial class HisKskDriverCarCheck : EntityBase
    {
        public HisKskDriverCarCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_DRIVER_CAR>();
        }

        private BridgeDAO<HIS_KSK_DRIVER_CAR> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
