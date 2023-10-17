using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicineLine
{
    partial class HisMedicineLineUpdate : EntityBase
    {
        public HisMedicineLineUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_LINE>();
        }

        private BridgeDAO<HIS_MEDICINE_LINE> bridgeDAO;

        public bool Update(HIS_MEDICINE_LINE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEDICINE_LINE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
