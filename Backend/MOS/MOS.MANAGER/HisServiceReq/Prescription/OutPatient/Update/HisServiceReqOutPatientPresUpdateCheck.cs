using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.OutPatient.Update
{
    class HisServiceReqOutPatientPresUpdateCheck: BusinessBase
    {
        internal HisServiceReqOutPatientPresUpdateCheck()
            : base()
        {
        }

        internal HisServiceReqOutPatientPresUpdateCheck(CommonParam param)
            : base(param)
        {
        }

        internal bool IsNotPaidSaleExpMest(OutPatientPresSDO data, HIS_SERVICE_REQ serviceReq, ref HIS_EXP_MEST saleExpMest)
        {
            bool valid = true;
            try
            {
                //Trong truong hop co tu dong tao phieu xuat ban thi check xem phieu xuat ban da duoc thanh toan chua
                if (HisServiceReqCFG.IS_AUTO_CREATE_SALE_EXP_MEST && (IsNotNullOrEmpty(data.ServiceReqMeties) || IsNotNullOrEmpty(data.ServiceReqMaties)))
                {
                    List<HIS_EXP_MEST> exps = new HisExpMestGet().GetByPrescriptionId(serviceReq.ID);
                    saleExpMest = IsNotNullOrEmpty(exps) ? exps[0] : null;
                    if (saleExpMest != null && saleExpMest.BILL_ID.HasValue)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_PhieuXuatBanTuongUngDaDuocThanhToan, saleExpMest.EXP_MEST_CODE);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
