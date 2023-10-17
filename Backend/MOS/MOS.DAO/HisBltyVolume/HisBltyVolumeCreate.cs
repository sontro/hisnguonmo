using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisBltyVolume
{
    partial class HisBltyVolumeCreate : EntityBase
    {
        public HisBltyVolumeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BLTY_VOLUME>();
        }

        private BridgeDAO<HIS_BLTY_VOLUME> bridgeDAO;

        public bool Create(HIS_BLTY_VOLUME data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_BLTY_VOLUME> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
