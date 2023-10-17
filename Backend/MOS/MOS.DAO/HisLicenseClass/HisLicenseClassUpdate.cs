using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisLicenseClass
{
    partial class HisLicenseClassUpdate : EntityBase
    {
        public HisLicenseClassUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_LICENSE_CLASS>();
        }

        private BridgeDAO<HIS_LICENSE_CLASS> bridgeDAO;

        public bool Update(HIS_LICENSE_CLASS data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_LICENSE_CLASS> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
