using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicineTypeAcin
{
    partial class HisMedicineTypeAcinCreate : EntityBase
    {
        public HisMedicineTypeAcinCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_TYPE_ACIN>();
        }

        private BridgeDAO<HIS_MEDICINE_TYPE_ACIN> bridgeDAO;

        public bool Create(HIS_MEDICINE_TYPE_ACIN data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEDICINE_TYPE_ACIN> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
