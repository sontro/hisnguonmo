using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisKskContract
{
    partial class HisKskContractUpdate : EntityBase
    {
        public HisKskContractUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_CONTRACT>();
        }

        private BridgeDAO<HIS_KSK_CONTRACT> bridgeDAO;

        public bool Update(HIS_KSK_CONTRACT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_KSK_CONTRACT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
