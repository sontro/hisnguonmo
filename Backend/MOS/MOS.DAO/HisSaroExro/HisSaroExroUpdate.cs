using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSaroExro
{
    partial class HisSaroExroUpdate : EntityBase
    {
        public HisSaroExroUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SARO_EXRO>();
        }

        private BridgeDAO<HIS_SARO_EXRO> bridgeDAO;

        public bool Update(HIS_SARO_EXRO data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SARO_EXRO> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
