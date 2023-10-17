using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisQcType
{
    partial class HisQcTypeUpdate : EntityBase
    {
        public HisQcTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_QC_TYPE>();
        }

        private BridgeDAO<HIS_QC_TYPE> bridgeDAO;

        public bool Update(HIS_QC_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_QC_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
