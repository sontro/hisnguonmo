using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMediReactSum
{
    partial class HisMediReactSumUpdate : EntityBase
    {
        public HisMediReactSumUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_REACT_SUM>();
        }

        private BridgeDAO<HIS_MEDI_REACT_SUM> bridgeDAO;

        public bool Update(HIS_MEDI_REACT_SUM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEDI_REACT_SUM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
