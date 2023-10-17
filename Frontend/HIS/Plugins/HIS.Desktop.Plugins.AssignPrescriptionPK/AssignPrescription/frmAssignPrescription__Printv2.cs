using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using HIS.Desktop.ApplicationFont;
using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.Plugins.Library.PrintTreatmentFinish;
using HIS.Desktop.Print;
using HIS.UC.MenuPrint.ADO;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using HIS.Desktop.LocalStorage.LocalData;
using MOS.Filter;
using HIS.Desktop.ApiConsumer;
using Inventec.Common.Adapter;
using Inventec.Core;
using HIS.Desktop.Plugins.AssignPrescriptionPK.ADO;

namespace HIS.Desktop.Plugins.AssignPrescriptionPK.AssignPrescription
{
    public partial class frmAssignPrescription : HIS.Desktop.Utility.FormBase
    {
        List<HIS.UC.MenuPrint.ADO.MenuPrintADO> menuPrintADOs;
        private async Task InitMenuToButtonPrint()
        {
            try
            {
                Inventec.Common.Logging.LogSystem.Debug("InitMenuToButtonPrint .1");
                HIS.UC.MenuPrint.MenuPrintProcessor menuPrintProcessor = new HIS.UC.MenuPrint.MenuPrintProcessor();
                menuPrintADOs = new List<HIS.UC.MenuPrint.ADO.MenuPrintADO>();

                HIS.UC.MenuPrint.ADO.MenuPrintADO menuPrintADO__ThuocTongHop = new HIS.UC.MenuPrint.ADO.MenuPrintADO();
                menuPrintADO__ThuocTongHop.EventHandler = new EventHandler(OnClickPrintWithPrintTypeCfg);
                string savePrintMpsDefault = HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(HIS.Desktop.Plugins.AssignPrescriptionPK.Config.HisConfigCFG.SAVE_PRINT_MPS_DEFAULT);
                if (savePrintMpsDefault == "Mps000234")
                {
                    menuPrintADO__ThuocTongHop.PrintTypeCode = "Mps000234";
                    menuPrintADO__ThuocTongHop.Tag = "Mps000234";
                }
                else
                {
                    menuPrintADO__ThuocTongHop.PrintTypeCode = PrintTypeCodeStore.PRINT_TYPE_CODE__BIEUMAU__IN_DON_THUOC_TONG_HOP__MPS000118;
                    menuPrintADO__ThuocTongHop.Tag = PrintTypeCodeStore.PRINT_TYPE_CODE__BIEUMAU__IN_DON_THUOC_TONG_HOP__MPS000118;
                }

                menuPrintADOs.Add(menuPrintADO__ThuocTongHop);

                HIS.UC.MenuPrint.ADO.MenuPrintADO menuPrintADO__CacDonThuoc = new HIS.UC.MenuPrint.ADO.MenuPrintADO();
                menuPrintADO__CacDonThuoc.EventHandler = new EventHandler(OnClickPrintWithPrintTypeCfg);
                menuPrintADO__CacDonThuoc.PrintTypeCode = PrintTypeCodeStore.PRINT_TYPE_CODE__BIEUMAU__PHIEU_YEU_CAU_IN_DON_THUOC__MPS000044;
                menuPrintADO__CacDonThuoc.Tag = PrintTypeCodeStore.PRINT_TYPE_CODE__BIEUMAU__PHIEU_YEU_CAU_IN_DON_THUOC__MPS000044;
                menuPrintADOs.Add(menuPrintADO__CacDonThuoc);
                
                HIS.UC.MenuPrint.ADO.MenuPrintADO menuPrintADO__YHocCoTruyen = new HIS.UC.MenuPrint.ADO.MenuPrintADO();
                menuPrintADO__YHocCoTruyen.EventHandler = new EventHandler(OnClickPrintWithPrintTypeCfg);
                menuPrintADO__YHocCoTruyen.PrintTypeCode = PrintTypeCodeStore.PRINT_TYPE_CODE__BIEUMAU__PHIEU_YEU_CAU_IN_DON_THUOC_Y_HOC_CO_TRUYEN__MPS000050;
                menuPrintADO__YHocCoTruyen.Tag = PrintTypeCodeStore.PRINT_TYPE_CODE__BIEUMAU__PHIEU_YEU_CAU_IN_DON_THUOC_Y_HOC_CO_TRUYEN__MPS000050;
                menuPrintADOs.Add(menuPrintADO__YHocCoTruyen);

                var treatUC = ((this.treatmentFinishProcessor != null && this.ucTreatmentFinish != null) ? this.treatmentFinishProcessor.GetDataOutput(this.ucTreatmentFinish) : null);
                //Nếu người dùng tích chọn kết thúc điều trị > Chọn "Lưu" = Lưu toa thuốc + kết thúc điều trị (tự động in giấy hẹn khám, bảng kê nếu tích chọn) + Tự động close form kê toa + xử lý khám (có option theo user).
                if (treatUC != null && treatUC.IsAutoTreatmentFinish)
                {
                    if (treatUC.TreatmentEndTypeId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__CHUYEN)
                    {
                        HIS.UC.MenuPrint.ADO.MenuPrintADO inGiayChuyenVienItem = new HIS.UC.MenuPrint.ADO.MenuPrintADO();
                        inGiayChuyenVienItem.EventHandler = new EventHandler(OnClickPrintWithPrintTypeCfg);
                        inGiayChuyenVienItem.PrintTypeCode = PrintTypeCodeStore.PRINT_TYPE_CODE__IN_GIAY_CHUYEN_VIEN__MPS000011;
                        inGiayChuyenVienItem.Tag = PrintTypeCodeStore.PRINT_TYPE_CODE__IN_GIAY_CHUYEN_VIEN__MPS000011;

                        menuPrintADOs.Add(inGiayChuyenVienItem);

                        if ((this.currentTreatmentWithPatientType.TDL_TREATMENT_TYPE_ID ?? 0) != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM)
                        {
                            HIS.UC.MenuPrint.ADO.MenuPrintADO inGiayRaVienItem = new HIS.UC.MenuPrint.ADO.MenuPrintADO();
                            inGiayRaVienItem.Tag = PrintTypeCodeStore.PRINT_TYPE_CODE__IN_GIAY_RA_VIEN__MPS000008;
                            inGiayRaVienItem.EventHandler = new EventHandler(OnClickPrintWithPrintTypeCfg);
                            inGiayRaVienItem.PrintTypeCode = PrintTypeCodeStore.PRINT_TYPE_CODE__IN_GIAY_RA_VIEN__MPS000008;

                            menuPrintADOs.Add(inGiayRaVienItem);
                        }
                    }
                    else if (treatUC.TreatmentEndTypeId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__HEN)//Hen kham
                    {
                        HIS.UC.MenuPrint.ADO.MenuPrintADO inGiayHenKhamLaiItem = new HIS.UC.MenuPrint.ADO.MenuPrintADO();
                        inGiayHenKhamLaiItem.Tag = PrintTypeCodeStore.PRINT_TYPE_CODE__IN_GIAY_HEN_KHAM__MPS000010;
                        inGiayHenKhamLaiItem.EventHandler = new EventHandler(OnClickPrintWithPrintTypeCfg);
                        inGiayHenKhamLaiItem.PrintTypeCode = PrintTypeCodeStore.PRINT_TYPE_CODE__IN_GIAY_HEN_KHAM__MPS000010;

                        menuPrintADOs.Add(inGiayHenKhamLaiItem);
                    }
                    else if ((treatUC.TreatmentEndTypeId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__RAVIEN || treatUC.TreatmentEndTypeId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__XINRAVIEN)

                        && (this.currentTreatmentWithPatientType.TDL_TREATMENT_TYPE_ID ?? 0) != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM)//ra vien
                    {
                        HIS.UC.MenuPrint.ADO.MenuPrintADO inGiayRaVienItem = new HIS.UC.MenuPrint.ADO.MenuPrintADO();
                        inGiayRaVienItem.Tag = PrintTypeCodeStore.PRINT_TYPE_CODE__IN_GIAY_RA_VIEN__MPS000008;
                        inGiayRaVienItem.EventHandler = new EventHandler(OnClickPrintWithPrintTypeCfg);
                        inGiayRaVienItem.PrintTypeCode = PrintTypeCodeStore.PRINT_TYPE_CODE__IN_GIAY_RA_VIEN__MPS000008;

                        menuPrintADOs.Add(inGiayRaVienItem);
                    }
                }
                //Mps000478_Tóm tắt y lệnh phẫu thuật thủ thuật và đơn thuốc
                HIS.UC.MenuPrint.ADO.MenuPrintADO menuPrintADO__TomTatYLenhPTTTVaDonThuoc = new HIS.UC.MenuPrint.ADO.MenuPrintADO();
                menuPrintADO__TomTatYLenhPTTTVaDonThuoc.EventHandler = new EventHandler(OnClickPrintWithPrintTypeCfg);
                menuPrintADO__TomTatYLenhPTTTVaDonThuoc.PrintTypeCode = PrintTypeCodeStore.PRINT_TYPE_CODE__TOM_TAT_Y_LENH_PTTT_VA_DON_THUOC__MPS000478;
                menuPrintADO__TomTatYLenhPTTTVaDonThuoc.Tag = PrintTypeCodeStore.PRINT_TYPE_CODE__TOM_TAT_Y_LENH_PTTT_VA_DON_THUOC__MPS000478;
                menuPrintADOs.Add(menuPrintADO__TomTatYLenhPTTTVaDonThuoc);

                var printTypes = BackendDataWorker.Get<SAR.EFMODEL.DataModels.SAR_PRINT_TYPE>();
                HIS.UC.MenuPrint.ADO.MenuPrintInitADO menuPrintInitADO = new HIS.UC.MenuPrint.ADO.MenuPrintInitADO(menuPrintADOs, printTypes);
                //menuPrintInitADO.MinSizeHeight = this.lcibtnSave.MinSize.Height;
                //menuPrintInitADO.MaxSizeHeight = this.lcibtnSave.MaxSize.Height;

                int sizePlusByFontSize = 0, sizePlusByMinSize = 0;
                float fz = ApplicationFontWorker.GetFontSize();

                if (fz >= ApplicationFontConfig.FontSize1300)
                {
                    sizePlusByFontSize = 90;
                }
                else if (fz >= ApplicationFontConfig.FontSize1025)
                {
                    sizePlusByFontSize = 40;
                }
                else if (fz > ApplicationFontConfig.FontSize825)
                {
                    sizePlusByFontSize = 20;
                }

                foreach (var item in menuPrintADOs)
                {
                    var pt = printTypes.FirstOrDefault(o => o.PRINT_TYPE_CODE == item.Tag.ToString());
                    if (pt != null && pt.IS_NO_GROUP == GlobalVariables.CommonNumberTrue)
                    {
                        sizePlusByMinSize += 40;
                    }
                }

                if (this.lciPrintAssignPrescription.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                {
                    if (this.actionType != HIS.Desktop.LocalStorage.LocalData.GlobalVariables.ActionView)
                    {
                        lciPrintAssignPrescription.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
                        lciPrintAssignPrescription.MaxSize = new System.Drawing.Size(440 + sizePlusByFontSize, 40);
                        lciPrintAssignPrescription.MinSize = new System.Drawing.Size(140 + sizePlusByFontSize + sizePlusByMinSize, 40);
                    }
                    menuPrintInitADO.ControlContainer = this.layoutControlPrintAssignPrescription;
                }
                else
                {
                    lciPrintAssignPrescriptionExt.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
                    lciPrintAssignPrescriptionExt.MaxSize = new System.Drawing.Size(440 + sizePlusByFontSize, 40);
                    lciPrintAssignPrescriptionExt.MinSize = new System.Drawing.Size(140 + sizePlusByFontSize + sizePlusByMinSize, 40);

                    menuPrintInitADO.ControlContainer = this.layoutControlPrintAssignPrescriptionExt;
                }

                var menuResultADO = menuPrintProcessor.Run(menuPrintInitADO) as MenuPrintResultADO;
                if (menuResultADO == null)
                {
                    Inventec.Common.Logging.LogSystem.Warn("menuPrintProcessor run fail. " + LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => menuPrintInitADO), menuPrintInitADO));
                }
                Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData("lcibtnSave.Size", lcibtnSave.Size));
                Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData("lciPrintAssignPrescriptionExt.Size", lciPrintAssignPrescriptionExt.Size) + "____" + Inventec.Common.Logging.LogUtil.TraceData("lciPrintAssignPrescription.Size", lciPrintAssignPrescription.Size));
                Inventec.Common.Logging.LogSystem.Debug("InitMenuToButtonPrint .2");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        /// <summary>
        /// Hàm khi báo cho sự kiện nhấn nút in động
        /// Đặt đúng tên OnClickPrintWithPrintTypeCfg
        /// Trường hợp có nhiều nút in cần gen động trong 1 chức năng bổ sung sau, hiện tại chưa hỗ trợ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClickPrintWithPrintTypeCfg(object sender, EventArgs e)
        {
            LogTheadInSessionInfo(() => PrintWithPrintTypeCfg(sender, e), !GlobalStore.IsCabinet ? "PrintPresciption" : "PrintMedicalStore");
        }

        private void PrintWithPrintTypeCfg(object sender, EventArgs e) 
        {
            try
            {
                Inventec.Common.Logging.LogSystem.Debug("OnClickPrintWithPrintTypeCfg.1");
                //khi click nhiều lầu nút in sẽ chỉ hiển thị preview 1 lần
                bool isOpenPreview = false;
                foreach (Form item in Application.OpenForms)
                {
                    if (item.Name == "frmSetupPrintPreview")
                    {
                        isOpenPreview = true;
                        break;
                    }
                }
                if (isOpenPreview)
                {
                    return;
                }

                string printTypeCode = "";
                if (sender is DXMenuItem)
                {
                    var bbtnItem = sender as DXMenuItem;
                    printTypeCode = (bbtnItem.Tag ?? "").ToString();
                }
                else if (sender is SimpleButton)
                {
                    var bbtnItem = sender as SimpleButton;
                    printTypeCode = (bbtnItem.Tag ?? "").ToString();
                }

                PrescriptionSavePrintShowHasClickSave(printTypeCode, false);

                //PrescriptionPrintShowPrintOnly(printTypeCode, false);
                Inventec.Common.Logging.LogSystem.Debug("OnClickPrintWithPrintTypeCfg.2____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => printTypeCode), printTypeCode));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void PrescriptionPrintShowPrintOnly(string printTypeCode, bool isPrintNow, MPS.ProcessorBase.PrintConfig.PreviewType? previewType = null)
        {
            try
            {
                var IsNotShow = lstConfig.Exists(o => o.IsChecked && o.ID == (int)ConfigADO.RowConfigID.KhongHienThiDonKhongLayODonThuocTH);
                if (printTypeCode == PrintTypeCodeStore.PRINT_TYPE_CODE__IN_GIAY_RA_VIEN__MPS000008
                    || printTypeCode == PrintTypeCodeStore.PRINT_TYPE_CODE__IN_GIAY_HEN_KHAM__MPS000010
                    || printTypeCode == PrintTypeCodeStore.PRINT_TYPE_CODE__IN_GIAY_CHUYEN_VIEN__MPS000011)
                {
                    HIS_TREATMENT treatment = new HIS_TREATMENT();
                    Inventec.Common.Mapper.DataObjectMapper.Map<HIS_TREATMENT>(treatment, currentTreatmentWithPatientType);

                    PrintTreatmentFinishProcessor printTreatmentFinishProcessor = new PrintTreatmentFinishProcessor(treatment, currentModule != null ? currentModule.RoomId : 0);
                    if (printTypeCode == PrintTypeCodeStore.PRINT_TYPE_CODE__IN_GIAY_HEN_KHAM__MPS000010)
                    {
                        printTreatmentFinishProcessor.Print(MPS.Processor.Mps000010.PDO.Mps000010PDO.printTypeCode, isPrintNow);
                    }
                    else if (printTypeCode == PrintTypeCodeStore.PRINT_TYPE_CODE__IN_GIAY_CHUYEN_VIEN__MPS000011)
                    {
                        printTreatmentFinishProcessor.Print(MPS.Processor.Mps000011.PDO.Mps000011PDO.printTypeCode, isPrintNow);
                    }
                    else if ((currentTreatmentWithPatientType.TDL_TREATMENT_TYPE_ID ?? 0) != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM)
                    {
                        printTreatmentFinishProcessor.Print(MPS.Processor.Mps000008.PDO.Mps000008PDO.printTypeCode, isPrintNow);
                    }
                }
                else
                {
                    HIS.Desktop.Plugins.Library.PrintPrescription.PrintPrescriptionProcessor printPrescriptionProcessor;
                    if (GlobalStore.IsTreatmentIn && !GlobalStore.IsCabinet)
                    {
                        List<InPatientPresResultSDO> InPatientPresResultSDOForPrints = new List<InPatientPresResultSDO>();
                        InPatientPresResultSDO inPatientPresResultSDO = new InPatientPresResultSDO();
                        if (inPrescriptionResultSDOs != null)
                            InPatientPresResultSDOForPrints.Add(inPrescriptionResultSDOs);
                        else if (this.oldServiceReq != null)
                        {
                            if (this.oldExpMest != null)
                            {
                                inPatientPresResultSDO.ExpMests = new List<HIS_EXP_MEST>();
                                inPatientPresResultSDO.ExpMests.Add(oldExpMest);
                            }

                            if (this.oldServiceReq != null)
                            {
                                inPatientPresResultSDO.ServiceReqs = new List<HIS_SERVICE_REQ>();
                                inPatientPresResultSDO.ServiceReqs.Add(this.oldServiceReq);
                            }

                            if (this.expMestMedicineEditPrints != null)
                            {
                                AutoMapper.Mapper.CreateMap<V_HIS_EXP_MEST_MEDICINE, HIS_EXP_MEST_MEDICINE>();
                                inPatientPresResultSDO.Medicines = AutoMapper.Mapper.Map<List<HIS_EXP_MEST_MEDICINE>>(this.expMestMedicineEditPrints);
                            }

                            if (this.expMestMaterialEditPrints != null)
                            {
                                AutoMapper.Mapper.CreateMap<V_HIS_EXP_MEST_MATERIAL, HIS_EXP_MEST_MATERIAL>();
                                inPatientPresResultSDO.Materials = AutoMapper.Mapper.Map<List<HIS_EXP_MEST_MATERIAL>>(this.expMestMaterialEditPrints);
                            }
                            if (this.serviceReqMetys != null && this.serviceReqMetys.Count > 0)
                            {
                                inPatientPresResultSDO.ServiceReqMeties = serviceReqMetys;
                            }
                            if (this.serviceReqMatys != null && this.serviceReqMatys.Count > 0)
                            {
                                inPatientPresResultSDO.ServiceReqMaties = serviceReqMatys;
                            }

                            InPatientPresResultSDOForPrints.Add(inPatientPresResultSDO);
                        }

                        printPrescriptionProcessor = new Library.PrintPrescription.PrintPrescriptionProcessor(InPatientPresResultSDOForPrints, IsNotShow, this.currentModule, true);
                        printPrescriptionProcessor.SetOutHospital((currentMediStockNhaThuocSelecteds != null && currentMediStockNhaThuocSelecteds.Count > 0));
                    }
                    else
                    {
                        List<OutPatientPresResultSDO> OutPatientPresResultSDOForPrints = new List<OutPatientPresResultSDO>();
                        OutPatientPresResultSDO OutPatientPresResultSDO = new OutPatientPresResultSDO();

                        if (outPrescriptionResultSDOs != null)
                            OutPatientPresResultSDOForPrints.Add(outPrescriptionResultSDOs);
                        else if (this.oldServiceReq != null)
                        {
                            if (this.oldExpMest != null)
                            {
                                OutPatientPresResultSDO.ExpMests = new List<HIS_EXP_MEST>();
                                OutPatientPresResultSDO.ExpMests.Add(oldExpMest);
                            }

                            if (this.oldServiceReq != null)
                            {
                                OutPatientPresResultSDO.ServiceReqs = new List<HIS_SERVICE_REQ>();
                                OutPatientPresResultSDO.ServiceReqs.Add(this.oldServiceReq);
                            }

                            if (this.expMestMedicineEditPrints != null)
                            {
                                AutoMapper.Mapper.CreateMap<V_HIS_EXP_MEST_MEDICINE, HIS_EXP_MEST_MEDICINE>();
                                OutPatientPresResultSDO.Medicines = AutoMapper.Mapper.Map<List<HIS_EXP_MEST_MEDICINE>>(this.expMestMedicineEditPrints);
                            }

                            if (this.expMestMaterialEditPrints != null)
                            {
                                AutoMapper.Mapper.CreateMap<V_HIS_EXP_MEST_MATERIAL, HIS_EXP_MEST_MATERIAL>();
                                OutPatientPresResultSDO.Materials = AutoMapper.Mapper.Map<List<HIS_EXP_MEST_MATERIAL>>(this.expMestMaterialEditPrints);
                            }
                            if (this.serviceReqMetys != null && this.serviceReqMetys.Count > 0)
                            {
                                OutPatientPresResultSDO.ServiceReqMeties = serviceReqMetys;
                            }
                            if (this.serviceReqMatys != null && this.serviceReqMatys.Count > 0)
                            {
                                OutPatientPresResultSDO.ServiceReqMaties = serviceReqMatys;
                            }

                            OutPatientPresResultSDOForPrints.Add(OutPatientPresResultSDO);
                        }

                        printPrescriptionProcessor = new Library.PrintPrescription.PrintPrescriptionProcessor(OutPatientPresResultSDOForPrints, IsNotShow, this.currentModule, true);
                        printPrescriptionProcessor.SetOutHospital((currentMediStockNhaThuocSelecteds != null && currentMediStockNhaThuocSelecteds.Count > 0));
                    }
                    if (chkPreviewBeforePrint.Checked)
                    {
                        isPrintNow = false;
                    }

                    if (isPrintNow)
                        printPrescriptionProcessor.Print(previewType);
                    else
                        printPrescriptionProcessor.Print(printTypeCode, isPrintNow, previewType);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void PrescriptionSavePrintShowHasClickSave(string printTypeCode, bool isPrintNow, MPS.ProcessorBase.PrintConfig.PreviewType? previewType = null)
        {
            try
            {
                var IsNotShow = lstConfig.Exists(o => o.IsChecked && o.ID == (int)ConfigADO.RowConfigID.KhongHienThiDonKhongLayODonThuocTH);
                Inventec.Common.Logging.LogSystem.Debug("PrescriptionSavePrintShowHasClickSave.1____"
                    + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => printTypeCode), printTypeCode)
                    + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => isPrintNow), isPrintNow)
                    + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => previewType), previewType)
                    + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => IsNotShow), IsNotShow));
                if (printTypeCode == PrintTypeCodeStore.PRINT_TYPE_CODE__IN_GIAY_RA_VIEN__MPS000008
                    || printTypeCode == PrintTypeCodeStore.PRINT_TYPE_CODE__IN_GIAY_HEN_KHAM__MPS000010
                    || printTypeCode == PrintTypeCodeStore.PRINT_TYPE_CODE__IN_GIAY_CHUYEN_VIEN__MPS000011
                    || printTypeCode == PrintTypeCodeStore.PRINT_TYPE_CODE__TOM_TAT_Y_LENH_PTTT_VA_DON_THUOC__MPS000478)
                {
                    HIS_TREATMENT treatment = new HIS_TREATMENT();
                    Inventec.Common.Mapper.DataObjectMapper.Map<HIS_TREATMENT>(treatment, currentTreatmentWithPatientType);

                    PrintTreatmentFinishProcessor printTreatmentFinishProcessor = new PrintTreatmentFinishProcessor(treatment, currentModule != null ? currentModule.RoomId : 0);
                    if (printTypeCode == PrintTypeCodeStore.PRINT_TYPE_CODE__TOM_TAT_Y_LENH_PTTT_VA_DON_THUOC__MPS000478)
                    {
                        printTreatmentFinishProcessor.Print(MPS.Processor.Mps000478.PDO.Mps000478PDO.printTypeCode, isPrintNow);
                    }
                    else if (printTypeCode == PrintTypeCodeStore.PRINT_TYPE_CODE__IN_GIAY_HEN_KHAM__MPS000010)
                    {
                        printTreatmentFinishProcessor.Print(MPS.Processor.Mps000010.PDO.Mps000010PDO.printTypeCode, isPrintNow);
                    }
                    else if (printTypeCode == PrintTypeCodeStore.PRINT_TYPE_CODE__IN_GIAY_CHUYEN_VIEN__MPS000011)
                    {
                        printTreatmentFinishProcessor.Print(MPS.Processor.Mps000011.PDO.Mps000011PDO.printTypeCode, isPrintNow);
                    }
                    else if ((currentTreatmentWithPatientType.TDL_TREATMENT_TYPE_ID ?? 0) != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM)
                    {
                        printTreatmentFinishProcessor.Print(MPS.Processor.Mps000008.PDO.Mps000008PDO.printTypeCode, isPrintNow);
                    }
                }
                else
                {
                    HIS.Desktop.Plugins.Library.PrintPrescription.PrintPrescriptionProcessor printPrescriptionProcessor;
                    if (GlobalStore.IsTreatmentIn && !GlobalStore.IsCabinet)
                    {
                        //List<InPatientPresResultSDO> InPatientPresResultSDOForPrints = new List<InPatientPresResultSDO>();
                        //InPatientPresResultSDOForPrints.Add(inPrescriptionResultSDOs);
                        //printPrescriptionProcessor = new Library.PrintPrescription.PrintPrescriptionProcessor(InPatientPresResultSDOForPrints, this.currentModule);
                        List<InPatientPresResultSDO> InPatientPresResultSDOForPrints = new List<InPatientPresResultSDO>();
                        InPatientPresResultSDO inPatientPresResultSDO = new InPatientPresResultSDO();
                        if (inPrescriptionResultSDOs != null)
                            InPatientPresResultSDOForPrints.Add(inPrescriptionResultSDOs);
                        else if (this.oldServiceReq != null)
                        {
                            if (this.oldExpMest != null && this.oldExpMest.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DTT)
                            {
                                inPatientPresResultSDO.ExpMests = new List<HIS_EXP_MEST>();
                                inPatientPresResultSDO.ExpMests.Add(oldExpMest);
                            }

                            if (this.oldServiceReq != null)
                            {
                                inPatientPresResultSDO.ServiceReqs = new List<HIS_SERVICE_REQ>();
                                inPatientPresResultSDO.ServiceReqs.Add(this.oldServiceReq);
                            }

                            if (this.expMestMedicineEditPrints != null)
                            {
                                AutoMapper.Mapper.CreateMap<V_HIS_EXP_MEST_MEDICINE, HIS_EXP_MEST_MEDICINE>();
                                inPatientPresResultSDO.Medicines = AutoMapper.Mapper.Map<List<HIS_EXP_MEST_MEDICINE>>(this.expMestMedicineEditPrints);
                            }

                            if (this.expMestMaterialEditPrints != null)
                            {
                                AutoMapper.Mapper.CreateMap<V_HIS_EXP_MEST_MATERIAL, HIS_EXP_MEST_MATERIAL>();
                                inPatientPresResultSDO.Materials = AutoMapper.Mapper.Map<List<HIS_EXP_MEST_MATERIAL>>(this.expMestMaterialEditPrints);
                            }
                            if (this.serviceReqMetys != null && this.serviceReqMetys.Count > 0)
                            {
                                inPatientPresResultSDO.ServiceReqMeties = serviceReqMetys;
                            }
                            if (this.serviceReqMatys != null && this.serviceReqMatys.Count > 0)
                            {
                                inPatientPresResultSDO.ServiceReqMaties = serviceReqMatys;
                            }

                            InPatientPresResultSDOForPrints.Add(inPatientPresResultSDO);
                        }

                        Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => InPatientPresResultSDOForPrints), InPatientPresResultSDOForPrints));

                        printPrescriptionProcessor = new Library.PrintPrescription.PrintPrescriptionProcessor(InPatientPresResultSDOForPrints, IsNotShow, this.currentModule, true);
                        printPrescriptionProcessor.SetOutHospital((currentMediStockNhaThuocSelecteds != null && currentMediStockNhaThuocSelecteds.Count > 0));
                    }
                    else
                    {
                        List<OutPatientPresResultSDO> OutPatientPresResultSDOForPrints = new List<OutPatientPresResultSDO>();
                        OutPatientPresResultSDO OutPatientPresResultSDO = new OutPatientPresResultSDO();
                        string savePrintMpsDefault = HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(HIS.Desktop.Plugins.AssignPrescriptionPK.Config.HisConfigCFG.SAVE_PRINT_MPS_DEFAULT);

                        Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => savePrintMpsDefault), savePrintMpsDefault)
                            + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => printTypeCode), printTypeCode)
                            + "((this.expMestPrints != null && this.expMestPrints.Count > 0) || (this.serviceReqPrints != null && this.serviceReqPrints.Count > 0)) =" + ((this.expMestPrints != null && this.expMestPrints.Count > 0) || (this.serviceReqPrints != null && this.serviceReqPrints.Count > 0)));

                        if ((String.IsNullOrEmpty(printTypeCode) || printTypeCode == "Mps000234")
                            //&& ((this.expMestPrints != null && this.expMestPrints.Count > 0) || (this.serviceReqPrints != null && this.serviceReqPrints.Count > 0))
                            && !GlobalStore.IsTreatmentIn && !GlobalStore.IsCabinet)
                        {
                            List<HIS_EXP_MEST> expMestPrintPlus = new List<HIS_EXP_MEST>();
                            List<HIS_SERVICE_REQ> serviceReqPrintPlus = new List<HIS_SERVICE_REQ>();
                            List<HIS_EXP_MEST_MEDICINE> expMestMedicinePrintPlus = new List<HIS_EXP_MEST_MEDICINE>();
                            List<HIS_EXP_MEST_MATERIAL> expMestMaterialPrintPlus = new List<HIS_EXP_MEST_MATERIAL>();

                            if (this.expMestPrints != null && this.expMestPrints.Count > 0)
                            {
                                if (this.outPrescriptionResultSDOs != null && this.outPrescriptionResultSDOs.ExpMests != null && this.outPrescriptionResultSDOs.ExpMests.Count > 0)
                                {
                                    expMestPrintPlus = expMestPrints.Where(o => !this.outPrescriptionResultSDOs.ExpMests.Exists(k => k.ID == o.ID)).ToList();
                                    expMestPrintPlus.AddRange(this.outPrescriptionResultSDOs.ExpMests);

                                    if (this.expMestMedicinePrints != null && this.expMestMedicinePrints.Count > 0)
                                    {
                                        expMestMedicinePrintPlus = this.expMestMedicinePrints.Where(o => !this.outPrescriptionResultSDOs.ExpMests.Exists(k => k.ID == o.EXP_MEST_ID)).ToList();
                                    }

                                    if (this.expMestMaterialPrints != null && this.expMestMaterialPrints.Count > 0)
                                    {
                                        expMestMaterialPrintPlus = this.expMestMaterialPrints.Where(o => !this.outPrescriptionResultSDOs.ExpMests.Exists(k => k.ID == o.EXP_MEST_ID)).ToList();
                                    }
                                }
                                else
                                {
                                    expMestPrintPlus.AddRange(expMestPrints);
                                    if (this.expMestMedicinePrints != null && this.expMestMedicinePrints.Count > 0)
                                    {
                                        expMestMedicinePrintPlus.AddRange(this.expMestMedicinePrints);
                                    }

                                    if (this.expMestMaterialPrints != null && this.expMestMaterialPrints.Count > 0)
                                    {
                                        expMestMaterialPrintPlus.AddRange(this.expMestMaterialPrints);
                                    }
                                }
                            }
                            else if (this.outPrescriptionResultSDOs != null && this.outPrescriptionResultSDOs.ExpMests != null && this.outPrescriptionResultSDOs.ExpMests.Count > 0)
                            {
                                expMestPrintPlus.AddRange(this.outPrescriptionResultSDOs.ExpMests);
                            }

                            if (this.outPrescriptionResultSDOs.Medicines != null && this.outPrescriptionResultSDOs.Medicines.Count > 0)
                            {
                                expMestMedicinePrintPlus.AddRange(this.outPrescriptionResultSDOs.Medicines);
                            }
                            if (this.outPrescriptionResultSDOs.Materials != null && this.outPrescriptionResultSDOs.Materials.Count > 0)
                            {
                                expMestMaterialPrintPlus.AddRange(this.outPrescriptionResultSDOs.Materials);
                            }

                            if (this.serviceReqPrints != null && this.serviceReqPrints.Count > 0)
                            {
                                if (this.outPrescriptionResultSDOs != null && this.outPrescriptionResultSDOs.ServiceReqs != null && this.outPrescriptionResultSDOs.ServiceReqs.Count > 0)
                                {
                                    serviceReqPrintPlus = this.serviceReqPrints.Where(o => !this.outPrescriptionResultSDOs.ServiceReqs.Exists(k => k.ID == o.ID)).ToList();
                                    serviceReqPrintPlus.AddRange(this.outPrescriptionResultSDOs.ServiceReqs);
                                }
                                else
                                {
                                    serviceReqPrintPlus.AddRange(this.serviceReqPrints);
                                }
                            }
                            else if (this.outPrescriptionResultSDOs != null && this.outPrescriptionResultSDOs.ServiceReqs != null && this.outPrescriptionResultSDOs.ServiceReqs.Count > 0)
                            {
                                serviceReqPrintPlus.AddRange(this.outPrescriptionResultSDOs.ServiceReqs);
                            }

                            if (this.outPrescriptionResultSDOs != null && this.outPrescriptionResultSDOs.ServiceReqMaties != null && this.outPrescriptionResultSDOs.ServiceReqMaties.Count > 0)
                            {
                                OutPatientPresResultSDO.ServiceReqMaties = this.outPrescriptionResultSDOs.ServiceReqMaties;
                            }
                            if (this.outPrescriptionResultSDOs != null && this.outPrescriptionResultSDOs.ServiceReqMeties != null && this.outPrescriptionResultSDOs.ServiceReqMeties.Count > 0)
                            {
                                OutPatientPresResultSDO.ServiceReqMeties = this.outPrescriptionResultSDOs.ServiceReqMeties;
                            }

                            if (expMestPrintPlus != null && expMestPrintPlus.Count > 0)
                            {
                                expMestPrintPlus = expMestPrintPlus.Where(o => o.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DTT).ToList();
                            }

                            OutPatientPresResultSDO.ExpMests = expMestPrintPlus;
                            OutPatientPresResultSDO.Medicines = expMestMedicinePrintPlus;
                            OutPatientPresResultSDO.Materials = expMestMaterialPrintPlus;
                            OutPatientPresResultSDO.ServiceReqs = serviceReqPrintPlus;
                            OutPatientPresResultSDOForPrints.Add(OutPatientPresResultSDO);
                        }
                        else
                        {
                            if (outPrescriptionResultSDOs != null)
                                OutPatientPresResultSDOForPrints.Add(outPrescriptionResultSDOs);
                            else if (this.oldServiceReq != null)
                            {
                                if (this.oldExpMest != null)
                                {
                                    OutPatientPresResultSDO.ExpMests = new List<HIS_EXP_MEST>();
                                    OutPatientPresResultSDO.ExpMests.Add(oldExpMest);
                                }

                                if (this.oldServiceReq != null)
                                {
                                    OutPatientPresResultSDO.ServiceReqs = new List<HIS_SERVICE_REQ>();
                                    OutPatientPresResultSDO.ServiceReqs.Add(this.oldServiceReq);
                                }

                                if (this.expMestMedicineEditPrints != null)
                                {
                                    AutoMapper.Mapper.CreateMap<V_HIS_EXP_MEST_MEDICINE, HIS_EXP_MEST_MEDICINE>();
                                    OutPatientPresResultSDO.Medicines = AutoMapper.Mapper.Map<List<HIS_EXP_MEST_MEDICINE>>(this.expMestMedicineEditPrints);
                                }

                                if (this.expMestMaterialEditPrints != null)
                                {
                                    AutoMapper.Mapper.CreateMap<V_HIS_EXP_MEST_MATERIAL, HIS_EXP_MEST_MATERIAL>();
                                    OutPatientPresResultSDO.Materials = AutoMapper.Mapper.Map<List<HIS_EXP_MEST_MATERIAL>>(this.expMestMaterialEditPrints);
                                }
                                if (this.serviceReqMetys != null && this.serviceReqMetys.Count > 0)
                                {
                                    OutPatientPresResultSDO.ServiceReqMeties = serviceReqMetys;
                                }
                                if (this.serviceReqMatys != null && this.serviceReqMatys.Count > 0)
                                {
                                    OutPatientPresResultSDO.ServiceReqMaties = serviceReqMatys;
                                }

                                OutPatientPresResultSDOForPrints.Add(OutPatientPresResultSDO);
                            }
                        }

                        Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => OutPatientPresResultSDOForPrints), OutPatientPresResultSDOForPrints));

                        printPrescriptionProcessor = new Library.PrintPrescription.PrintPrescriptionProcessor(OutPatientPresResultSDOForPrints, IsNotShow, this.currentModule, true);
                        printPrescriptionProcessor.SetOutHospital((currentMediStockNhaThuocSelecteds != null && currentMediStockNhaThuocSelecteds.Count > 0));
                    }
                    if (isPrintNow)
                        printPrescriptionProcessor.Print(previewType);
                    else
                        printPrescriptionProcessor.Print(printTypeCode, isPrintNow, previewType);
                }
                Inventec.Common.Logging.LogSystem.Debug("PrescriptionSavePrintShowHasClickSave.2");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

    }
}
