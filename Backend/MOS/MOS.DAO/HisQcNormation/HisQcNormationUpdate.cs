using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisQcNormation
{
    partial class HisQcNormationUpdate : EntityBase
    {
        public HisQcNormationUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_QC_NORMATION>();
        }

        private BridgeDAO<HIS_QC_NORMATION> bridgeDAO;

        public bool Update(HIS_QC_NORMATION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_QC_NORMATION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
