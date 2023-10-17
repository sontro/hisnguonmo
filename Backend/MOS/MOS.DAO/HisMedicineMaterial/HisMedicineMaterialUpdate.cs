using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicineMaterial
{
    partial class HisMedicineMaterialUpdate : EntityBase
    {
        public HisMedicineMaterialUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_MATERIAL>();
        }

        private BridgeDAO<HIS_MEDICINE_MATERIAL> bridgeDAO;

        public bool Update(HIS_MEDICINE_MATERIAL data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEDICINE_MATERIAL> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
