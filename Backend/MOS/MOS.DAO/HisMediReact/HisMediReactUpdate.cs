using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMediReact
{
    partial class HisMediReactUpdate : EntityBase
    {
        public HisMediReactUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_REACT>();
        }

        private BridgeDAO<HIS_MEDI_REACT> bridgeDAO;

        public bool Update(HIS_MEDI_REACT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEDI_REACT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
