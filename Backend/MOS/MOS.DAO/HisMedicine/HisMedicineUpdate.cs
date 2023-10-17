using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicine
{
    partial class HisMedicineUpdate : EntityBase
    {
        public HisMedicineUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE>();
        }

        private BridgeDAO<HIS_MEDICINE> bridgeDAO;

        public bool Update(HIS_MEDICINE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEDICINE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
