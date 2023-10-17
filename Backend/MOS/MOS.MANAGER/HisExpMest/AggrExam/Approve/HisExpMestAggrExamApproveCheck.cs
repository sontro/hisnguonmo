using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisExpMestBlood;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.Token;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisExpMest.Common.Export;

namespace MOS.MANAGER.HisExpMest.AggrExam.Approve
{
    partial class HisExpMestAggrExamApproveCheck : BusinessBase
    {
        internal HisExpMestAggrExamApproveCheck()
            : base()
        {

        }

        internal HisExpMestAggrExamApproveCheck(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool IsAllowed(HisExpMestSDO data, ref HIS_EXP_MEST aggrExpMest)
        {
            try
            {
                HIS_EXP_MEST tmpAggr = new HisExpMestGet().GetById(data.ExpMestId);

                if (tmpAggr == null || tmpAggr.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__THPK)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("ExpMestId ko hop le hoac loai ko phai la tong hop kham");
                    return false;
                }

                if (tmpAggr.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST)
                {
                    HIS_EXP_MEST_STT expMestStt = HisExpMestSttCFG.DATA.Where(o => o.ID == tmpAggr.EXP_MEST_STT_ID).FirstOrDefault();
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_PhieuDaOTrangThaiKhongChoPhepDuyet, expMestStt.EXP_MEST_STT_NAME);
                    return false;
                }

                WorkPlaceSDO workPlace = TokenManager.GetWorkPlace(data.ReqRoomId);
                if (workPlace == null || workPlace.MediStockId != tmpAggr.MEDI_STOCK_ID)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_BanDangKhongTruyCapVaoKhoXuatKhongChoPhepThucHien);
                    return false;
                }
                aggrExpMest = tmpAggr;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
            return true;
        }

        internal bool CheckUnpaidOutPres(HIS_EXP_MEST expMest)
        {
            bool valid = true;
            try
            {
                if (HisExpMestCFG.OUT_PRES_IS_CHECK_UNPAID)
                {
                    if (expMest != null && expMest.TDL_TREATMENT_ID.HasValue)
                    {
                        List<HIS_EXP_MEST_MATERIAL> materials = new HisExpMestMaterialGet().GetByAggrExpMestId(expMest.ID);
                        List<HIS_EXP_MEST_MEDICINE> medicines = new HisExpMestMedicineGet().GetByAggrExpMestId(expMest.ID);

                        HIS_TREATMENT treatment = new HisTreatmentGet().GetById(expMest.TDL_TREATMENT_ID.Value);
                        valid = valid && new HisExpMestExportCheck(param).CheckUnpaidOutPatientPrescription(treatment, expMest, medicines, materials);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }
    }
}
