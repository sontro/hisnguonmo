using Inventec.Core;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.DAO.HisSereServTein
{
    public partial class HisSereServTeinDAO : EntityBase
    {
        public List<V_HIS_SERE_SERV_TEIN_1> GetView1(HisSereServTeinSO search, CommonParam param)
        {
            List<V_HIS_SERE_SERV_TEIN_1> result = new List<V_HIS_SERE_SERV_TEIN_1>();
            try
            {
                result = GetWorker.GetView1(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }
            return result;
        }

        public V_HIS_SERE_SERV_TEIN_1 GetView1ById(long id, HisSereServTeinSO search)
        {
            V_HIS_SERE_SERV_TEIN_1 result = null;

            try
            {
                result = GetWorker.GetView1ById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }
    }
}
