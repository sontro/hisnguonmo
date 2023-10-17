using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBedType
{
    partial class HisBedTypeUpdate : EntityBase
    {
        public HisBedTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BED_TYPE>();
        }

        private BridgeDAO<HIS_BED_TYPE> bridgeDAO;

        public bool Update(HIS_BED_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_BED_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
