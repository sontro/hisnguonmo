using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisGender
{
    partial class HisGenderUpdate : EntityBase
    {
        public HisGenderUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_GENDER>();
        }

        private BridgeDAO<HIS_GENDER> bridgeDAO;

        public bool Update(HIS_GENDER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_GENDER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
