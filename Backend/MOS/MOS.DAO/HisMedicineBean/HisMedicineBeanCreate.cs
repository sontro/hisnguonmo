using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicineBean
{
    partial class HisMedicineBeanCreate : EntityBase
    {
        public HisMedicineBeanCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_BEAN>();
        }

        private BridgeDAO<HIS_MEDICINE_BEAN> bridgeDAO;

        public bool Create(HIS_MEDICINE_BEAN data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEDICINE_BEAN> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
