using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBloodVolume
{
    partial class HisBloodVolumeUpdate : EntityBase
    {
        public HisBloodVolumeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BLOOD_VOLUME>();
        }

        private BridgeDAO<HIS_BLOOD_VOLUME> bridgeDAO;

        public bool Update(HIS_BLOOD_VOLUME data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_BLOOD_VOLUME> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
