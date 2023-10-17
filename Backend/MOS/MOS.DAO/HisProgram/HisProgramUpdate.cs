using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisProgram
{
    partial class HisProgramUpdate : EntityBase
    {
        public HisProgramUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PROGRAM>();
        }

        private BridgeDAO<HIS_PROGRAM> bridgeDAO;

        public bool Update(HIS_PROGRAM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_PROGRAM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
