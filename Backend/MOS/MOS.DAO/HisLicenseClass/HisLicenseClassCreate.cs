using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisLicenseClass
{
    partial class HisLicenseClassCreate : EntityBase
    {
        public HisLicenseClassCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_LICENSE_CLASS>();
        }

        private BridgeDAO<HIS_LICENSE_CLASS> bridgeDAO;

        public bool Create(HIS_LICENSE_CLASS data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_LICENSE_CLASS> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
