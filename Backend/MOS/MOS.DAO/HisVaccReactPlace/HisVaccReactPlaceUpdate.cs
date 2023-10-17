using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVaccReactPlace
{
    partial class HisVaccReactPlaceUpdate : EntityBase
    {
        public HisVaccReactPlaceUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACC_REACT_PLACE>();
        }

        private BridgeDAO<HIS_VACC_REACT_PLACE> bridgeDAO;

        public bool Update(HIS_VACC_REACT_PLACE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_VACC_REACT_PLACE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
