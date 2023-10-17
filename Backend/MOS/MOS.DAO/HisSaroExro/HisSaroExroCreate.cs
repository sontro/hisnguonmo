using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisSaroExro
{
    partial class HisSaroExroCreate : EntityBase
    {
        public HisSaroExroCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SARO_EXRO>();
        }

        private BridgeDAO<HIS_SARO_EXRO> bridgeDAO;

        public bool Create(HIS_SARO_EXRO data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SARO_EXRO> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
