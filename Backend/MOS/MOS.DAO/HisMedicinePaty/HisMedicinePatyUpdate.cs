using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicinePaty
{
    partial class HisMedicinePatyUpdate : EntityBase
    {
        public HisMedicinePatyUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_PATY>();
        }

        private BridgeDAO<HIS_MEDICINE_PATY> bridgeDAO;

        public bool Update(HIS_MEDICINE_PATY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEDICINE_PATY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
