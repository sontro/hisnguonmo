using Inventec.Core;
using Inventec.Common.Logging;
using MOS.MANAGER.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.MediRecord.DocumentViewCount
{
    class HisTreatmentDocumentViewCount : BusinessBase
    {
        internal HisTreatmentDocumentViewCount()
            : base()
        {
        }

        internal HisTreatmentDocumentViewCount(CommonParam param)
            : base(param)
        {
        }

        internal bool Run(long id, ref HIS_TREATMENT resultData)
        {

            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    HIS_TREATMENT data = new HisTreatmentGet().GetById(id);
                    if (data != null)
                    {
                        if (data.DOCUMENT_VIEW_COUNT.HasValue)
                        {
                            data.DOCUMENT_VIEW_COUNT += 1;
                        }
                        else
                        {
                            data.DOCUMENT_VIEW_COUNT = 1;
                        }

                        if (!DAOWorker.HisTreatmentDAO.Update(data))
                        {
                            MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatment_CapNhatThatBai);
                            throw new Exception("Cap nhat thong tin HisTreatment that bai." + LogUtil.TraceData("data", data));
                        }
                        result = true;
                        resultData = data;
                    }
                    else
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    }
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
