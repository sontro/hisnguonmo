using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSpeciality
{
    partial class HisSpecialityUpdate : EntityBase
    {
        public HisSpecialityUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SPECIALITY>();
        }

        private BridgeDAO<HIS_SPECIALITY> bridgeDAO;

        public bool Update(HIS_SPECIALITY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SPECIALITY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
