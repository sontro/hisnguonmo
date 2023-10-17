using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Aggr.Approve
{
    class HisImpMestAggrApproveCheck : BusinessBase
    {
        internal HisImpMestAggrApproveCheck()
            : base()
        {
        }

        internal HisImpMestAggrApproveCheck(CommonParam param)
            : base(param)
        {
        }

        /// <summary>
        /// Kiem tra xem du lieu truyen vao co hop le hay ko
        /// </summary>
        /// <param name="data"></param>
        /// <param name="impMestMedicines"></param>
        /// <param name="impMestMaterials"></param>
        /// <returns></returns>
        internal bool IsValidData(ImpMestAggrApprovalSDO data, HIS_IMP_MEST impMest, List<HIS_IMP_MEST_MEDICINE> impMestMedicines, List<HIS_IMP_MEST_MATERIAL> impMestMaterials, ref List<MobaMedicineSDO> rejectedMedicineList, ref List<MobaMaterialSDO> rejectedMaterialList)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(impMestMedicines))
                {
                    rejectedMedicineList = new List<MobaMedicineSDO>();

                    var groups = impMestMedicines.GroupBy(o => o.MEDICINE_ID).ToList();

                    List<MobaMedicineSDO> reqs = groups.Select(o => new MobaMedicineSDO
                    {
                        Amount = o.Sum(t => t.REQ_AMOUNT ?? 0),
                        MedicineId = o.Key
                    }).ToList();

                    foreach (MobaMedicineSDO req in reqs)
                    {
                        MobaMedicineSDO approval = IsNotNullOrEmpty(data.ApprovalMedicines) ? data.ApprovalMedicines.Where(o => o.MedicineId == req.MedicineId).FirstOrDefault() : null;
                        if (approval == null || approval.Amount < 0 || approval.Amount > req.Amount)
                        {
                            LogSystem.Warn("Du lieu ko hop le: approval == null || approval.Amount < 0 || approval.Amount > req.Amount. MedicineId: " + req.MedicineId);
                            return false;
                        }

                        if (approval == null || approval.Amount < req.Amount)
                        {
                            MobaMedicineSDO rejected = new MobaMedicineSDO();
                            rejected.Amount = req.Amount - (approval != null ? approval.Amount : 0);
                            rejected.MedicineId = req.MedicineId;
                            rejected.Note = approval != null ? approval.Note : null;
                            rejectedMedicineList.Add(rejected);
                        }
                    }
                }

                if (IsNotNullOrEmpty(impMestMaterials))
                {
                    rejectedMaterialList = new List<MobaMaterialSDO>();

                    var groups = impMestMaterials.GroupBy(o => o.MATERIAL_ID).ToList();

                    List<MobaMaterialSDO> reqs = groups.Select(o => new MobaMaterialSDO
                    {
                        Amount = o.Sum(t => t.REQ_AMOUNT ?? 0),
                        MaterialId = o.Key
                    }).ToList();

                    foreach (MobaMaterialSDO req in reqs)
                    {
                        MobaMaterialSDO approval = IsNotNullOrEmpty(data.ApprovalMaterials) ? data.ApprovalMaterials.Where(o => o.MaterialId == req.MaterialId).FirstOrDefault() : null;
                        if (approval == null || approval.Amount < 0 || approval.Amount > req.Amount)
                        {
                            LogSystem.Warn("Du lieu ko hop le: approval == null || approval.Amount < 0 || approval.Amount > req.Amount. MaterialId: " + req.MaterialId);
                            return false;
                        }

                        if (approval == null || approval.Amount < req.Amount)
                        {
                            MobaMaterialSDO rejected = new MobaMaterialSDO();
                            rejected.Amount = req.Amount - (approval != null ? approval.Amount : 0);
                            rejected.MaterialId = req.MaterialId;
                            rejected.Note = approval != null ? approval.Note : null;
                            rejectedMaterialList.Add(rejected);
                        }
                    }
                }

                //Neu co du lieu tu choi ma ko chon kho de luu thuoc/vat tu tu choi thi bao loi
                if ((IsNotNullOrEmpty(rejectedMedicineList) || IsNotNullOrEmpty(rejectedMaterialList)) && !data.RejectedMediStockId.HasValue)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisImpMest_ChuaChonKhoDeLuuThuocVatTuTuChoiDuyet);
                    return false;
                }

                V_HIS_MEDI_STOCK rejectedMediStock = HisMediStockCFG.DATA.Where(o => o.ID == data.RejectedMediStockId).FirstOrDefault();
                if (rejectedMediStock != null)
                {
                    if (rejectedMediStock.DEPARTMENT_ID != impMest.REQ_DEPARTMENT_ID)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisImpMest_KhoKhongThuocKhoaVoiKhoaYeuCauTra, rejectedMediStock.MEDI_STOCK_NAME);
                        return false;
                    }
                    if (rejectedMediStock.IS_FOR_REJECTED_MOBA != Constant.IS_TRUE)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisImpMest_KhoKhongChoPhepLuuThuocVatTuTuChoiDuyet, rejectedMediStock.MEDI_STOCK_NAME);
                        return false;
                    }
                }
                return true;
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
