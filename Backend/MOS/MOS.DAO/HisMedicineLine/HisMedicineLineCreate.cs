using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicineLine
{
    partial class HisMedicineLineCreate : EntityBase
    {
        public HisMedicineLineCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_LINE>();
        }

        private BridgeDAO<HIS_MEDICINE_LINE> bridgeDAO;

        public bool Create(HIS_MEDICINE_LINE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEDICINE_LINE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
