using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicineBean
{
    partial class HisMedicineBeanUpdate : EntityBase
    {
        public HisMedicineBeanUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_BEAN>();
        }

        private BridgeDAO<HIS_MEDICINE_BEAN> bridgeDAO;

        public bool Update(HIS_MEDICINE_BEAN data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEDICINE_BEAN> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
