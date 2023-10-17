using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisBloodVolume
{
    partial class HisBloodVolumeCreate : EntityBase
    {
        public HisBloodVolumeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BLOOD_VOLUME>();
        }

        private BridgeDAO<HIS_BLOOD_VOLUME> bridgeDAO;

        public bool Create(HIS_BLOOD_VOLUME data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_BLOOD_VOLUME> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
