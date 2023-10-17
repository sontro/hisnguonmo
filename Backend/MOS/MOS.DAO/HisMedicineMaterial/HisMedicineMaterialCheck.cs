using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMedicineMaterial
{
    partial class HisMedicineMaterialCheck : EntityBase
    {
        public HisMedicineMaterialCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_MATERIAL>();
        }

        private BridgeDAO<HIS_MEDICINE_MATERIAL> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
