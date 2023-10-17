using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicineMedicine
{
    partial class HisMedicineMedicineUpdate : EntityBase
    {
        public HisMedicineMedicineUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_MEDICINE>();
        }

        private BridgeDAO<HIS_MEDICINE_MEDICINE> bridgeDAO;

        public bool Update(HIS_MEDICINE_MEDICINE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEDICINE_MEDICINE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
