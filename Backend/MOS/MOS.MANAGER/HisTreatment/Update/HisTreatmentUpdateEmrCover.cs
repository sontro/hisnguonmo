using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Update
{
    class HisTreatmentUpdateEmrCover : BusinessBase
    {
        internal HisTreatmentUpdateEmrCover()
            : base()
        {
            this.Init();
        }

        internal HisTreatmentUpdateEmrCover(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
        }

        internal bool Run(HisTreatmentEmrCoverSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT raw = null;
                HisTreatmentCheck checker = new HisTreatmentCheck(param);
                valid = valid && (!String.IsNullOrWhiteSpace(data.TreatmentCode) || checker.VerifyId(data.TreatmentId, ref raw));
                valid = valid && (String.IsNullOrWhiteSpace(data.TreatmentCode) || checker.IsVerifyTreatmentCode(data.TreatmentCode, ref raw));
                if (valid && data.EmrCoverTypeId <= 0)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("EmrCoverTypeId invalid");
                }
                if (valid)
                {
                    List<string> sqls = new List<string>();
                    if (raw.MEDI_RECORD_ID.HasValue)
                    {
                        string sqlTreatment = String.Format("UPDATE HIS_TREATMENT SET EMR_COVER_TYPE_ID = {0} WHERE MEDI_RECORD_ID = {1} ", data.EmrCoverTypeId, raw.MEDI_RECORD_ID.Value);
                        string sqlMediRecord = String.Format("UPDATE HIS_MEDI_RECORD SET EMR_COVER_TYPE_ID = {0} WHERE ID = {1} ", data.EmrCoverTypeId, raw.MEDI_RECORD_ID.Value);
                        sqls.Add(sqlTreatment);
                        sqls.Add(sqlMediRecord);
                    }
                    else
                    {
                        string sqlTreatment = String.Format("UPDATE HIS_TREATMENT SET EMR_COVER_TYPE_ID = {0} WHERE ID = {1} ", data.EmrCoverTypeId, raw.ID);
                        sqls.Add(sqlTreatment);
                    }

                    if (!DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Update EMR_COVER_TYPE_ID cho HIS_TREATMENT, MEDI_RECORD_ID that bai.");
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
