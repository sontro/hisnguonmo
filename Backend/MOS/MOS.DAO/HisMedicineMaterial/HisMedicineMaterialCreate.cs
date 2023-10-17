using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicineMaterial
{
    partial class HisMedicineMaterialCreate : EntityBase
    {
        public HisMedicineMaterialCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_MATERIAL>();
        }

        private BridgeDAO<HIS_MEDICINE_MATERIAL> bridgeDAO;

        public bool Create(HIS_MEDICINE_MATERIAL data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEDICINE_MATERIAL> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
