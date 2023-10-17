using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicinePaty
{
    partial class HisMedicinePatyCreate : EntityBase
    {
        public HisMedicinePatyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_PATY>();
        }

        private BridgeDAO<HIS_MEDICINE_PATY> bridgeDAO;

        public bool Create(HIS_MEDICINE_PATY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEDICINE_PATY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
