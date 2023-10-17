using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisKskDriverCar
{
    partial class HisKskDriverCarCreate : EntityBase
    {
        public HisKskDriverCarCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_DRIVER_CAR>();
        }

        private BridgeDAO<HIS_KSK_DRIVER_CAR> bridgeDAO;

        public bool Create(HIS_KSK_DRIVER_CAR data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_KSK_DRIVER_CAR> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
