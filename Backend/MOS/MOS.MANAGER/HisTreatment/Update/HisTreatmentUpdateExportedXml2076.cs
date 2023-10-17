using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Update
{
    class HisTreatmentUpdateExportedXml2076 : BusinessBase
    {
        internal HisTreatmentUpdateExportedXml2076()
            : base()
        {

        }

        internal HisTreatmentUpdateExportedXml2076(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(List<long> listData)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(listData))
                {

                    string sql = DAOWorker.SqlDAO.AddInClause(listData, "UPDATE HIS_TREATMENT SET IS_EXPORTED_XML2076 = 1 WHERE %IN_CLAUSE%", "ID");
                    LogSystem.Info("Update Exported Xml2076 Sql: " + sql);
                    if (!DAOWorker.SqlDAO.Execute(sql))
                    {
                        throw new Exception("Update Exported Xml2076 that bai");
                    }

                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }
    }
}
