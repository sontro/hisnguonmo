using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisBloodVolume
{
    partial class HisBloodVolumeCheck : EntityBase
    {
        public HisBloodVolumeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BLOOD_VOLUME>();
        }

        private BridgeDAO<HIS_BLOOD_VOLUME> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
