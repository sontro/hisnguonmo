using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicineInteractive
{
    partial class HisMedicineInteractiveCreate : EntityBase
    {
        public HisMedicineInteractiveCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_INTERACTIVE>();
        }

        private BridgeDAO<HIS_MEDICINE_INTERACTIVE> bridgeDAO;

        public bool Create(HIS_MEDICINE_INTERACTIVE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEDICINE_INTERACTIVE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
