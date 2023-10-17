using Inventec.Core;
using MOS.DAO.StagingObject;
using MOS.DynamicDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.DAO.HisSereServTemp
{
    public partial class HisSereServTempDAO : EntityBase
    {
        public List<HisSereServTempDTO> GetDynamic(HisSereServTempSO search, CommonParam param)
        {
            List<HisSereServTempDTO> result = new List<HisSereServTempDTO>();
            try
            {
                result = GetWorker.GetDynamic(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }
            return result;
        }
    }
}
