using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisQcNormation
{
    partial class HisQcNormationCreate : EntityBase
    {
        public HisQcNormationCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_QC_NORMATION>();
        }

        private BridgeDAO<HIS_QC_NORMATION> bridgeDAO;

        public bool Create(HIS_QC_NORMATION data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_QC_NORMATION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
