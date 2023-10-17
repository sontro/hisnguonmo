using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDiseaseType
{
    partial class HisDiseaseTypeUpdate : EntityBase
    {
        public HisDiseaseTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DISEASE_TYPE>();
        }

        private BridgeDAO<HIS_DISEASE_TYPE> bridgeDAO;

        public bool Update(HIS_DISEASE_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_DISEASE_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
