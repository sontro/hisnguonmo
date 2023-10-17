using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicineInteractive
{
    partial class HisMedicineInteractiveUpdate : EntityBase
    {
        public HisMedicineInteractiveUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_INTERACTIVE>();
        }

        private BridgeDAO<HIS_MEDICINE_INTERACTIVE> bridgeDAO;

        public bool Update(HIS_MEDICINE_INTERACTIVE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEDICINE_INTERACTIVE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
