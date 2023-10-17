using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisContraindication
{
    partial class HisContraindicationUpdate : EntityBase
    {
        public HisContraindicationUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CONTRAINDICATION>();
        }

        private BridgeDAO<HIS_CONTRAINDICATION> bridgeDAO;

        public bool Update(HIS_CONTRAINDICATION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_CONTRAINDICATION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
