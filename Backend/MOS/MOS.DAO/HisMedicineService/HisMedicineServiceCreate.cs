using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicineService
{
    partial class HisMedicineServiceCreate : EntityBase
    {
        public HisMedicineServiceCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_SERVICE>();
        }

        private BridgeDAO<HIS_MEDICINE_SERVICE> bridgeDAO;

        public bool Create(HIS_MEDICINE_SERVICE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEDICINE_SERVICE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
