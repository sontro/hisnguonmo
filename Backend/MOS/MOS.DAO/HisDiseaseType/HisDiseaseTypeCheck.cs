using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisDiseaseType
{
    partial class HisDiseaseTypeCheck : EntityBase
    {
        public HisDiseaseTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DISEASE_TYPE>();
        }

        private BridgeDAO<HIS_DISEASE_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
