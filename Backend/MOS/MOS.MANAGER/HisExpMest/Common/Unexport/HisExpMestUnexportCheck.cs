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
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisSeseDepoRepay;
using MOS.MANAGER.HisTransfusionSum;
using MOS.MANAGER.HisBcsMetyReqDt;
using MOS.MANAGER.HisBcsMatyReqDt;
using MOS.MANAGER.HisExpMestMetyReq;
using MOS.MANAGER.HisBcsMetyReqReq;
using MOS.MANAGER.HisExpMestMatyReq;
using MOS.MANAGER.HisBcsMatyReqReq;
using MOS.MANAGER.HisMediStockMaty;
using MOS.MANAGER.HisMediStockMety;
using MOS.MANAGER.HisMedicineType;
using MOS.MANAGER.HisMedicineBean;
using MOS.MANAGER.HisMaterialBean;
using MOS.MANAGER.HisImpMestMaterial;

namespace MOS.MANAGER.HisExpMest.Common.Unexport
{
    partial class HisExpMestUnexportCheck : BusinessBase
    {
        internal HisExpMestUnexportCheck()
            : base()
        {

        }

        internal HisExpMestUnexportCheck(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool IsAllowed(HisExpMestSDO data, bool isAuto, ref HIS_EXP_MEST expMest)
        {
            try
            {
                HIS_EXP_MEST tmp = new HisExpMestGet().GetById(data.ExpMestId);

                if (tmp.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE)
                {
                    HIS_EXP_MEST_STT expMestStt = HisExpMestSttCFG.DATA.Where(o => o.ID == tmp.EXP_MEST_STT_ID).FirstOrDefault();
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_PhieuDaOTrangThaiKhongChoPhepHuyThucXuat, expMestStt.EXP_MEST_STT_NAME);
                    return false;
                }
                if (tmp.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__PL || tmp.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__THPK)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Error("Loai phieu xuat la phieu tong hop (phieu linh). Khong duoc thuc hien chuc nang nay");
                    return false;
                }
                if (!isAuto)
                {
                    WorkPlaceSDO workPlace = TokenManager.GetWorkPlace(data.ReqRoomId);
                    if (workPlace == null || workPlace.MediStockId != tmp.MEDI_STOCK_ID)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_BanDangKhongTruyCapVaoKhoXuatKhongChoPhepThucHien);
                        return false;
                    }
                }
                expMest = tmp;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
            return true;
        }

        internal bool IsUnlockTreatment(HIS_EXP_MEST expMest, ref HIS_TREATMENT treatment)
        {
            try
            {
                bool valid = true;
                
                if (expMest.TDL_TREATMENT_ID.HasValue
                    && (expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DM || HisExpMestCFG.DONOT_ALLOW_UNEXPORT_AFTER_TREATMENT_FINISHING_IN_CASE_OF_INPATIENT))
                {
                    treatment = new HisTreatmentGet().GetById(expMest.TDL_TREATMENT_ID.Value);
                    HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                    if (expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DM)
                    {
                        valid = valid && treatmentChecker.IsUnLock(treatment);
                        valid = valid && treatmentChecker.IsUnTemporaryLock(treatment);
                        valid = valid && treatmentChecker.IsUnpause(treatment);
                        valid = valid && treatmentChecker.IsUnLockHein(treatment);
                    }
                    else
                    {
                        valid = treatment.TDL_TREATMENT_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU
                            || treatmentChecker.IsUnpause(treatment);
                    }
                }
                return valid;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// Kiem tra xem da duoc thanh toan chua
        /// Luu y: Chỉ kiểm tra với đơn máu. Do
        /// - Đơn phòng khám và đơn tủ trực thì sẽ thanh toán xong mới thực xuất
        /// - Đơn nội trú sẽ xử lý ở nghiệp vụ hủy thực xuất phiếu lĩnh
        /// </summary>
        /// <param name="children"></param>
        /// <param name="treatments"></param>
        /// <returns></returns>
        internal bool HasNoBill(HIS_EXP_MEST expMest, ref List<HIS_SERE_SERV> existedSereServ)
        {
            try
            {
                if (expMest.SERVICE_REQ_ID.HasValue && expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DM)
                {
                    List<HIS_SERE_SERV> sereServs = new HisSereServGet().GetByServiceReqId(expMest.SERVICE_REQ_ID.Value);
                    List<HIS_SERE_SERV> bloodSereServs = IsNotNullOrEmpty(sereServs) ? sereServs.Where(o => o.SERVICE_REQ_ID == expMest.SERVICE_REQ_ID.Value).ToList() : null;

                    if (IsNotNullOrEmpty(bloodSereServs))
                    {
                        List<long> sereServIds = bloodSereServs.Select(o => o.ID).ToList();
                        List<HIS_SERE_SERV_BILL> sereServBills = new HisSereServBillGet().GetNoCancelBySereServIds(sereServIds);
                        List<HIS_SERE_SERV_DEPOSIT> sereServDeposits = new HisSereServDepositGet().GetNoCancelBySereServIds(sereServIds);

                        if (IsNotNullOrEmpty(sereServBills))
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisSereServ_DichVuDaThanhToanKhongChoPhepHuy);
                            return false;
                        }

                        if (IsNotNullOrEmpty(sereServDeposits))
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisSereServ_DichVuDaTamUngKhongChoPhepHuy);
                            return false;
                        }
                    }

                    existedSereServ = sereServs;
                }
                else if (expMest.SERVICE_REQ_ID.HasValue && expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT)
                {
                    List<HIS_SERE_SERV_BILL> sereServBills = new HisSereServBillGet().GetNoCancelByTreatmentId(expMest.TDL_TREATMENT_ID.Value);
                    if (IsNotNullOrEmpty(sereServBills))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisSereServ_DichVuDaThanhToanKhongChoPhepHuy);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// Kiem tra xem cac phieu xuat da co phieu nhap thu hoi nao chua
        /// </summary>
        /// <param name="children"></param>
        /// <returns></returns>
        internal bool HasNoMoba(HIS_EXP_MEST expMest)
        {
            try
            {
                List<HIS_IMP_MEST> mobaImpMests = new HisImpMestGet().GetByMobaExpMestId(expMest.ID);
                if (IsNotNullOrEmpty(mobaImpMests))
                {
                    List<string> impMestCodes = mobaImpMests.Select(o => o.IMP_MEST_CODE).ToList();
                    string impMestCodeStr = string.Join(",", impMestCodes);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_CacPhieuDaCoPhieuNhapHoi, impMestCodeStr);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// Neu la phieu xuat chuyen kho/bu co so thi kiem tra xem da co phieu nhap chuyen kho nao chua
        /// </summary>
        /// <param name="children"></param>
        /// <returns></returns>
        internal bool HasNoImpMest(HIS_EXP_MEST expMest)
        {
            try
            {
                if (expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BCS
                    || expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__CK)
                {
                    List<HIS_IMP_MEST> impMests = new HisImpMestGet().GetByChmsExpMestId(expMest.ID);
                    if (IsNotNullOrEmpty(impMests))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_DaTonTaiPhieuNhap, impMests[0].IMP_MEST_CODE);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// Neu la phieu xuat don tu truc/ bu co so thi kiem tra xem da thuoc phieu bu co so nao chua
        /// </summary>
        /// <param name="children"></param>
        /// <returns></returns>
        internal bool HasNoXbttExpMest(HIS_EXP_MEST expMest)
        {
            try
            {
                if (expMest.XBTT_EXP_MEST_ID.HasValue)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMes_PhieuXuaDaThuocPhieuBuCoSoCoMa, expMest.TDL_XBTT_EXP_MEST_CODE);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
            return true;
        }

        internal bool HasNoTransfusionSum(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_BLOOD> expMestBloods)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(expMestBloods) && expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DM)
                {
                    List<HIS_TRANSFUSION_SUM> exists = new HisTransfusionSumGet().GetByExpMestBloodIds(expMestBloods.Select(s => s.ID).ToList());
                    if (IsNotNullOrEmpty(exists))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMestBlood_TonTaiTuiMauDaDuocTruyenMau);
                        return false;
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

        /// <summary>
        /// Kiem tra xem da cho thong tin chot ky chua
        /// </summary>
        /// <param name="expMest"></param>
        /// <returns></returns>
        internal bool HasNoMediStockPeriod(HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MEDICINE> expMestMedicines, ref List<HIS_EXP_MEST_MATERIAL> expMestMaterials, ref List<HIS_EXP_MEST_BLOOD> expMestBloods)
        {
            try
            {
                //Lay cac du lieu da duoc xuat
                HisExpMestMedicineFilterQuery filter = new HisExpMestMedicineFilterQuery();
                filter.EXP_MEST_ID = expMest.ID;
                filter.IS_EXPORT = true;
                expMestMedicines = new HisExpMestMedicineGet().Get(filter);

                HisExpMestMaterialFilterQuery materialFilter = new HisExpMestMaterialFilterQuery();
                materialFilter.EXP_MEST_ID = expMest.ID;
                materialFilter.IS_EXPORT = true;
                expMestMaterials = new HisExpMestMaterialGet().Get(materialFilter);

                expMestBloods = new HisExpMestBloodGet().GetExportedByExpMestId(expMest.ID);

                bool check = true;
                check = check && (!IsNotNullOrEmpty(expMestMedicines) || !expMestMedicines.Exists(t => t.MEDI_STOCK_PERIOD_ID.HasValue));
                check = check && (!IsNotNullOrEmpty(expMestMaterials) || !expMestMaterials.Exists(t => t.MEDI_STOCK_PERIOD_ID.HasValue));
                check = check && (!IsNotNullOrEmpty(expMestBloods) || !expMestBloods.Exists(t => t.MEDI_STOCK_PERIOD_ID.HasValue));
                if (!check)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_DaDuocChotKyKhongChoPhepCapNhatHoacXoa);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        internal bool HasNoBcsDetail(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MEDICINE> expMestMedicines, List<HIS_EXP_MEST_MATERIAL> expMestMaterials)
        {
            bool valid = true;
            try
            {
                if (expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DTT)
                {
                    if (IsNotNullOrEmpty(expMestMedicines))
                    {
                        var existsDetails = new HisBcsMetyReqDtGet().GetByExpMestMedicineIds(expMestMedicines.Select(s => s.ID).ToList());
                        if (IsNotNullOrEmpty(existsDetails))
                        {
                            string expMestCodes = String.Join(",", existsDetails.Select(s => s.TDL_XBTT_EXP_MEST_CODE).Distinct().ToList());
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_ThuocDaDuocBuCoSoOCacPhieuXuatSau, expMestCodes);
                            return false;
                        }
                    }

                    if (IsNotNullOrEmpty(expMestMaterials))
                    {
                        var existsDetails = new HisBcsMatyReqDtGet().GetByExpMestMaterialIds(expMestMaterials.Select(s => s.ID).ToList());
                        if (IsNotNullOrEmpty(existsDetails))
                        {
                            string expMestCodes = String.Join(",", existsDetails.Select(s => s.TDL_XBTT_EXP_MEST_CODE).Distinct().ToList());
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_VatTuDaDuocBuCoSoOCacPhieuXuatSau, expMestCodes);
                            return false;
                        }
                    }
                }
                else if (expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BCS)
                {
                    List<HIS_EXP_MEST_METY_REQ> metyReqs = new HisExpMestMetyReqGet(param).GetByExpMestId(expMest.ID);
                    if (IsNotNullOrEmpty(metyReqs))
                    {
                        var existsDetails = new HisBcsMetyReqReqGet().GetByPreExpMestMetyReqIds(metyReqs.Select(s => s.ID).ToList());
                        if (IsNotNullOrEmpty(existsDetails))
                        {
                            string expMestCodes = String.Join(",", existsDetails.Select(s => s.TDL_XBTT_EXP_MEST_CODE).Distinct().ToList());
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_ThuocDaDuocBuCoSoOCacPhieuXuatSau, expMestCodes);
                            return false;
                        }
                    }
                    List<HIS_EXP_MEST_MATY_REQ> matyReqs = new HisExpMestMatyReqGet(param).GetByExpMestId(expMest.ID);

                    if (IsNotNullOrEmpty(matyReqs))
                    {
                        var existsDetails = new HisBcsMatyReqReqGet().GetByPreExpMestMatyReqIds(matyReqs.Select(s => s.ID).ToList());
                        if (IsNotNullOrEmpty(existsDetails))
                        {
                            string expMestCodes = String.Join(",", existsDetails.Select(s => s.TDL_XBTT_EXP_MEST_CODE).Distinct().ToList());
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_VatTuDaDuocBuCoSoOCacPhieuXuatSau, expMestCodes);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool CheckVerifyBaseAmount(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MEDICINE> expMestMedicines, List<HIS_EXP_MEST_MATERIAL> expMestMaterials)
        {
            bool valid = true;
            try
            {
                if (expMest.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DTT)
                {
                    return true;
                }

                V_HIS_MEDI_STOCK stock = HisMediStockCFG.DATA.FirstOrDefault(o => o.ID == expMest.MEDI_STOCK_ID);
                if (stock != null && stock.CABINET_MANAGE_OPTION == IMSys.DbConfig.HIS_RS.HIS_MEDI_STOCK.CABINET_MANAGE_OPTION__BASE)
                {
                    if (IsNotNullOrEmpty(expMestMedicines))
                    {
                        var Groups = expMestMedicines.GroupBy(g => g.TDL_MEDICINE_TYPE_ID ?? 0);
                        HisMediStockMetyFilterQuery metyFilter = new HisMediStockMetyFilterQuery();
                        metyFilter.MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                        metyFilter.MEDICINE_TYPE_IDs = Groups.Select(s => s.Key).ToList();
                        List<HIS_MEDI_STOCK_METY> stockMetys = new HisMediStockMetyGet().Get(metyFilter);

                        HisMedicineBeanFilterQuery beanFilter = new HisMedicineBeanFilterQuery();
                        beanFilter.MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                        beanFilter.MEDICINE_TYPE_IDs = Groups.Select(s => s.Key).ToList();
                        List<HIS_MEDICINE_BEAN> beanInStocks = new HisMedicineBeanGet().Get(beanFilter);

                        List<string> nameErrors = new List<string>();
                        foreach (var group in Groups)
                        {
                            List<HIS_EXP_MEST_MEDICINE> list = group.ToList();
                            decimal expAmount = list.Sum(s => s.AMOUNT);
                            HIS_MEDI_STOCK_METY metyStock = stockMetys != null ? stockMetys.FirstOrDefault(o => o.MEDICINE_TYPE_ID == group.Key) : null;
                            List<HIS_MEDICINE_BEAN> inStocks = beanInStocks != null ? beanInStocks.Where(o => o.TDL_MEDICINE_TYPE_ID == group.Key).ToList() : null;
                            decimal stockAmount = inStocks != null ? (inStocks.Sum(s => s.AMOUNT)) : 0;
                            decimal baseAmount = metyStock != null ? (metyStock.ALERT_MAX_IN_STOCK ?? 0) : 0;
                            if (expAmount > (baseAmount - stockAmount))
                            {
                                HIS_MEDICINE_TYPE medicineType = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == group.Key);
                                string name = medicineType != null ? medicineType.MEDICINE_TYPE_NAME : "";
                                nameErrors.Add(name);
                            }
                        }

                        if (IsNotNullOrEmpty(nameErrors))
                        {
                            string names = String.Join(",", nameErrors);
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_ThuocVuotCoSoKhiHuyThuocXuat, names);
                            return false;
                        }
                    }

                    if (IsNotNullOrEmpty(expMestMaterials))
                    {
                        var Groups = expMestMaterials.GroupBy(g => g.TDL_MATERIAL_TYPE_ID ?? 0);
                        HisMediStockMatyFilterQuery matyFilter = new HisMediStockMatyFilterQuery();
                        matyFilter.MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                        matyFilter.MATERIAL_TYPE_IDs = Groups.Select(s => s.Key).ToList();
                        List<HIS_MEDI_STOCK_MATY> stockMatys = new HisMediStockMatyGet().Get(matyFilter);

                        HisMaterialBeanFilterQuery beanFilter = new HisMaterialBeanFilterQuery();
                        beanFilter.MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                        beanFilter.MATERIAL_TYPE_IDs = Groups.Select(s => s.Key).ToList();
                        List<HIS_MATERIAL_BEAN> beanInStocks = new HisMaterialBeanGet().Get(beanFilter);

                        List<string> nameErrors = new List<string>();
                        foreach (var group in Groups)
                        {
                            List<HIS_EXP_MEST_MATERIAL> list = group.ToList();
                            decimal expAmount = list.Sum(s => s.AMOUNT);
                            HIS_MEDI_STOCK_MATY metyStock = stockMatys != null ? stockMatys.FirstOrDefault(o => o.MATERIAL_TYPE_ID == group.Key) : null;
                            List<HIS_MATERIAL_BEAN> inStocks = beanInStocks != null ? beanInStocks.Where(o => o.TDL_MATERIAL_TYPE_ID == group.Key).ToList() : null;
                            decimal stockAmount = inStocks != null ? (inStocks.Sum(s => s.AMOUNT)) : 0;
                            decimal baseAmount = metyStock != null ? (metyStock.ALERT_MAX_IN_STOCK ?? 0) : 0;
                            if (expAmount > (baseAmount - stockAmount))
                            {
                                HIS_MATERIAL_TYPE medicineType = HisMaterialTypeCFG.DATA.FirstOrDefault(o => o.ID == group.Key);
                                string name = medicineType != null ? medicineType.MATERIAL_TYPE_NAME : "";
                                nameErrors.Add(name);
                            }
                        }

                        if (IsNotNullOrEmpty(nameErrors))
                        {
                            string names = String.Join(",", nameErrors);
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_VatTuVuotCoSoKhiHuyThuocXuat, names);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        /// <summary>
        /// hủy thực xuất đơn phòng khám tủ trực
        /// </summary>
        /// <param name="materials"></param>
        /// <param name="expMest"></param>
        /// <returns></returns>
        internal bool IsValidCancellationOfExport(List<HIS_EXP_MEST_MATERIAL> materials, HIS_EXP_MEST expMest)
        {
            try
            {
                List<HIS_EXP_MEST_MATERIAL> materialProcess = IsNotNullOrEmpty(materials) ? materials.Where(o => (o.EXP_MEST_ID == expMest.ID)
                        && !String.IsNullOrWhiteSpace(o.SERIAL_NUMBER)).ToList() : null;

                if (IsNotNullOrEmpty(materialProcess))
                {
                    List<string> serialNumbers = materialProcess.Select(o => o.SERIAL_NUMBER).Distinct().ToList();
                    List<long> materialIds = materialProcess.Select(o => o.MATERIAL_ID.Value).Distinct().ToList();
                    HisImpMestMaterialView4FilterQuery filterImp = new HisImpMestMaterialView4FilterQuery();
                    filterImp.SERIAL_NUMBERs = serialNumbers;
                    filterImp.MATERIAL_IDs = materialIds;
                    List<V_HIS_IMP_MEST_MATERIAL_4> imp = new HisImpMestMaterialGet().GetView4(filterImp);

                    List<V_HIS_IMP_MEST_MATERIAL_4> impMest = IsNotNullOrEmpty(imp) ? imp.Where(o => (o.IMP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__IMPORT) || (o.IMP_TIME > expMest.LAST_EXP_TIME)).ToList() : null;

                    if (IsNotNullOrEmpty(impMest))
                    {
                        List<string> serials = impMest.Select(o => o.SERIAL_NUMBER).Distinct().ToList();
                        List<string> impMestCodes = impMest.Select(o => o.IMP_MEST_CODE).Distinct().ToList();

                        string serial = string.Join(",", serials);
                        string impMestCode = string.Join(",", impMestCodes);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_VatTuCoSoSerialDaDuocNhapTaiSuDung
, serial, impMestCode);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
            return true;
        }
    }
}
