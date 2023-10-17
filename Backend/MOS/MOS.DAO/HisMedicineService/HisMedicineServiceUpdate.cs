using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicineService
{
    partial class HisMedicineServiceUpdate : EntityBase
    {
        public HisMedicineServiceUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_SERVICE>();
        }

        private BridgeDAO<HIS_MEDICINE_SERVICE> bridgeDAO;

        public bool Update(HIS_MEDICINE_SERVICE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEDICINE_SERVICE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
