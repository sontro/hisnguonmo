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
using MOS.MANAGER.HisSereServ;
using MOS.UTILITY;
using MOS.MANAGER.HisMedicineBean;
using MOS.MANAGER.HisMaterialBean;

namespace MOS.MANAGER.HisExpMest.AggrExam.Export
{
    partial class HisExpMestAggrExamExportCheck : BusinessBase
    {
        internal HisExpMestAggrExamExportCheck()
            : base()
        {

        }

        internal HisExpMestAggrExamExportCheck(CommonParam paramCreate)
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

                if (tmpAggr.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE)
                {
                    HIS_EXP_MEST_STT expMestStt = HisExpMestSttCFG.DATA.Where(o => o.ID == tmpAggr.EXP_MEST_STT_ID).FirstOrDefault();
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_PhieuDaOTrangThaiKhongChoPhepThucXuat, expMestStt.EXP_MEST_STT_NAME);
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

        internal bool Children(long aggrExpMestId, ref List<HIS_EXP_MEST> children)
        {
            try
            {
                //Chi lay cac phieu dang o trang thai da duyet
                //Muc dich: tranh truong hop bi loi du lieu, dan den trang thai phieu linh la đã hoàn thành nhưng vẫn
                //còn các phiếu con chưa hoàn thành thì chỉ cần vào DB cập nhật lại trạng thái của phiếu lĩnh rồi
                //thực hiện thực xuất lại.
                HisExpMestFilterQuery filter = new HisExpMestFilterQuery();
                filter.AGGR_EXP_MEST_ID = aggrExpMestId;
                filter.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE;
                children = new HisExpMestGet().Get(filter);
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        internal bool CheckUnpaidOutPatientPrescription(HIS_TREATMENT treatment, HIS_EXP_MEST aggrExp, List<HIS_EXP_MEST> children)
        {
            try
            {
                //Neu la don phong kham thi kiem tra xem con no tien vien phi ko
                if (aggrExp != null && aggrExp.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__THPK)
                {
                    //Neu la don phong kham thi kiem tra xem con no tien vien phi ko
                    if (HisExpMestCFG.CHECK_UNPAID_PRES_OPTION == HisExpMestCFG.CheckUnpaidPresOption.BY_PRES_TYPE
                        || (HisExpMestCFG.CHECK_UNPAID_PRES_OPTION == HisExpMestCFG.CheckUnpaidPresOption.BY_TREATMENT_TYPE && treatment != null && treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM))
                    {
                        List<long> notCheckPatientTypeIds = HisPatientTypeCFG.DATA.Where(o => o.IS_NOT_CHECK_FEE_WHEN_EXP_PRES == Constant.IS_TRUE).Select(o => o.ID).ToList();

                        List<HIS_SERE_SERV> hisSereServs = new HisSereServGet().GetByServiceReqIds(children.Select(s => s.SERVICE_REQ_ID.Value).ToList());
                        //Lay ra xem co bao nhieu thuoc/vat tu thuoc phieu xuat ko phai la hao phi
                        int countNotAllow = !IsNotNullOrEmpty(hisSereServs) ? 0 : hisSereServs.Where(o => o.IS_EXPEND != MOS.UTILITY.Constant.IS_TRUE && (notCheckPatientTypeIds == null || !notCheckPatientTypeIds.Contains(o.PATIENT_TYPE_ID))).Count();

                        //Neu co thi thuc hien kiem tra
                        if (countNotAllow > 0)
                        {
                            V_HIS_TREATMENT_FEE_1 treatmentFee = new HisTreatmentGet().GetFeeView1ById(aggrExp.TDL_TREATMENT_ID.Value);

                            if (treatmentFee.IS_ACTIVE == MOS.UTILITY.Constant.IS_TRUE)
                            {
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_HoSoDieuTriChuaDuocDuyetKhoaTaiChinhKhongChoThucXuat, treatmentFee.TREATMENT_CODE);
                                return false;
                            }

                            decimal? unpaid = new HisTreatmentGet().GetUnpaid(treatmentFee);
                            if (!unpaid.HasValue)
                            {
                                LogSystem.Warn("Loi du lieu");
                                return false;
                            }

                            //tranh truong hop lam tron den 4 so sau phan thap phan dan den nguoi dung ko the thanh toan het toan bo chi phi
                            if (unpaid.Value > 0.0001m)
                            {
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_HoSoDieuTriChuaDongDuTienKhongChoThucXuat, treatmentFee.TREATMENT_CODE);
                                return false;
                            }

                            hisSereServs = hisSereServs != null ? hisSereServs.Where(o => o.AMOUNT > 0 && o.VIR_TOTAL_PATIENT_PRICE > 0).ToList() : null;
                            if (IsNotNullOrEmpty(hisSereServs))
                            {
                                HisSereServCheck ssChecker = new HisSereServCheck(param);
                                if (!ssChecker.HasBillOrDepositAndNoRepay(hisSereServs))
                                {
                                    LogSystem.Warn("Don chua duoc thanh toan hoac chua tam ung hoac da tam ung ma da hoan ung");
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
            return true;
        }

        internal bool Children(long aggrExpMestId, ref List<HIS_EXP_MEST> children, ref List<HIS_EXP_MEST_MEDICINE> expMestMedicines, ref List<HIS_EXP_MEST_MATERIAL> expMestMaterials)
        {
            try
            {
                //Chi lay cac phieu dang o trang thai da duyet
                //Muc dich: tranh truong hop bi loi du lieu, dan den trang thai phieu linh la đã hoàn thành nhưng vẫn
                //còn các phiếu con chưa hoàn thành thì chỉ cần vào DB cập nhật lại trạng thái của phiếu lĩnh rồi
                //thực hiện thực xuất lại.
                HisExpMestFilterQuery filter = new HisExpMestFilterQuery();
                filter.AGGR_EXP_MEST_ID = aggrExpMestId;
                filter.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE;
                children = new HisExpMestGet().Get(filter);

                //Lay cac du lieu chua duoc xuat va da duoc duyet
                HisExpMestMaterialFilterQuery materialFilter = new HisExpMestMaterialFilterQuery();
                materialFilter.TDL_AGGR_EXP_MEST_ID__OR__EXP_MEST_ID = aggrExpMestId;
                materialFilter.IS_EXPORT = false;
                materialFilter.IS_APPROVED = true;

                expMestMaterials = new HisExpMestMaterialGet().Get(materialFilter);

                //Lay cac du lieu chua duoc xuat va da duoc duyet
                HisExpMestMedicineFilterQuery medicineFilter = new HisExpMestMedicineFilterQuery();
                medicineFilter.TDL_AGGR_EXP_MEST_ID__OR__EXP_MEST_ID = aggrExpMestId;
                medicineFilter.IS_EXPORT = false;
                medicineFilter.IS_APPROVED = true;

                expMestMedicines = new HisExpMestMedicineGet().Get(medicineFilter);

                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        internal bool CheckValidData(HIS_EXP_MEST aggrExpMest, List<HIS_EXP_MEST_MEDICINE> medicines, List<HIS_EXP_MEST_MATERIAL> materials)
        {
            bool valid = true;
            try
            {
                List<HIS_EXP_MEST_MEDICINE> listMedicineErr = new List<HIS_EXP_MEST_MEDICINE>();
                List<HIS_EXP_MEST_MATERIAL> listMaterialErr = new List<HIS_EXP_MEST_MATERIAL>();

                List<long> expMestMedicineIds = IsNotNullOrEmpty(medicines) ? medicines.Select(o => o.ID).ToList() : new List<long>();
                List<long> expMestMaterialIds = IsNotNullOrEmpty(materials) ? materials.Select(o => o.ID).ToList() : new List<long>();
                List<HIS_MEDICINE_BEAN> medicineBeans = (new HisMedicineBeanGet().GetByExpMestMedicineIds(expMestMedicineIds)) ?? new List<HIS_MEDICINE_BEAN>();
                List<HIS_MATERIAL_BEAN> materialBeans = (new HisMaterialBeanGet().GetByExpMestMaterialIds(expMestMaterialIds)) ?? new List<HIS_MATERIAL_BEAN>();
                if (IsNotNullOrEmpty(medicines))
                {
                    foreach (var medicine in medicines)
                    {
                        if (medicine == null)
                            continue;
                        if (medicine.AMOUNT != medicineBeans.Where(o => o.EXP_MEST_MEDICINE_ID == medicine.ID).Sum(o => o.AMOUNT))
                            listMedicineErr.Add(medicine);
                    }
                }
                if (IsNotNullOrEmpty(materials))
                {
                    foreach (var material in materials)
                    {
                        if (material == null)
                            continue;
                        if (material.AMOUNT != materialBeans.Where(o => o.EXP_MEST_MATERIAL_ID == material.ID).Sum(o => o.AMOUNT))
                            listMaterialErr.Add(material);
                    }
                }
                if (listMedicineErr.Count > 0 || listMaterialErr.Count > 0)
                {
                    valid = false;
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.LoiDuLieuKhongHopLe);
                    Inventec.Common.Logging.LogSystem.Warn("Loi du lieu,Khong cho phep xuat phieu " + aggrExpMest.EXP_MEST_CODE);
                    if (listMedicineErr.Count > 0)
                    {
                        Inventec.Common.Logging.LogSystem.Warn("__Thuoc loi: " + Inventec.Common.Logging.LogUtil.TraceData("listMedicineErr", listMedicineErr));
                    }
                    if (listMaterialErr.Count > 0)
                    {
                        Inventec.Common.Logging.LogSystem.Warn("__Vat tu loi: " + Inventec.Common.Logging.LogUtil.TraceData("listMaterialErr", listMaterialErr));
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
