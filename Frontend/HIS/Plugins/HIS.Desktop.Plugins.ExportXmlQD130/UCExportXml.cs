using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MOS.EFMODEL.DataModels;
using HIS.Desktop.LocalStorage.ConfigApplication;
using Inventec.Core;
using MOS.Filter;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.LocalData;
using HIS.UC.SereServTree;
using Inventec.Desktop.Common.Message;
using HIS.Desktop.Controls.Session;
using His.Bhyt.ExportXml.XML130;
using HIS.Desktop.Plugins.ExportXmlQD130.Base;
using HIS.Desktop.Utility;
using System.Collections;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.IO;
using HIS.Desktop.Utilities.Extensions;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using Inventec.Common.Adapter;
using Inventec.Common.Logging;
using HIS.Desktop.LibraryMessage;
using MOS.SDO;
using HIS.Desktop.Plugins.ExportXmlQD130.ADO;
using Inventec.Common.Controls.EditorLoader;
using DevExpress.Utils.Menu;
using DevExpress.Utils;
using Inventec.Fss.Client;
using Newtonsoft.Json;
using DevExpress.XtraBars;
using System.Resources;
using Inventec.Desktop.Common.LanguageManager;

namespace HIS.Desktop.Plugins.ExportXmlQD130
{
    public partial class UCExportXml : HIS.Desktop.Utility.UserControlBase
    {
        Inventec.Desktop.Common.Modules.Module currentModule = null;

        bool timerTickIsRunning = false;

        SereServTreeProcessor ssTreeProcessor;
        UserControl ucSereServTree;

        List<V_HIS_TREATMENT_1> listTreatment1 = new List<V_HIS_TREATMENT_1>();
        List<V_HIS_TREATMENT_1> listSelection = new List<V_HIS_TREATMENT_1>();

        List<V_HIS_TREATMENT_12> HisTreatments = new List<V_HIS_TREATMENT_12>();
        List<V_HIS_PATIENT_TYPE_ALTER> ListPatientTypeAlter = new List<V_HIS_PATIENT_TYPE_ALTER>();
        List<V_HIS_SERE_SERV_2> ListSereServ = new List<V_HIS_SERE_SERV_2>();
        List<V_HIS_BABY> ListBaby = new List<V_HIS_BABY>();
        List<V_HIS_MEDICAL_ASSESSMENT> ListMedicalAssessment = new List<V_HIS_MEDICAL_ASSESSMENT>();
        List<HIS_HIV_TREATMENT> ListHivTreatment = new List<HIS_HIV_TREATMENT>();

        List<V_HIS_SERE_SERV_TEIN> HisSereServTeins = new List<V_HIS_SERE_SERV_TEIN>();
        List<V_HIS_SERE_SERV_PTTT> HisSereServPttts = new List<V_HIS_SERE_SERV_PTTT>();
        List<HIS_DHST> ListDhst = new List<HIS_DHST>();
        List<HIS_TRACKING> HisTrackings = new List<HIS_TRACKING>();
        List<HIS_EKIP_USER> ListEkipUser = new List<HIS_EKIP_USER>();
        List<V_HIS_BED_LOG> ListBedlog = new List<V_HIS_BED_LOG>();
        List<HIS_DEBATE> ListDebates = new List<HIS_DEBATE>();
        List<TreatmentImportADO> listTreatmentImport;

        internal string filterType__IN = "Trong DS đầu thẻ BHYT sau:";
        internal string filterType__OUT = "Ngoài DS đầu thẻ BHYT sau:";
        internal string filterType__FeeLockTime = "Thời gian khóa viện phí từ:";
        internal string filterType__EndTreatmentTime = "Thời gian kết thúc điều trị từ:";
        internal string filterType__BeginTreatmentTime = "Thời gian vào viện từ:";
        int rowCount = 0;
        int dataTotal = 0;
        int start = 0;
        int limit = 0;

        List<HIS_BRANCH> branchSelecteds;
        List<HIS_PATIENT_TYPE> patientTypeSelecteds;
        List<HIS_TREATMENT_TYPE> treatmentTypeSelecteds;
        string SavePath;
        bool isNotLoadWhileChangeControlStateInFirst;
        HIS.Desktop.Library.CacheClient.ControlStateWorker controlStateWorker;
        List<HIS.Desktop.Library.CacheClient.ControlStateRDO> currentControlStateRDO;
        string moduleLink = "HIS.Desktop.Plugins.ExportXmlQD130";
        V_HIS_TREATMENT_1 currentTreatment;
        List<HIS_CONFIG> NewConfig;
        string saveFilePath;
        ConfigSyncADO configSync;
        List<V_HIS_TREATMENT_1> listTreatmentSync;
        List<string> listMessageError;
        CommonParam paramUpdateXml130;
        bool callSyncSuccess;
        bool isAutoSync = false;

        public UCExportXml(Inventec.Desktop.Common.Modules.Module moduleData)
            : base(moduleData)
        {
            InitializeComponent();
            try
            {
                this.currentModule = moduleData;
                HisConfigCFG.LoadConfig();
                this.InitSereServTree();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void InitSereServTree()
        {
            try
            {
                System.Resources.ResourceManager languageMessage = new System.Resources.ResourceManager("HIS.Desktop.Plugins.ExportXmlQD130.Resources.Lang", System.Reflection.Assembly.GetExecutingAssembly());

                ssTreeProcessor = new UC.SereServTree.SereServTreeProcessor();
                SereServTreeADO ado = new SereServTreeADO();
                ado.IsShowSearchPanel = false;
                ado.IsCreateParentNodeWithSereServExpend = false;
                ado.SereServTree_CustomDrawNodeCell = treeSereServ_CustomDrawNodeCell;
                ado.SereServTreeColumns = new List<SereServTreeColumn>();
                SereServTreeColumn serviceNameCol = new SereServTreeColumn("Tên dịch vụ", "TDL_SERVICE_NAME", 150, false);
                serviceNameCol.VisibleIndex = 0;
                ado.SereServTreeColumns.Add(serviceNameCol);

                SereServTreeColumn amountCol = new SereServTreeColumn("SL", "AMOUNT_PLUS", 40, false);
                amountCol.VisibleIndex = 1;
                amountCol.Format = new DevExpress.Utils.FormatInfo();
                amountCol.Format.FormatString = "#,##0.00";
                amountCol.Format.FormatType = DevExpress.Utils.FormatType.Custom;
                ado.SereServTreeColumns.Add(amountCol);

                SereServTreeColumn virPriceCol = new SereServTreeColumn("Đơn giá", "VIR_PRICE", 80, false);
                virPriceCol.VisibleIndex = 2;
                virPriceCol.Format = new DevExpress.Utils.FormatInfo();
                virPriceCol.Format.FormatString = "#,##0.0000";
                virPriceCol.Format.FormatType = DevExpress.Utils.FormatType.Custom;
                ado.SereServTreeColumns.Add(virPriceCol);

                SereServTreeColumn virTotalPriceCol = new SereServTreeColumn("Thành tiền", "VIR_TOTAL_PRICE", 90, false);
                virTotalPriceCol.VisibleIndex = 3;
                virTotalPriceCol.Format = new DevExpress.Utils.FormatInfo();
                virTotalPriceCol.Format.FormatString = "#,##0.0000";
                virTotalPriceCol.Format.FormatType = DevExpress.Utils.FormatType.Custom;
                ado.SereServTreeColumns.Add(virTotalPriceCol);

                SereServTreeColumn virTotalHeinPriceCol = new SereServTreeColumn("Đồng chi trả", "VIR_TOTAL_HEIN_PRICE", 90, false);
                virTotalHeinPriceCol.VisibleIndex = 4;
                virTotalHeinPriceCol.Format = new DevExpress.Utils.FormatInfo();
                virTotalHeinPriceCol.Format.FormatString = "#,##0.0000";
                virTotalHeinPriceCol.Format.FormatType = DevExpress.Utils.FormatType.Custom;
                ado.SereServTreeColumns.Add(virTotalHeinPriceCol);

                SereServTreeColumn virTotalPatientPriceCol = new SereServTreeColumn("Bệnh nhân trả", "VIR_TOTAL_PATIENT_PRICE", 110, false);
                virTotalPatientPriceCol.VisibleIndex = 5;
                virTotalPatientPriceCol.Format = new DevExpress.Utils.FormatInfo();
                virTotalPatientPriceCol.Format.FormatString = "#,##0.0000";
                virTotalPatientPriceCol.Format.FormatType = DevExpress.Utils.FormatType.Custom;
                ado.SereServTreeColumns.Add(virTotalPatientPriceCol);

                SereServTreeColumn virDiscountCol = new SereServTreeColumn("Chiết khấu", "DISCOUNT", 90, false);
                virDiscountCol.VisibleIndex = 6;
                virDiscountCol.Format = new DevExpress.Utils.FormatInfo();
                virDiscountCol.Format.FormatString = "#,##0.0000";
                virDiscountCol.Format.FormatType = DevExpress.Utils.FormatType.Custom;
                ado.SereServTreeColumns.Add(virDiscountCol);

                SereServTreeColumn virIsExpendCol = new SereServTreeColumn("Hao phí", "IsExpend", 60, false);
                virIsExpendCol.VisibleIndex = 7;
                ado.SereServTreeColumns.Add(virIsExpendCol);

                SereServTreeColumn virVatRatioCol = new SereServTreeColumn("VAT", "VAT", 100, false);
                virVatRatioCol.VisibleIndex = 8;
                virVatRatioCol.Format = new DevExpress.Utils.FormatInfo();
                virVatRatioCol.Format.FormatString = "#,##0.00";
                virVatRatioCol.Format.FormatType = DevExpress.Utils.FormatType.Custom;
                ado.SereServTreeColumns.Add(virVatRatioCol);

                SereServTreeColumn serviceCodeCol = new SereServTreeColumn("Mã dịch vụ", "TDL_SERVICE_CODE", 100, false);
                serviceCodeCol.VisibleIndex = 9;
                ado.SereServTreeColumns.Add(serviceCodeCol);

                SereServTreeColumn serviceReqCodeCol = new SereServTreeColumn("Mã yêu cầu", "TDL_SERVICE_REQ_CODE", 100, false);
                serviceReqCodeCol.VisibleIndex = 10;
                ado.SereServTreeColumns.Add(serviceReqCodeCol);

                this.ucSereServTree = (UserControl)ssTreeProcessor.Run(ado);
                if (this.ucSereServTree != null)
                {
                    this.panelControlSereServTree.Controls.Add(this.ucSereServTree);
                    this.ucSereServTree.Dock = DockStyle.Fill;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void UCExportXml_Load(object sender, EventArgs e)
        {
            try
            {
                this.SetCaptionByLanguageKey();
                this.AddFilterItem();
                this.InItCboFeeLockOrEndTreatment();
                this.InitComboStatus();
                this.InitComboXml130Result();
                this.SetDefaultValueControl();
                this.FillDataToGridTreatment();
                this.InitComboTreatmentType();
                this.InitComboBranch();
                this.InitComboPatientType();
                this.InitControlState();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void SetCaptionByLanguageKey()
        {
            try
            {
                ////Khoi tao doi tuong resource
                Resources.ResourceLanguageManager.LanguageResource = new ResourceManager("HIS.Desktop.Plugins.ExportXmlQD130.Resources.Lang", typeof(UCExportXml).Assembly);

                ////Gan gia tri cho cac control editor co Text/Caption/ToolTip/NullText/NullValuePrompt/FindNullPrompt
                this.layoutControl1.Text = Inventec.Common.Resource.Get.Value("UCExportXml.layoutControl1.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.btnSettingConfigSync.ToolTip = Inventec.Common.Resource.Get.Value("UCExportXml.btnSettingConfigSync.ToolTip", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.btnAutoSync.Text = Inventec.Common.Resource.Get.Value("UCExportXml.btnAutoSync.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.btnAutoSync.ToolTip = Inventec.Common.Resource.Get.Value("UCExportXml.btnAutoSync.ToolTip", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.btnSync.Text = Inventec.Common.Resource.Get.Value("UCExportXml.btnSync.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.btnUnlock.Text = Inventec.Common.Resource.Get.Value("UCExportXml.btnUnlock.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.btnLock.Text = Inventec.Common.Resource.Get.Value("UCExportXml.btnLock.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.cboPatientType.Properties.NullText = Inventec.Common.Resource.Get.Value("UCExportXml.cboPatientType.Properties.NullText", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.btnPath.ToolTip = Inventec.Common.Resource.Get.Value("UCExportXml.btnPath.ToolTip", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.txtPatientCode.Properties.NullValuePrompt = Inventec.Common.Resource.Get.Value("UCExportXml.txtPatientCode.Properties.NullValuePrompt", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.cboFilterType.Text = Inventec.Common.Resource.Get.Value("UCExportXml.cboFilterType.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.cboStatusFeeLockOrEndTreatment.Text = Inventec.Common.Resource.Get.Value("UCExportXml.cboStatusFeeLockOrEndTreatment.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.txtKeyword.Properties.NullValuePrompt = Inventec.Common.Resource.Get.Value("UCExportXml.txtKeyword.Properties.NullValuePrompt", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.txtKeyword.ToolTip = Inventec.Common.Resource.Get.Value("UCExportXml.txtKeyword.ToolTip", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.cboStatus.Properties.NullText = Inventec.Common.Resource.Get.Value("UCExportXml.cboStatus.Properties.NullText", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.txtTreatCodeOrHeinCard.Properties.NullValuePrompt = Inventec.Common.Resource.Get.Value("UCExportXml.txtTreatCodeOrHeinCard.Properties.NullValuePrompt", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.btnDownload.Text = Inventec.Common.Resource.Get.Value("UCExportXml.btnDownload.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.btnImport.Text = Inventec.Common.Resource.Get.Value("UCExportXml.btnImport.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.btnImport.ToolTip = Inventec.Common.Resource.Get.Value("UCExportXml.btnImport.ToolTip", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.CboBranch.Properties.NullText = Inventec.Common.Resource.Get.Value("UCExportXml.CboBranch.Properties.NullText", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.btnExportXml.Text = Inventec.Common.Resource.Get.Value("UCExportXml.btnExportXml.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridCol_ViewXML.Caption = Inventec.Common.Resource.Get.Value("UCExportXml.gridCol_ViewXML.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridCol_ViewXML.ToolTip = Inventec.Common.Resource.Get.Value("UCExportXml.gridCol_ViewXML.ToolTip", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridCol.Caption = Inventec.Common.Resource.Get.Value("UCExportXml.gridCol.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridCol_Treatment_Stt.Caption = Inventec.Common.Resource.Get.Value("UCExportXml.gridCol_Treatment_Stt.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridCol_Treatment_TreatmentCode.Caption = Inventec.Common.Resource.Get.Value("UCExportXml.gridCol_Treatment_TreatmentCode.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridColumn_Treatment_PatientCode.Caption = Inventec.Common.Resource.Get.Value("UCExportXml.gridColumn_Treatment_PatientCode.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridCol_Treatment_VirPatientName.Caption = Inventec.Common.Resource.Get.Value("UCExportXml.gridCol_Treatment_VirPatientName.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridCol_Treatment_Gender.Caption = Inventec.Common.Resource.Get.Value("UCExportXml.gridCol_Treatment_Gender.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridCol_Treatment_Dob.Caption = Inventec.Common.Resource.Get.Value("UCExportXml.gridCol_Treatment_Dob.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridCol_Treatment_HeinCardNumber.Caption = Inventec.Common.Resource.Get.Value("UCExportXml.gridCol_Treatment_HeinCardNumber.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridColumn_Treatment_EndDepartment.Caption = Inventec.Common.Resource.Get.Value("UCExportXml.gridColumn_Treatment_EndDepartment.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridColumn3.Caption = Inventec.Common.Resource.Get.Value("UCExportXml.gridColumn3.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridCol_Treatment_InTime.Caption = Inventec.Common.Resource.Get.Value("UCExportXml.gridCol_Treatment_InTime.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridCol_Clinical_InTime.Caption = Inventec.Common.Resource.Get.Value("UCExportXml.gridCol_Clinical_InTime.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridCol_Treatment_OutTime.Caption = Inventec.Common.Resource.Get.Value("UCExportXml.gridCol_Treatment_OutTime.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridCol_Treatment_FeeLockTime.Caption = Inventec.Common.Resource.Get.Value("UCExportXml.gridCol_Treatment_FeeLockTime.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridCol_Treatment_HeinLockTime.Caption = Inventec.Common.Resource.Get.Value("UCExportXml.gridCol_Treatment_HeinLockTime.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridColumn_Treatment_TotalPrice.Caption = Inventec.Common.Resource.Get.Value("UCExportXml.gridColumn_Treatment_TotalPrice.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridColumn_Treatment_TotalHeinPrice.Caption = Inventec.Common.Resource.Get.Value("UCExportXml.gridColumn_Treatment_TotalHeinPrice.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridColumn_Treatment_TotalPatientPrice.Caption = Inventec.Common.Resource.Get.Value("UCExportXml.gridColumn_Treatment_TotalPatientPrice.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.btnFind.Text = Inventec.Common.Resource.Get.Value("UCExportXml.btnFind.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.txtHeinCardPrefix.Properties.NullValuePrompt = Inventec.Common.Resource.Get.Value("UCExportXml.txtHeinCardPrefix.Properties.NullValuePrompt", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.cboFilterTreatmentType.Properties.NullText = Inventec.Common.Resource.Get.Value("UCExportXml.cboFilterTreatmentType.Properties.NullText", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.lciTimeFrom.Text = Inventec.Common.Resource.Get.Value("UCExportXml.lciTimeFrom.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.lciTimeTo.Text = Inventec.Common.Resource.Get.Value("UCExportXml.lciTimeTo.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.LciCboBranch.Text = Inventec.Common.Resource.Get.Value("UCExportXml.LciCboBranch.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.layoutControlItem2.Text = Inventec.Common.Resource.Get.Value("UCExportXml.layoutControlItem2.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.layoutControlItem13.Text = Inventec.Common.Resource.Get.Value("UCExportXml.layoutControlItem13.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.layoutControlItem20.Text = Inventec.Common.Resource.Get.Value("UCExportXml.layoutControlItem20.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.layoutControlItem22.Text = Inventec.Common.Resource.Get.Value("UCExportXml.layoutControlItem22.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.layoutControlItem16.Text = Inventec.Common.Resource.Get.Value("UCExportXml.layoutControlItem16.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.layoutControlItem18.Text = Inventec.Common.Resource.Get.Value("UCExportXml.layoutControlItem18.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.layoutControlItem3.Text = Inventec.Common.Resource.Get.Value("UCExportXml.layoutControlItem3.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.cboXml130Result.Properties.NullText = Inventec.Common.Resource.Get.Value("UCExportXml.cboXml130Result.Properties.NullText", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.layoutControlItem24.Text = Inventec.Common.Resource.Get.Value("UCExportXml.layoutControlItem24.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }


        public void InitComboStatus()
        {
            try
            {
                List<FilterTypeADO> ListStatusAll = new List<FilterTypeADO>();
                FilterTypeADO tatCa = new FilterTypeADO(0, Resources.ResourceMessageLang.TatCa);
                ListStatusAll.Add(tatCa);

                FilterTypeADO duyetBhyt = new FilterTypeADO(1, Resources.ResourceMessageLang.DaKhoaBHYT);
                ListStatusAll.Add(duyetBhyt);

                FilterTypeADO ketthuc = new FilterTypeADO(2, Resources.ResourceMessageLang.DaKTDieuTri);
                ListStatusAll.Add(ketthuc);

                FilterTypeADO dacosovaovien = new FilterTypeADO(3, Resources.ResourceMessageLang.DaCoSoVaoVien);
                ListStatusAll.Add(dacosovaovien);

                List<ColumnInfo> columnInfos = new List<ColumnInfo>();
                columnInfos.Add(new ColumnInfo("Name", "", 250, 1));
                ControlEditorADO controlEditorADO = new ControlEditorADO("Name", "id", columnInfos, false, 250);
                ControlEditorLoader.Load(cboStatus, ListStatusAll, controlEditorADO);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
        public void InitComboXml130Result()
        {
            try
            {
                List<FilterTypeADO> ListXml130ResultAll = new List<FilterTypeADO>();
                FilterTypeADO tatCa = new FilterTypeADO(0, Resources.ResourceMessageLang.TatCa);
                ListXml130ResultAll.Add(tatCa);

                FilterTypeADO daGuiHoSo = new FilterTypeADO(1, Resources.ResourceMessageLang.DaGuiHoSo);
                ListXml130ResultAll.Add(daGuiHoSo);

                FilterTypeADO chuaGuiHoSo = new FilterTypeADO(2, Resources.ResourceMessageLang.ChuaGuiHoSo);
                ListXml130ResultAll.Add(chuaGuiHoSo);

                FilterTypeADO hoSoGuiThatBai = new FilterTypeADO(3, Resources.ResourceMessageLang.HoSoGuiThatBai);
                ListXml130ResultAll.Add(hoSoGuiThatBai);

                FilterTypeADO hoSoGuiThanhCong = new FilterTypeADO(4, Resources.ResourceMessageLang.HoSoGuiThanhCong);
                ListXml130ResultAll.Add(hoSoGuiThanhCong);

                List<ColumnInfo> columnInfos = new List<ColumnInfo>();
                columnInfos.Add(new ColumnInfo("Name", "", 250, 1));
                ControlEditorADO controlEditorADO = new ControlEditorADO("Name", "id", columnInfos, false, 250);
                ControlEditorLoader.Load(cboXml130Result, ListXml130ResultAll, controlEditorADO);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void SetDefaultValueControl()
        {
            try
            {
                cboStatus.EditValue = 0;
                cboXml130Result.EditValue = 0;
                txtSavePath.Text = this.SavePath;
                dtTimeFrom.DateTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(Inventec.Common.DateTime.Get.StartDay() ?? 0) ?? DateTime.MinValue;
                dtTimeTo.DateTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(Inventec.Common.DateTime.Get.EndDay() ?? 0) ?? DateTime.MinValue;
                dtHeinLockTime.DateTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(Inventec.Common.DateTime.Get.Now() ?? 0) ?? DateTime.MinValue;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void AddFilterItem()
        {
            try
            {
                DXPopupMenu menu = new DXPopupMenu();
                DXMenuItem itemFilterIn = new DXMenuItem(filterType__IN, new EventHandler(btnFilterType_Click));
                itemFilterIn.Tag = "filterIn";
                menu.Items.Add(itemFilterIn);

                DXMenuItem itemFilterOut = new DXMenuItem(filterType__OUT, new EventHandler(btnFilterType_Click));
                itemFilterOut.Tag = "filterOut";
                menu.Items.Add(itemFilterOut);

                cboFilterType.DropDownControl = menu;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void InItCboFeeLockOrEndTreatment()
        {
            try
            {
                DXPopupMenu menu = new DXPopupMenu();
                DXMenuItem itemFeeLockTime = new DXMenuItem(filterType__FeeLockTime, new EventHandler(btnFeeLockOrEndTreatment_Click));
                itemFeeLockTime.Tag = "filterFeeLockTime";
                menu.Items.Add(itemFeeLockTime);

                DXMenuItem itemFilterEndTreatment = new DXMenuItem(filterType__EndTreatmentTime, new EventHandler(btnFeeLockOrEndTreatment_Click));
                itemFilterEndTreatment.Tag = "filterEndTreatment";
                menu.Items.Add(itemFilterEndTreatment);


                DXMenuItem itemBiginTreatmentTime = new DXMenuItem(filterType__BeginTreatmentTime, new EventHandler(btnFeeLockOrEndTreatment_Click));
                itemFilterEndTreatment.Tag = "BiginTreatmentTime";
                menu.Items.Add(itemBiginTreatmentTime);

                cboStatusFeeLockOrEndTreatment.DropDownControl = menu;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void btnFilterType_Click(object sender, EventArgs e)
        {
            try
            {
                var btnMenuCodeFind = sender as DXMenuItem;
                cboFilterType.Text = btnMenuCodeFind.Caption;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void btnFeeLockOrEndTreatment_Click(object sender, EventArgs e)
        {
            try
            {
                var btnMenuCodeFind = sender as DXMenuItem;
                cboStatusFeeLockOrEndTreatment.Text = btnMenuCodeFind.Caption;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void FillDataToGridTreatment()
        {
            try
            {
                FillDataToGridTreatment(new CommonParam(0, (int)ConfigApplications.NumPageSize));

                CommonParam param = new CommonParam();
                param.Limit = rowCount;
                param.Count = dataTotal;
                ucPaging1.Init(FillDataToGridTreatment, param, (int)ConfigApplications.NumPageSize, this.gridControlTreatment);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void FillDataToGridTreatment(object param)
        {
            try
            {
                listTreatment1 = new List<V_HIS_TREATMENT_1>();
                listSelection = new List<V_HIS_TREATMENT_1>();
                listTreatmentImport = null;
                gridControlTreatment.DataSource = null;
                btnExportXml.Enabled = false;
                btnLock.Enabled = false;
                btnUnlock.Enabled = false;
                FillDataToSereServTreeByTreatment(null);

                start = ((CommonParam)param).Start ?? 0;
                limit = ((CommonParam)param).Limit ?? 0;
                CommonParam paramCommon = new CommonParam(start, limit);

                HisTreatmentView1Filter filter = new HisTreatmentView1Filter();
                filter.ORDER_DIRECTION = "ACS";
                filter.ORDER_FIELD = "FEE_LOCK_TIME";



                if (!String.IsNullOrEmpty(txtTreatCodeOrHeinCard.Text.Trim()))
                {
                    string code = txtTreatCodeOrHeinCard.Text.Trim();
                    try
                    {
                        code = string.Format("{0:000000000000}", Convert.ToInt64(code));
                    }
                    catch (Exception ex)
                    {
                        Inventec.Common.Logging.LogSystem.Error(ex);
                    }
                    txtTreatCodeOrHeinCard.Text = code;
                    filter.TREATMENT_CODE__EXACT = code;
                }
                else if (!String.IsNullOrEmpty(txtPatientCode.Text.Trim()))
                {
                    string code = txtPatientCode.Text.Trim();
                    try
                    {
                        code = string.Format("{0:0000000000}", Convert.ToInt64(code));
                    }
                    catch (Exception ex)
                    {
                        Inventec.Common.Logging.LogSystem.Error(ex);
                    }

                    txtPatientCode.Text = code;
                    filter.TDL_PATIENT_CODE__EXACT = code;
                }

                if (String.IsNullOrEmpty(filter.TREATMENT_CODE__EXACT) && String.IsNullOrEmpty(filter.TDL_PATIENT_CODE__EXACT))
                {
                    if (this.branchSelecteds != null && this.branchSelecteds.Count > 0)
                        filter.BRANCH_IDs = this.branchSelecteds.Select(o => o.ID).ToList();

                    if (this.patientTypeSelecteds != null && this.patientTypeSelecteds.Count > 0)
                        filter.TDL_PATIENT_TYPE_IDs = this.patientTypeSelecteds.Select(o => o.ID).ToList();

                    if (this.treatmentTypeSelecteds != null && this.treatmentTypeSelecteds.Count > 0)
                        filter.TDL_TREATMENT_TYPE_IDs = this.treatmentTypeSelecteds.Select(o => o.ID).ToList();

                    if (!String.IsNullOrWhiteSpace(txtKeyword.Text))
                    {
                        filter.KEY_WORD = txtKeyword.Text.Trim();
                    }
                    if (cboStatus.EditValue != null && (int)cboStatus.EditValue == 1)// Đã khóa BHYT
                    {
                        filter.IS_LOCK_HEIN = true;
                    }
                    else if (cboStatus.EditValue != null && (int)cboStatus.EditValue == 2)//Đã kết thúc điều trị
                    {
                        filter.IS_PAUSE = true;
                    }
                    else if (cboStatus.EditValue != null && (int)cboStatus.EditValue == 3)// Đã có số vào viện
                    {
                        filter.HAS_IN_CODE = true;
                    }
                    if (cboXml130Result.EditValue != null && (int)cboXml130Result.EditValue == 1)// Đã gửi hồ sơ
                    {
                        filter.HAS_XML130_RESULT = true;
                    }
                    else if (cboXml130Result.EditValue != null && (int)cboXml130Result.EditValue == 2)//Chưa gửi hồ sơ
                    {
                        filter.HAS_XML130_RESULT = false;
                    }
                    else if (cboXml130Result.EditValue != null && (int)cboXml130Result.EditValue == 3)//Hồ sơ gửi thất bại
                    {
                        filter.XML130_RESULT = 1;
                    }
                    else if (cboXml130Result.EditValue != null && (int)cboXml130Result.EditValue == 4)// Hồ sơ gửi thành công
                    {
                        filter.XML130_RESULT = 2;
                    }
                    if (cboStatusFeeLockOrEndTreatment.Text == this.filterType__FeeLockTime) //Thời gian khóa viện phí
                    {
                        if (dtTimeFrom.EditValue != null && dtTimeFrom.DateTime != DateTime.MinValue)
                        {
                            filter.FEE_LOCK_TIME_FROM = Convert.ToInt64(dtTimeFrom.DateTime.ToString("yyyyMMddHHmm") + "00");
                        }
                        if (dtTimeTo.EditValue != null && dtTimeTo.DateTime != DateTime.MinValue)
                        {
                            filter.FEE_LOCK_TIME_TO = Convert.ToInt64(dtTimeTo.DateTime.ToString("yyyyMMddHHmm") + "59");
                        }
                    }
                    else if (cboStatusFeeLockOrEndTreatment.Text == this.filterType__EndTreatmentTime) //Thời gian kết thúc điều trị
                    {
                        if (dtTimeFrom.EditValue != null && dtTimeFrom.DateTime != DateTime.MinValue)
                        {
                            filter.OUT_TIME_FROM = Convert.ToInt64(dtTimeFrom.DateTime.ToString("yyyyMMdd") + "000000");
                        }
                        if (dtTimeTo.EditValue != null && dtTimeTo.DateTime != DateTime.MinValue)
                        {
                            filter.OUT_TIME_TO = Convert.ToInt64(dtTimeTo.DateTime.ToString("yyyyMMdd") + "235959");
                        }

                    }
                    else if (cboStatusFeeLockOrEndTreatment.Text == filterType__BeginTreatmentTime) //Thời gian vào viện
                    {

                        if (dtTimeFrom.EditValue != null && dtTimeFrom.DateTime != DateTime.MinValue)
                        {
                            filter.IN_TIME_FROM = Convert.ToInt64(dtTimeFrom.DateTime.ToString("yyyyMMdd") + "000000");
                        }
                        if (dtTimeTo.EditValue != null && dtTimeTo.DateTime != DateTime.MinValue)
                        {
                            filter.IN_TIME_TO = Convert.ToInt64(dtTimeTo.DateTime.ToString("yyyyMMdd") + "235959");
                        }
                    }
                    if (!String.IsNullOrEmpty(txtHeinCardPrefix.Text) && !String.IsNullOrEmpty(txtHeinCardPrefix.Text.Trim()))
                    {
                        string[] heinCardArr = txtHeinCardPrefix.Text.Trim().Split(new char[] { ',' });
                        if (heinCardArr != null && heinCardArr.Length > 0)
                        {
                            foreach (var item in heinCardArr)
                            {
                                if (String.IsNullOrEmpty(item.Trim()))
                                    continue;
                                var card = item.Trim().ToUpper();
                                if (cboFilterType.Text == filterType__IN)
                                {
                                    if (filter.TDL_HEIN_CARD_NUMBER_PREFIXs == null) filter.TDL_HEIN_CARD_NUMBER_PREFIXs = new List<string>();
                                    filter.TDL_HEIN_CARD_NUMBER_PREFIXs.Add(card);
                                }
                                else if (cboFilterType.Text == filterType__OUT)
                                {
                                    if (filter.TDL_HEIN_CARD_NUMBER_PREFIX__NOT_INs == null) filter.TDL_HEIN_CARD_NUMBER_PREFIX__NOT_INs = new List<string>();
                                    filter.TDL_HEIN_CARD_NUMBER_PREFIX__NOT_INs.Add(card);
                                }
                                else
                                {
                                    if (filter.TDL_HEIN_CARD_NUMBER_PREFIXs == null) filter.TDL_HEIN_CARD_NUMBER_PREFIXs = new List<string>();
                                    filter.TDL_HEIN_CARD_NUMBER_PREFIXs.Add(card);
                                }
                            }
                        }
                    }
                }
                Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData("filter__:", filter));

                var result = new Inventec.Common.Adapter.BackendAdapter(paramCommon).GetRO<List<V_HIS_TREATMENT_1>>("api/HisTreatment/GetView1", ApiConsumers.MosConsumer, filter, paramCommon);
                if (result != null)
                {
                    listTreatment1 = (List<V_HIS_TREATMENT_1>)result.Data;
                    rowCount = (listTreatment1 == null ? 0 : listTreatment1.Count);
                    dataTotal = (result.Param == null ? 0 : result.Param.Count ?? 0);
                }
                gridControlTreatment.BeginUpdate();
                gridControlTreatment.DataSource = listTreatment1;
                gridControlTreatment.EndUpdate();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void FillDataToSereServTreeByTreatment(V_HIS_TREATMENT_1 data)
        {
            try
            {
                var listSereServ = new List<V_HIS_SERE_SERV_5>();
                if (data != null)
                {
                    HisSereServView5Filter ssFilter = new HisSereServView5Filter();
                    ssFilter.TDL_TREATMENT_ID = data.ID;
                    listSereServ = new Inventec.Common.Adapter.BackendAdapter(new CommonParam()).Get<List<V_HIS_SERE_SERV_5>>("api/HisSereServ/GetView5", ApiConsumers.MosConsumer, ssFilter, null);
                }

                this.ssTreeProcessor.Reload(ucSereServTree, listSereServ);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void dtTimeFrom_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            try
            {
                if (e.CloseMode == DevExpress.XtraEditors.PopupCloseMode.Normal)
                {
                    dtTimeTo.Focus();
                    dtTimeTo.ShowPopup();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void dtTimeTo_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            try
            {
                if (e.CloseMode == DevExpress.XtraEditors.PopupCloseMode.Normal)
                {
                    txtTreatCodeOrHeinCard.Focus();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void txtTreatCodeOrHeinCard_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (String.IsNullOrEmpty(txtTreatCodeOrHeinCard.Text))
                    {
                        txtPatientCode.Focus();
                        txtPatientCode.SelectAll();
                    }
                    else
                    {
                        this.btnFind_Click(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void txtKeyword_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.btnFind_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void gridViewTreatment_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            try
            {
                if (e.ListSourceRowIndex < 0 || !e.IsGetData || e.Column.UnboundType == DevExpress.Data.UnboundColumnType.Bound)
                    return;
                var data = (V_HIS_TREATMENT_1)gridViewTreatment.GetRow(e.ListSourceRowIndex);
                if (data != null)
                {
                    if (e.Column.FieldName == "STT")
                    {
                        e.Value = e.ListSourceRowIndex + 1 + start;
                    }
                    else if (e.Column.FieldName == "DOB_STR")
                    {
                        e.Value = Inventec.Common.DateTime.Convert.TimeNumberToDateString(data.TDL_PATIENT_DOB);
                    }
                    else if (e.Column.FieldName == "IN_TIME_STR")
                    {
                        e.Value = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(data.IN_TIME);
                    }
                    else if (e.Column.FieldName == "CLINICAL_IN_TIME_STR")
                    {
                        e.Value = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(data.CLINICAL_IN_TIME ?? 0);
                    }
                    else if (e.Column.FieldName == "OUT_TIME_STR")
                    {
                        e.Value = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(data.OUT_TIME ?? 0);
                    }
                    else if (e.Column.FieldName == "FEE_LOCK_TIME_STR")
                    {
                        e.Value = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(data.FEE_LOCK_TIME ?? 0);
                    }
                    else if (e.Column.FieldName == "HEIN_LOCK_TIME_STR")
                    {
                        e.Value = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(data.HEIN_LOCK_TIME ?? 0);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void gridViewTreatment_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                    return;
                WaitingManager.Show();
                var row = (V_HIS_TREATMENT_1)gridViewTreatment.GetFocusedRow();
                FillDataToSereServTreeByTreatment(row);
                WaitingManager.Hide();
            }
            catch (Exception ex)
            {
                WaitingManager.Hide();
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void gridViewTreatment_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            try
            {
                listSelection = new List<V_HIS_TREATMENT_1>();
                var listIndex = gridViewTreatment.GetSelectedRows();
                foreach (var index in listIndex)
                {
                    var treatment = (V_HIS_TREATMENT_1)gridViewTreatment.GetRow(index);
                    if (treatment != null)
                    {
                        listSelection.Add(treatment);
                    }
                }

                if (listSelection.Count > 0)
                {
                    btnExportXml.Enabled = true;
                }
                else
                {
                    btnExportXml.Enabled = false;
                }

                gridViewTreatment.BeginDataUpdate();
                gridViewTreatment.EndDataUpdate();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void treeSereServ_CustomDrawNodeCell(SereServADO data, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            try
            {
                if (data != null && !e.Node.HasChildren)
                {
                    if (!data.VIR_TOTAL_PATIENT_PRICE.HasValue || data.VIR_TOTAL_PATIENT_PRICE.Value <= 0)
                    {
                        e.Appearance.Font = new Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size, FontStyle.Italic);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                if (!btnFind.Enabled)
                    return;
                WaitingManager.Show();
                FillDataToGridTreatment();
                if (listTreatment1 != null && listTreatment1.Count == 1)
                {
                    FillDataToSereServTreeByTreatment(listTreatment1.First());
                }
                gridControlTreatment.Focus();
                WaitingManager.Hide();
            }
            catch (Exception ex)
            {
                WaitingManager.Hide();
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void btnExportXml_Click(object sender, EventArgs e)
        {
            try
            {
                if (!btnExportXml.Enabled || listSelection == null || listSelection.Count == 0) return;
                CommonParam param = new CommonParam();
                MemoryStream memoryStream = new MemoryStream();
                bool success = false;

                if (string.IsNullOrEmpty(this.SavePath))
                {
                    btnPath_Click(null, null);
                }
                if (!string.IsNullOrEmpty(this.SavePath))
                {
                    WaitingManager.Show();
                    Inventec.Common.Logging.LogSystem.Info("btnExportXml_Click Begin");
                    success = this.GenerateXml(ref param, ref memoryStream, false, listSelection);
                    Inventec.Common.Logging.LogSystem.Info("btnExportXml_Click End");
                    WaitingManager.Hide();
                    if (success && param.Messages.Count == 0)
                    {
                        MessageManager.Show(this.ParentForm, param, success);
                    }
                    else
                    {
                        MessageManager.Show(param, success);
                    }

                    this.gridControlTreatment.RefreshDataSource();
                }
                SessionManager.ProcessTokenLost(param);
            }
            catch (Exception ex)
            {
                WaitingManager.Hide();
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
        bool GenerateXml(ref CommonParam paramExport, ref MemoryStream memoryStream, bool viewXml, List<V_HIS_TREATMENT_1> listSelection)
        {
            bool result = false;
            try
            {
                if (listSelection.Count > 0)
                {
                    listSelection = listSelection.GroupBy(o => o.TREATMENT_CODE).Select(s => s.First()).ToList();
                    if (String.IsNullOrEmpty(this.SavePath))
                    {
                        this.SavePath = ConfigStore.GetFolderSaveXml + "\\ExportXmlPlus\\Xml" + DateTime.Now.ToString("ddMMyyyy");
                        var dicInfo = System.IO.Directory.CreateDirectory(this.SavePath);
                        if (dicInfo == null)
                        {
                            paramExport.Messages.Add(Resources.ResourceMessageLang.KhongTaoDuocFolderLuuXml);
                            return result;
                        }
                    }
                    this.NewConfig = GetNewConfig();
                    int skip = 0;
                    while (listSelection.Count - skip > 0)
                    {
                        var limit = listSelection.Skip(skip).Take(GlobalVariables.MAX_REQUEST_LENGTH_PARAM).ToList();
                        skip = skip + GlobalVariables.MAX_REQUEST_LENGTH_PARAM;

                        ListPatientTypeAlter = new List<V_HIS_PATIENT_TYPE_ALTER>();
                        ListSereServ = new List<V_HIS_SERE_SERV_2>();
                        ListEkipUser = new List<HIS_EKIP_USER>();
                        ListBedlog = new List<V_HIS_BED_LOG>();
                        HisTreatments = new List<V_HIS_TREATMENT_12>();
                        ListDhst = new List<HIS_DHST>();
                        HisTrackings = new List<HIS_TRACKING>();
                        HisSereServTeins = new List<V_HIS_SERE_SERV_TEIN>();
                        HisSereServPttts = new List<V_HIS_SERE_SERV_PTTT>();
                        ListDebates = new List<HIS_DEBATE>();
                        ListBaby = new List<V_HIS_BABY>();
                        ListMedicalAssessment = new List<V_HIS_MEDICAL_ASSESSMENT>();
                        ListHivTreatment = new List<HIS_HIV_TREATMENT>();
                        string message = "";
                        CreateThreadGetData(limit);
                        message = ProcessExportXmlDetail(ref result, ref memoryStream, viewXml, HisTreatments, ListPatientTypeAlter, ListSereServ, ListDhst, HisSereServTeins, HisTrackings, HisSereServPttts, ListEkipUser, ListBedlog, ListDebates, ListBaby, ListMedicalAssessment, ListHivTreatment);
                        if (!String.IsNullOrEmpty(message))
                        {
                            paramExport.Messages.Add(message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        string ProcessExportXmlDetail(ref bool isSuccess, ref MemoryStream memoryStream, bool viewXml, List<V_HIS_TREATMENT_12> hisTreatments, List<V_HIS_PATIENT_TYPE_ALTER> hisPatientTypeAlters,
            List<V_HIS_SERE_SERV_2> ListSereServ, List<HIS_DHST> listDhst, List<V_HIS_SERE_SERV_TEIN> listSereServTein,
            List<HIS_TRACKING> hisTrackings, List<V_HIS_SERE_SERV_PTTT> hisSereServPttts, List<HIS_EKIP_USER> ListEkipUser,
            List<V_HIS_BED_LOG> ListBedlog, List<HIS_DEBATE> listDebate, List<V_HIS_BABY> listBaby, List<V_HIS_MEDICAL_ASSESSMENT> listMedicalAssessment, List<HIS_HIV_TREATMENT> listHivTreatment)
        {
            string result = "";
            Dictionary<string, List<string>> DicErrorMess = new Dictionary<string, List<string>>();
            try
            {
                Dictionary<long, List<V_HIS_PATIENT_TYPE_ALTER>> dicPatientTypeAlter = new Dictionary<long, List<V_HIS_PATIENT_TYPE_ALTER>>();
                Dictionary<long, List<V_HIS_SERE_SERV_2>> dicSereServ = new Dictionary<long, List<V_HIS_SERE_SERV_2>>();
                Dictionary<long, List<V_HIS_SERE_SERV_TEIN>> dicSereServTein = new Dictionary<long, List<V_HIS_SERE_SERV_TEIN>>();
                Dictionary<long, List<V_HIS_SERE_SERV_PTTT>> dicSereServPttt = new Dictionary<long, List<V_HIS_SERE_SERV_PTTT>>();
                Dictionary<long, List<V_HIS_BED_LOG>> dicBedLog = new Dictionary<long, List<V_HIS_BED_LOG>>();
                Dictionary<long, List<HIS_TRACKING>> dicTracking = new Dictionary<long, List<HIS_TRACKING>>();
                Dictionary<long, List<HIS_EKIP_USER>> dicEkipUser = new Dictionary<long, List<HIS_EKIP_USER>>();
                Dictionary<long, List<V_HIS_BABY>> dicBaby = new Dictionary<long, List<V_HIS_BABY>>();
                Dictionary<long, List<HIS_DEBATE>> dicDebate = new Dictionary<long, List<HIS_DEBATE>>();
                Dictionary<long, List<HIS_DHST>> dicDhstList = new Dictionary<long, List<HIS_DHST>>();
                Dictionary<long, List<V_HIS_MEDICAL_ASSESSMENT>> dicMedicalAssessment = new Dictionary<long, List<V_HIS_MEDICAL_ASSESSMENT>>();
                Dictionary<long, HIS_HIV_TREATMENT> dicHivTreatment = new Dictionary<long, HIS_HIV_TREATMENT>();

                if (hisPatientTypeAlters != null && hisPatientTypeAlters.Count > 0)
                {
                    foreach (var item in hisPatientTypeAlters)
                    {
                        if (!dicPatientTypeAlter.ContainsKey(item.TREATMENT_ID))
                            dicPatientTypeAlter[item.TREATMENT_ID] = new List<V_HIS_PATIENT_TYPE_ALTER>();
                        dicPatientTypeAlter[item.TREATMENT_ID].Add(item);
                    }
                }

                if (ListSereServ != null && ListSereServ.Count > 0)
                {
                    foreach (var sereServ in ListSereServ)
                    {
                        if (sereServ.TDL_HEIN_SERVICE_TYPE_ID.HasValue && sereServ.AMOUNT > 0 && sereServ.PRICE > 0 && sereServ.IS_EXPEND != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE && sereServ.IS_NO_EXECUTE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE && sereServ.TDL_TREATMENT_ID.HasValue)
                        {
                            if (!dicSereServ.ContainsKey(sereServ.TDL_TREATMENT_ID.Value))
                                dicSereServ[sereServ.TDL_TREATMENT_ID.Value] = new List<V_HIS_SERE_SERV_2>();
                            dicSereServ[sereServ.TDL_TREATMENT_ID.Value].Add(sereServ);
                        }

                        if (sereServ.EKIP_ID.HasValue && ListEkipUser != null && ListEkipUser.Count > 0 && sereServ.TDL_TREATMENT_ID.HasValue)
                        {
                            var ekips = ListEkipUser.Where(o => o.EKIP_ID == sereServ.EKIP_ID).ToList();
                            if (ekips != null && ekips.Count > 0)
                            {
                                foreach (var item in ekips)
                                {
                                    if (!dicEkipUser.ContainsKey(sereServ.TDL_TREATMENT_ID.Value))
                                        dicEkipUser[sereServ.TDL_TREATMENT_ID.Value] = new List<HIS_EKIP_USER>();

                                    dicEkipUser[sereServ.TDL_TREATMENT_ID.Value].Add(item);
                                }
                            }
                        }
                    }
                }

                if (listSereServTein != null && listSereServTein.Count > 0)
                {
                    foreach (var ssTein in listSereServTein)
                    {
                        if (!ssTein.TDL_TREATMENT_ID.HasValue) continue;

                        if (!dicSereServTein.ContainsKey(ssTein.TDL_TREATMENT_ID.Value))
                            dicSereServTein[ssTein.TDL_TREATMENT_ID.Value] = new List<V_HIS_SERE_SERV_TEIN>();

                        dicSereServTein[ssTein.TDL_TREATMENT_ID.Value].Add(ssTein);
                    }
                }

                if (hisTrackings != null && hisTrackings.Count > 0)
                {
                    foreach (var tracking in hisTrackings)
                    {
                        if (!dicTracking.ContainsKey(tracking.TREATMENT_ID))
                            dicTracking[tracking.TREATMENT_ID] = new List<HIS_TRACKING>();

                        dicTracking[tracking.TREATMENT_ID].Add(tracking);
                    }
                }
                if (listBaby != null && listBaby.Count > 0)
                {
                    foreach (var baby in listBaby)
                    {
                        if (!dicBaby.ContainsKey(baby.TREATMENT_ID))
                            dicBaby[baby.TREATMENT_ID] = new List<V_HIS_BABY>();

                        dicBaby[baby.TREATMENT_ID].Add(baby);
                    }
                }
                if (listHivTreatment != null && listHivTreatment.Count > 0)
                {
                    listHivTreatment = listHivTreatment.OrderBy(o => o.ID).ToList();
                    foreach (var hivTreatment in listHivTreatment)
                    {
                        dicHivTreatment[hivTreatment.TREATMENT_ID] = hivTreatment;
                    }
                }
                if (hisSereServPttts != null && hisSereServPttts.Count > 0)
                {
                    foreach (var ssPttt in hisSereServPttts)
                    {
                        if (!ssPttt.TDL_TREATMENT_ID.HasValue) continue;

                        if (!dicSereServPttt.ContainsKey(ssPttt.TDL_TREATMENT_ID.Value))
                            dicSereServPttt[ssPttt.TDL_TREATMENT_ID.Value] = new List<V_HIS_SERE_SERV_PTTT>();

                        dicSereServPttt[ssPttt.TDL_TREATMENT_ID.Value].Add(ssPttt);
                    }
                }

                if (listDhst != null && listDhst.Count > 0)
                {
                    foreach (var item in listDhst)
                    {
                        if (!dicDhstList.ContainsKey(item.TREATMENT_ID))
                            dicDhstList[item.TREATMENT_ID] = new List<HIS_DHST>();

                        dicDhstList[item.TREATMENT_ID].Add(item);
                    }
                }

                if (ListBedlog != null && ListBedlog.Count > 0)
                {
                    foreach (var bed in ListBedlog)
                    {
                        if (!dicBedLog.ContainsKey(bed.TREATMENT_ID))
                            dicBedLog[bed.TREATMENT_ID] = new List<V_HIS_BED_LOG>();

                        dicBedLog[bed.TREATMENT_ID].Add(bed);
                    }
                }

                if (listDebate != null && listDebate.Count > 0)
                {
                    foreach (var item in listDebate)
                    {
                        if (!dicDebate.ContainsKey(item.TREATMENT_ID))
                            dicDebate[item.TREATMENT_ID] = new List<HIS_DEBATE>();

                        dicDebate[item.TREATMENT_ID].Add(item);
                    }
                }
                if (listMedicalAssessment != null && listMedicalAssessment.Count > 0)
                {
                    foreach (var item in listMedicalAssessment)
                    {
                        if (!dicMedicalAssessment.ContainsKey(item.TREATMENT_ID))
                            dicMedicalAssessment[item.TREATMENT_ID] = new List<V_HIS_MEDICAL_ASSESSMENT>();

                        dicMedicalAssessment[item.TREATMENT_ID].Add(item);
                    }
                }
                foreach (var treatment in hisTreatments)
                {
                    InputADO ado = new InputADO();
                    ado.Treatment = treatment;
                    if (dicPatientTypeAlter.ContainsKey(treatment.ID))
                    {
                        ado.ListPatientTypeAlter = dicPatientTypeAlter[treatment.ID];
                    }

                    if (!dicSereServ.ContainsKey(treatment.ID))
                    {
                        var errorSereServ = "Hồ sơ không có dịch vụ";
                        if (!DicErrorMess.ContainsKey(errorSereServ))
                        {
                            DicErrorMess[errorSereServ] = new List<string>();
                        }

                        DicErrorMess[errorSereServ].Add(treatment.TREATMENT_CODE);
                        continue;
                    }

                    ado.ListSereServ = dicSereServ.ContainsKey(treatment.ID) ? dicSereServ[treatment.ID] : null;

                    if (dicDhstList.ContainsKey(treatment.ID))
                    {
                        ado.ListDhst = dicDhstList[treatment.ID];
                    }

                    if (dicSereServTein.ContainsKey(treatment.ID))
                    {
                        ado.ListSereServTein = dicSereServTein[treatment.ID];
                    }

                    if (dicSereServPttt.ContainsKey(treatment.ID))
                    {
                        ado.ListSereServPttt = dicSereServPttt[treatment.ID];
                    }

                    if (dicBedLog.ContainsKey(treatment.ID))
                    {
                        ado.ListBedLog = dicBedLog[treatment.ID];
                    }

                    if (dicTracking.ContainsKey(treatment.ID))
                    {
                        ado.ListTracking = dicTracking[treatment.ID];
                    }

                    if (dicEkipUser.ContainsKey(treatment.ID))
                    {
                        ado.ListEkipUser = dicEkipUser[treatment.ID].Distinct().ToList();
                    }

                    if (dicDebate.ContainsKey(treatment.ID))
                    {
                        ado.ListDebate = dicDebate[treatment.ID];
                    }

                    if (dicBaby.ContainsKey(treatment.ID))
                    {
                        ado.ListBaby = dicBaby[treatment.ID];
                    }
                    if (dicMedicalAssessment.ContainsKey(treatment.ID))
                    {
                        ado.ListMedicalAssessment = dicMedicalAssessment[treatment.ID];
                    }
                    if (dicHivTreatment.ContainsKey(treatment.ID))
                    {
                        ado.HivTreatment = dicHivTreatment[treatment.ID];
                    }
                    ado.TotalMaterialTypeData = BackendDataWorker.Get<HIS_MATERIAL_TYPE>();
                    ado.TotalHeinMediOrgData = BackendDataWorker.Get<HIS_MEDI_ORG>();
                    ado.TotalConfigData = NewConfig;
                    ado.TotalPatientTypeData = BackendDataWorker.Get<HIS_PATIENT_TYPE>();
                    ado.TotalIcdData = BackendDataWorker.Get<HIS_ICD>();
                    ado.TotalSericeData = BackendDataWorker.Get<V_HIS_SERVICE>();
                    ado.TotalEmployeeData = BackendDataWorker.Get<HIS_EMPLOYEE>();
                    His.Bhyt.ExportXml.XML130.CreateXmlProcessor xmlProcessor = new His.Bhyt.ExportXml.XML130.CreateXmlProcessor(ado);

                    string errorMess = "";
                    string fullFileName = "";

                    fullFileName = xmlProcessor.GetFileName();
                    saveFilePath = String.Format("{0}/{1}", this.SavePath, fullFileName);
                    var rs = xmlProcessor.Run(ref errorMess);
                    if (!String.IsNullOrWhiteSpace(errorMess))
                    {
                        Inventec.Common.Logging.LogSystem.Error("Run130: " + errorMess);
                    }
                    if (rs != null)
                    {
                        if (viewXml)
                        {
                            memoryStream = rs;
                        }
                        else
                        {
                            FileStream file = new FileStream(saveFilePath, FileMode.Create, FileAccess.Write);
                            rs.WriteTo(file);
                            file.Close();
                            rs.Close();
                        }
                        isSuccess = true;
                    }
                    else
                    {
                        if (!DicErrorMess.ContainsKey(errorMess))
                        {
                            DicErrorMess[errorMess] = new List<string>();
                        }

                        DicErrorMess[errorMess].Add(treatment.TREATMENT_CODE);
                    }
                }

                if (DicErrorMess.Count > 0)
                {
                    foreach (var item in DicErrorMess)
                    {
                        result += String.Format("{0}:{1}. ", item.Key, String.Join(",", item.Value));
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = "";
            }
            return result;
        }
        private List<HIS_CONFIG> GetNewConfig()
        {
            List<HIS_CONFIG> result = null;
            try
            {
                CommonParam paramGet = new CommonParam();
                MOS.Filter.HisConfigFilter configFilter = new MOS.Filter.HisConfigFilter();
                configFilter.IS_ACTIVE = 1;
                result = new BackendAdapter(paramGet).Get<List<MOS.EFMODEL.DataModels.HIS_CONFIG>>("/api/HisConfig/Get", ApiConsumers.MosConsumer, configFilter, paramGet);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        public void BtnFind()
        {
            try
            {
                btnFind_Click(null, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public void BtnExportXml()
        {
            try
            {
                btnExportXml_Click(null, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
        public void BtnLock()
        {
            try
            {
                btnLock_Click(null, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
        public void BtnUnLock()
        {
            try
            {
                btnUnlock_Click(null, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }


        #region Thread
        private void CreateThreadGetData(List<V_HIS_TREATMENT_1> listSelection)
        {
            try
            {
                System.Threading.Thread PatientTypeAlter = new System.Threading.Thread(ThreadGetPatientTypeAlter);
                System.Threading.Thread Baby = new System.Threading.Thread(ThreadGetBaby);
                System.Threading.Thread MedicalAssessment = new System.Threading.Thread(ThreadGetMedicalAssessment);
                System.Threading.Thread SereServ2 = new System.Threading.Thread(ThreadGetSereServ2);
                System.Threading.Thread Treatment12 = new System.Threading.Thread(ThreadGetTreatment12);
                System.Threading.Thread Dhst_Tracking = new System.Threading.Thread(ThreadGetDhst_Tracking);
                System.Threading.Thread SereServTein_PTTT = new System.Threading.Thread(ThreadGetSereServTein_PTTT);
                try
                {
                    PatientTypeAlter = new System.Threading.Thread(ThreadGetPatientTypeAlter);
                    Baby = new System.Threading.Thread(ThreadGetBaby);
                    MedicalAssessment = new System.Threading.Thread(ThreadGetMedicalAssessment);
                    SereServ2 = new System.Threading.Thread(ThreadGetSereServ2);
                    Treatment12 = new System.Threading.Thread(ThreadGetTreatment12);
                    Dhst_Tracking = new System.Threading.Thread(ThreadGetDhst_Tracking);
                    SereServTein_PTTT = new System.Threading.Thread(ThreadGetSereServTein_PTTT);

                    PatientTypeAlter.Start(listSelection);
                    Baby.Start(listSelection);
                    MedicalAssessment.Start(listSelection);
                    SereServ2.Start(listSelection);
                    Treatment12.Start(listSelection);
                    Dhst_Tracking.Start(listSelection);
                    SereServTein_PTTT.Start(listSelection);

                    PatientTypeAlter.Join();
                    Baby.Join();
                    MedicalAssessment.Join();
                    SereServ2.Join();
                    Treatment12.Join();
                    Dhst_Tracking.Join();
                    SereServTein_PTTT.Join();
                }
                catch (Exception ex)
                {
                    PatientTypeAlter.Abort();
                    Baby.Abort();
                    MedicalAssessment.Abort();
                    SereServ2.Abort();
                    Treatment12.Abort();
                    Dhst_Tracking.Abort();
                    SereServTein_PTTT.Abort();
                    Inventec.Common.Logging.LogSystem.Error(ex);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ThreadGetSereServTein_PTTT(object obj)
        {
            try
            {
                if (obj == null) return;
                List<V_HIS_TREATMENT_1> listSelection = (List<V_HIS_TREATMENT_1>)obj;

                var skip = 0;
                while (listSelection.Count - skip > 0)
                {
                    var limit = listSelection.Skip(skip).Take(GlobalVariables.MAX_REQUEST_LENGTH_PARAM).ToList();
                    skip = skip + GlobalVariables.MAX_REQUEST_LENGTH_PARAM;

                    CommonParam param = new CommonParam();

                    HisSereServTeinViewFilter ssTeinFilter = new HisSereServTeinViewFilter();
                    ssTeinFilter.TDL_TREATMENT_IDs = limit.Select(s => s.ID).ToList();
                    var resulTein = new Inventec.Common.Adapter.BackendAdapter(param).Get<List<V_HIS_SERE_SERV_TEIN>>("api/HisSereServTein/GetView", ApiConsumers.MosConsumer, ssTeinFilter, param);
                    if (resulTein != null && resulTein.Count > 0)
                    {
                        HisSereServTeins.AddRange(resulTein);
                    }

                    HisSereServPtttViewFilter ssPtttFilter = new HisSereServPtttViewFilter();
                    ssPtttFilter.TDL_TREATMENT_IDs = limit.Select(s => s.ID).ToList();
                    var resultPttt = new Inventec.Common.Adapter.BackendAdapter(param).Get<List<V_HIS_SERE_SERV_PTTT>>("api/HisSereServPttt/GetView", ApiConsumers.MosConsumer, ssPtttFilter, param);
                    if (resultPttt != null && resultPttt.Count > 0)
                    {
                        HisSereServPttts.AddRange(resultPttt);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ThreadGetDhst_Tracking(object obj)
        {
            try
            {
                if (obj == null) return;
                List<V_HIS_TREATMENT_1> listSelection = (List<V_HIS_TREATMENT_1>)obj;

                var skip = 0;
                while (listSelection.Count - skip > 0)
                {
                    var limit = listSelection.Skip(skip).Take(GlobalVariables.MAX_REQUEST_LENGTH_PARAM).ToList();
                    skip = skip + GlobalVariables.MAX_REQUEST_LENGTH_PARAM;

                    CommonParam param = new CommonParam();

                    HisDhstFilter dhstFilter = new HisDhstFilter();
                    dhstFilter.TREATMENT_IDs = limit.Select(o => o.ID).ToList();
                    var resultDhst = new Inventec.Common.Adapter.BackendAdapter(param).Get<List<HIS_DHST>>(HisRequestUriStore.HIS_DHST_GET, ApiConsumers.MosConsumer, dhstFilter, param);
                    if (resultDhst != null && resultDhst.Count > 0)
                    {
                        ListDhst.AddRange(resultDhst);
                    }

                    HisTrackingFilter trackingFilter = new HisTrackingFilter();
                    trackingFilter.TREATMENT_IDs = limit.Select(o => o.ID).ToList();
                    var resultTracking = new Inventec.Common.Adapter.BackendAdapter(param).Get<List<HIS_TRACKING>>("api/HisTracking/Get", ApiConsumers.MosConsumer, trackingFilter, param);
                    if (resultTracking != null && resultTracking.Count > 0)
                    {
                        HisTrackings.AddRange(resultTracking);
                    }

                    HisDebateFilter debateFilter = new HisDebateFilter();
                    debateFilter.TREATMENT_IDs = limit.Select(o => o.ID).ToList();
                    var resultDebate = new Inventec.Common.Adapter.BackendAdapter(param).Get<List<HIS_DEBATE>>("api/HisDebate/Get", ApiConsumers.MosConsumer, debateFilter, param);
                    if (resultDebate != null && resultDebate.Count > 0)
                    {
                        ListDebates.AddRange(resultDebate);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ThreadGetTreatment12(object obj)
        {
            try
            {
                if (obj == null) return;
                List<V_HIS_TREATMENT_1> listSelection = (List<V_HIS_TREATMENT_1>)obj;

                var skip = 0;
                while (listSelection.Count - skip > 0)
                {
                    var limit = listSelection.Skip(skip).Take(GlobalVariables.MAX_REQUEST_LENGTH_PARAM).ToList();
                    skip = skip + GlobalVariables.MAX_REQUEST_LENGTH_PARAM;

                    CommonParam param = new CommonParam();
                    HisTreatmentView12Filter treatmentFilter = new HisTreatmentView12Filter();
                    treatmentFilter.IDs = limit.Select(o => o.ID).ToList();
                    var resultTreatment = new Inventec.Common.Adapter.BackendAdapter(param).Get<List<V_HIS_TREATMENT_12>>("api/HisTreatment/GetView12", ApiConsumers.MosConsumer, treatmentFilter, param);
                    if (resultTreatment != null && resultTreatment.Count > 0)
                    {
                        HisTreatments.AddRange(resultTreatment);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ThreadGetSereServ2(object obj)
        {
            try
            {
                if (obj == null) return;
                List<V_HIS_TREATMENT_1> listSelection = (List<V_HIS_TREATMENT_1>)obj;

                var skip = 0;
                while (listSelection.Count - skip > 0)
                {
                    var limit = listSelection.Skip(skip).Take(GlobalVariables.MAX_REQUEST_LENGTH_PARAM).ToList();
                    skip = skip + GlobalVariables.MAX_REQUEST_LENGTH_PARAM;

                    CommonParam param = new CommonParam();
                    HisSereServView2Filter ssFilter = new HisSereServView2Filter();
                    ssFilter.TREATMENT_IDs = limit.Select(o => o.ID).ToList();
                    var resultSS = new Inventec.Common.Adapter.BackendAdapter(param).Get<List<V_HIS_SERE_SERV_2>>(HisRequestUriStore.HIS_SERE_SERV_GETVIEW_2, ApiConsumers.MosConsumer, ssFilter, param);
                    if (resultSS != null && resultSS.Count > 0)
                    {
                        ListSereServ.AddRange(resultSS);

                        var ekipIds = resultSS.Select(o => o.EKIP_ID ?? 0).Where(o => o != 0).Distinct().ToList();
                        if (ekipIds != null && ekipIds.Count > 0)//null sẽ có 1 id bằng 0
                        {
                            HisEkipUserFilter ekipFilter = new HisEkipUserFilter();
                            ekipFilter.EKIP_IDs = ekipIds;
                            var resultEkip = new Inventec.Common.Adapter.BackendAdapter(param).Get<List<HIS_EKIP_USER>>("api/HisEkipUser/Get", ApiConsumers.MosConsumer, ekipFilter, param);
                            if (resultEkip != null && resultEkip.Count > 0)
                            {
                                ListEkipUser.AddRange(resultEkip);
                            }
                        }
                    }

                    HisBedLogViewFilter bedFilter = new HisBedLogViewFilter();
                    bedFilter.TREATMENT_IDs = limit.Select(o => o.ID).ToList();
                    var resultBed = new Inventec.Common.Adapter.BackendAdapter(param).Get<List<V_HIS_BED_LOG>>("api/HisBedLog/GetView", ApiConsumers.MosConsumer, bedFilter, param);
                    if (resultBed != null && resultBed.Count > 0)
                    {
                        ListBedlog.AddRange(resultBed);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ThreadGetPatientTypeAlter(object obj)
        {
            try
            {
                if (obj == null) return;
                List<V_HIS_TREATMENT_1> listSelection = (List<V_HIS_TREATMENT_1>)obj;

                var skip = 0;
                while (listSelection.Count - skip > 0)
                {
                    var limit = listSelection.Skip(skip).Take(GlobalVariables.MAX_REQUEST_LENGTH_PARAM).ToList();
                    skip = skip + GlobalVariables.MAX_REQUEST_LENGTH_PARAM;

                    CommonParam param = new CommonParam();
                    HisPatientTypeAlterViewFilter filter = new HisPatientTypeAlterViewFilter();
                    filter.TREATMENT_IDs = limit.Select(s => s.ID).ToList();
                    var resultPatientTypeAlter = new Inventec.Common.Adapter.BackendAdapter(param).Get<List<V_HIS_PATIENT_TYPE_ALTER>>("api/HisPatientTypeAlter/GetView", ApiConsumers.MosConsumer, filter, param);
                    if (resultPatientTypeAlter != null && resultPatientTypeAlter.Count > 0)
                    {
                        ListPatientTypeAlter.AddRange(resultPatientTypeAlter);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
        private void ThreadGetBaby(object obj)
        {
            try
            {
                if (obj == null) return;
                List<V_HIS_TREATMENT_1> listSelection = (List<V_HIS_TREATMENT_1>)obj;

                var skip = 0;
                while (listSelection.Count - skip > 0)
                {
                    var limit = listSelection.Skip(skip).Take(GlobalVariables.MAX_REQUEST_LENGTH_PARAM).ToList();
                    skip = skip + GlobalVariables.MAX_REQUEST_LENGTH_PARAM;

                    CommonParam param = new CommonParam();
                    HisBabyViewFilter filter = new HisBabyViewFilter();
                    filter.TREATMENT_IDs = limit.Select(s => s.ID).ToList();
                    var resultBaby = new Inventec.Common.Adapter.BackendAdapter(param).Get<List<V_HIS_BABY>>("api/HisBaby/GetView", ApiConsumers.MosConsumer, filter, param);
                    if (resultBaby != null && resultBaby.Count > 0)
                    {
                        ListBaby.AddRange(resultBaby);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
        private void ThreadGetMedicalAssessment(object obj)
        {
            try
            {
                if (obj == null) return;
                List<V_HIS_TREATMENT_1> listSelection = (List<V_HIS_TREATMENT_1>)obj;

                var skip = 0;
                while (listSelection.Count - skip > 0)
                {
                    var limit = listSelection.Skip(skip).Take(GlobalVariables.MAX_REQUEST_LENGTH_PARAM).ToList();
                    skip = skip + GlobalVariables.MAX_REQUEST_LENGTH_PARAM;

                    CommonParam param = new CommonParam();
                    HisMedicalAssessmentViewFilter filter = new HisMedicalAssessmentViewFilter();
                    filter.TREATMENT_IDs = limit.Select(s => s.ID).ToList();
                    var resultMedicalAssessment = new Inventec.Common.Adapter.BackendAdapter(param).Get<List<V_HIS_MEDICAL_ASSESSMENT>>("api/HisMedicalAssessment/GetView", ApiConsumers.MosConsumer, filter, param);
                    if (resultMedicalAssessment != null && resultMedicalAssessment.Count > 0)
                    {
                        ListMedicalAssessment.AddRange(resultMedicalAssessment);
                    }

                    HisHivTreatmentFilter filterHivTreatment = new HisHivTreatmentFilter();
                    filterHivTreatment.TREATMENT_IDs = limit.Select(s => s.ID).ToList();
                    var resultHivTreatment = new Inventec.Common.Adapter.BackendAdapter(param).Get<List<HIS_HIV_TREATMENT>>("api/HisHivTreatment/Get", ApiConsumers.MosConsumer, filterHivTreatment, param);
                    if (resultHivTreatment != null && resultHivTreatment.Count > 0)
                    {
                        ListHivTreatment.AddRange(resultHivTreatment);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
        #endregion

        private void gridViewTreatment_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
                {
                    GridView view = sender as GridView;
                    GridHitInfo hi = view.CalcHitInfo(e.Location);
                    if (hi.InRowCell)
                    {
                        var treatment1 = (V_HIS_TREATMENT_1)gridViewTreatment.GetRow(hi.RowHandle);
                        if (treatment1 != null)
                        {
                            if (hi.Column.FieldName == "ViewXML")
                            {
                                CommonParam param = new CommonParam();
                                MemoryStream memoryStream = new MemoryStream();
                                bool success = false;
                                WaitingManager.Show();
                                List<V_HIS_TREATMENT_1> listTreatments = new List<V_HIS_TREATMENT_1>();
                                listTreatments.Add(treatment1);
                                Inventec.Common.Logging.LogSystem.Info("btnExportXml_Click Begin");
                                success = this.GenerateXml(ref param, ref memoryStream, true, listTreatments);
                                Inventec.Common.Logging.LogSystem.Info("btnExportXml_Click End");
                                WaitingManager.Hide();
                                if (success && param.Messages.Count == 0)
                                {
                                    MessageManager.Show(this.ParentForm, param, success);
                                    Inventec.Desktop.Common.Modules.Module moduleData = GlobalVariables.currentModuleRaws.Where(o => o.ModuleLink == "HIS.Desktop.Plugins.XMLViewer130").FirstOrDefault();
                                    if (moduleData == null) throw new NullReferenceException("Not found module by ModuleLink = 'HIS.Desktop.Plugins.XMLViewer130'");
                                    if (moduleData.IsPlugin && moduleData.ExtensionInfo != null)
                                    {
                                        moduleData.RoomId = this.currentModule.RoomId;
                                        moduleData.RoomTypeId = this.currentModule.RoomTypeId;
                                        List<object> listArgs = new List<object>();
                                        if (memoryStream != null)
                                            listArgs.Add(memoryStream);
                                        else
                                        {
                                            DevExpress.XtraEditors.XtraMessageBox.Show("Lỗi tạo xml");
                                            return;
                                        }
                                        listArgs.Add(moduleData);
                                        var extenceInstance = PluginInstance.GetPluginInstance(moduleData, listArgs);
                                        if (extenceInstance == null)
                                        {
                                            throw new ArgumentNullException("moduleData is null");
                                        }

                                        ((Form)extenceInstance).ShowDialog();
                                    }
                                    else
                                    {
                                        MessageManager.Show(Resources.ResourceMessageLang.ChucNangChuaHoTroPhienBanHienTai);
                                    }
                                }
                                else
                                {
                                    MessageManager.Show(param, success);
                                }

                                this.gridControlTreatment.RefreshDataSource();

                                SessionManager.ProcessTokenLost(param);
                            }
                            else if (hi.Column.FieldName == "ErrorLine" && treatment1.XML130_RESULT == 1 && !string.IsNullOrEmpty(treatment1.XML130_DESC))
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show(treatment1.XML130_DESC);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void InitComboBranch()
        {
            try
            {
                InitCheck(CboBranch, SelectionGrid__cboBranch);
                InitCombo(CboBranch, BackendDataWorker.Get<HIS_BRANCH>(), "BRANCH_NAME");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
        private void InitComboPatientType()
        {
            InitCheck(cboPatientType, SelectionGrid__cboPatientType);
            InitCombo(cboPatientType, BackendDataWorker.Get<HIS_PATIENT_TYPE>(), "PATIENT_TYPE_NAME");
        }
        private void InitCombo(GridLookUpEdit cbo, object data, string DisplayValue)
        {
            try
            {
                cbo.Properties.DataSource = data;
                cbo.Properties.DisplayMember = DisplayValue;
                cbo.Properties.ValueMember = "ID";
                DevExpress.XtraGrid.Columns.GridColumn col2 = cbo.Properties.View.Columns.AddField(DisplayValue);
                col2.VisibleIndex = 2;
                col2.Width = 200;
                col2.Caption = Resources.ResourceMessageLang.TatCa;
                cbo.Properties.PopupFormWidth = 250;
                cbo.Properties.View.OptionsView.ShowColumnHeaders = true;
                cbo.Properties.View.OptionsSelection.MultiSelect = true;

                GridCheckMarksSelection gridCheckMark = cbo.Properties.Tag as GridCheckMarksSelection;
                if (gridCheckMark != null)
                {
                    gridCheckMark.ClearSelection(cbo.Properties.View);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void InitCheck(GridLookUpEdit cbo, GridCheckMarksSelection.SelectionChangedEventHandler eventSelect)
        {
            try
            {
                GridCheckMarksSelection gridCheck = new GridCheckMarksSelection(cbo.Properties);
                gridCheck.SelectionChanged += new GridCheckMarksSelection.SelectionChangedEventHandler(eventSelect);
                cbo.Properties.Tag = gridCheck;
                cbo.Properties.View.OptionsSelection.MultiSelect = true;
                GridCheckMarksSelection gridCheckMark = cbo.Properties.Tag as GridCheckMarksSelection;
                if (gridCheckMark != null)
                {
                    gridCheckMark.ClearSelection(cbo.Properties.View);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void SelectionGrid__cboPatientType(object sender, EventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                GridCheckMarksSelection gridCheckMark = sender as GridCheckMarksSelection;
                if (gridCheckMark != null)
                {
                    List<HIS_PATIENT_TYPE> sgSelectedNews = new List<HIS_PATIENT_TYPE>();
                    foreach (MOS.EFMODEL.DataModels.HIS_PATIENT_TYPE rv in (gridCheckMark).Selection)
                    {
                        if (rv != null)
                        {
                            if (sb.ToString().Length > 0) { sb.Append(", "); }
                            sb.Append(rv.PATIENT_TYPE_NAME.ToString());
                            sgSelectedNews.Add(rv);
                        }
                    }
                    this.patientTypeSelecteds = new List<HIS_PATIENT_TYPE>();
                    this.patientTypeSelecteds.AddRange(sgSelectedNews);
                }
                this.cboPatientType.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void SelectionGrid__cboBranch(object sender, EventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                GridCheckMarksSelection gridCheckMark = sender as GridCheckMarksSelection;
                if (gridCheckMark != null)
                {
                    List<HIS_BRANCH> sgSelectedNews = new List<HIS_BRANCH>();
                    foreach (MOS.EFMODEL.DataModels.HIS_BRANCH rv in (gridCheckMark).Selection)
                    {
                        if (rv != null)
                        {
                            if (sb.ToString().Length > 0) { sb.Append(", "); }
                            sb.Append(rv.BRANCH_NAME.ToString());
                            sgSelectedNews.Add(rv);
                        }
                    }
                    this.branchSelecteds = new List<HIS_BRANCH>();
                    this.branchSelecteds.AddRange(sgSelectedNews);
                }
                this.CboBranch.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void CboBranch_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                GridCheckMarksSelection gridCheckMark = sender is GridLookUpEdit ? (sender as GridLookUpEdit).Properties.Tag as GridCheckMarksSelection : (sender as RepositoryItemGridLookUpEdit).Tag as GridCheckMarksSelection;
                if (gridCheckMark == null) return;
                foreach (MOS.EFMODEL.DataModels.HIS_BRANCH rv in gridCheckMark.Selection)
                {
                    if (sb.ToString().Length > 0) { sb.Append(", "); }
                    sb.Append(rv.BRANCH_NAME.ToString());
                }
                e.DisplayText = sb.ToString();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void InitComboTreatmentType()
        {
            InitCheck(cboFilterTreatmentType, SelectionGrid__cboFilterTreatmentType);
            cboFilterTreatmentType.Properties.DataSource = BackendDataWorker.Get<HIS_TREATMENT_TYPE>();
            cboFilterTreatmentType.Properties.DisplayMember = "TREATMENT_TYPE_NAME";
            cboFilterTreatmentType.Properties.ValueMember = "ID";
            DevExpress.XtraGrid.Columns.GridColumn col1 = cboFilterTreatmentType.Properties.View.Columns.AddField("TREATMENT_TYPE_CODE");
            col1.VisibleIndex = 1;
            col1.Width = 50;
            col1.Caption = " ";
            DevExpress.XtraGrid.Columns.GridColumn col2 = cboFilterTreatmentType.Properties.View.Columns.AddField("TREATMENT_TYPE_NAME");
            col2.VisibleIndex = 2;
            col2.Width = 200;
            col2.Caption = Resources.ResourceMessageLang.TatCa;
            cboFilterTreatmentType.Properties.PopupFormWidth = 250;
            cboFilterTreatmentType.Properties.View.OptionsView.ShowColumnHeaders = true;
            cboFilterTreatmentType.Properties.View.OptionsSelection.MultiSelect = true;

            GridCheckMarksSelection gridCheckMark = cboFilterTreatmentType.Properties.Tag as GridCheckMarksSelection;
            if (gridCheckMark != null)
            {
                gridCheckMark.ClearSelection(cboFilterTreatmentType.Properties.View);
            }
        }
        private void SelectionGrid__cboFilterTreatmentType(object sender, EventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                GridCheckMarksSelection gridCheckMark = sender as GridCheckMarksSelection;
                if (gridCheckMark != null)
                {
                    List<HIS_TREATMENT_TYPE> sgSelectedNews = new List<HIS_TREATMENT_TYPE>();
                    foreach (MOS.EFMODEL.DataModels.HIS_TREATMENT_TYPE rv in (gridCheckMark).Selection)
                    {
                        if (rv != null)
                        {
                            if (sb.ToString().Length > 0) { sb.Append(", "); }
                            sb.Append(rv.TREATMENT_TYPE_NAME.ToString());
                            sgSelectedNews.Add(rv);
                        }
                    }
                    this.treatmentTypeSelecteds = new List<HIS_TREATMENT_TYPE>();
                    this.treatmentTypeSelecteds.AddRange(sgSelectedNews);
                }
                this.cboFilterTreatmentType.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Multiselect = false;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    WaitingManager.Show();

                    var import = new Inventec.Common.ExcelImport.Import();
                    if (import.ReadFileExcel(ofd.FileName))
                    {
                        this.listTreatmentImport = import.GetWithCheck<TreatmentImportADO>(0);
                        if (this.listTreatmentImport != null && this.listTreatmentImport.Count > 0)
                        {
                            string error = "";
                            List<HisTreatmentView1ImportFilter.TreatmentImportFilter> processImport = ProcessDataImport(this.listTreatmentImport, ref error);
                            List<V_HIS_TREATMENT_1> listTreatment = new List<V_HIS_TREATMENT_1>();

                            if (!string.IsNullOrEmpty(error))
                            {
                                WaitingManager.Hide();
                                DevExpress.XtraEditors.XtraMessageBox.Show(error, MessageUtil.GetMessage(LibraryMessage.Message.Enum.TieuDeCuaSoThongBaoLaThongBao));
                                return;
                            }
                            else if (processImport == null)
                            {
                                WaitingManager.Hide();
                                DevExpress.XtraEditors.XtraMessageBox.Show(Resources.ResourceMessageLang.LoiKhiLayDuLieuLoc, MessageUtil.GetMessage(LibraryMessage.Message.Enum.TieuDeCuaSoThongBaoLaThongBao));
                                return;
                            }
                            else
                            {
                                var skip = 0;
                                while (processImport.Count - skip >= 0)
                                {
                                    var imports = processImport.Skip(skip).Take(20).ToList();
                                    skip += 20;
                                    CommonParam param = new CommonParam();
                                    HisTreatmentView1ImportFilter filter = new HisTreatmentView1ImportFilter();
                                    filter.TreatmentImportFilters = imports;
                                    filter.ORDER_DIRECTION = "DESC";
                                    filter.ORDER_FIELD = "TREATMENT_CODE";

                                    var rsApi = new BackendAdapter(param).Get<List<V_HIS_TREATMENT_1>>("api/HisTreatment/GetByImportView1", ApiConsumer.ApiConsumers.MosConsumer, filter, param);
                                    if (rsApi != null)
                                    {
                                        listTreatment.AddRange(rsApi);
                                    }
                                }

                                if (listTreatment != null && listTreatment.Count > 0)//lọc lại danh sách
                                {
                                    listTreatment = listTreatment.GroupBy(o => o.ID).Select(s => s.First()).ToList();
                                }

                                if (listTreatment != null && listTreatment.Count > 0 && ucPaging1 != null && ucPaging1.pagingGrid != null)
                                {
                                    ucPaging1.pagingGrid.CurrentPage = 1;
                                    ucPaging1.pagingGrid.PageCount = 1;
                                    ucPaging1.pagingGrid.MaxRec = listTreatment.Count;
                                    ucPaging1.pagingGrid.DataCount = listTreatment.Count;
                                    ucPaging1.pagingGrid.LoadPage();
                                }

                                gridControlTreatment.BeginUpdate();
                                gridControlTreatment.DataSource = listTreatment;
                                gridControlTreatment.EndUpdate();

                                WaitingManager.Hide();
                            }
                        }
                    }

                    WaitingManager.Hide();
                }
            }
            catch (Exception ex)
            {
                WaitingManager.Hide();
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private List<HisTreatmentView1ImportFilter.TreatmentImportFilter> ProcessDataImport(List<TreatmentImportADO> treatmentImport, ref string error)
        {
            List<HisTreatmentView1ImportFilter.TreatmentImportFilter> result = new List<HisTreatmentView1ImportFilter.TreatmentImportFilter>();
            try
            {
                Inventec.Common.Logging.LogSystem.Info("begin time format");
                string cultureName = "en";
                string timeMax = "";
                if (treatmentImport.Exists(o => !string.IsNullOrEmpty(o.IN_TIME_STR)))
                {
                    var in_time = treatmentImport.Where(o => !string.IsNullOrEmpty(o.IN_TIME_STR)).ToList();
                    if (in_time != null && in_time.Count() > 0)
                    {
                        timeMax = in_time.OrderByDescending(o => o.IN_TIME_STR.Length).ThenByDescending(o => o.IN_TIME_STR).First().IN_TIME_STR;
                    }
                }
                else if (treatmentImport.Exists(o => !string.IsNullOrEmpty(o.OUT_TIME_STR)))
                {
                    var out_time = treatmentImport.Where(o => !string.IsNullOrEmpty(o.OUT_TIME_STR)).ToList();
                    if (out_time != null && out_time.Count() > 0)
                    {
                        timeMax = out_time.OrderByDescending(o => o.IN_TIME_STR.Length).ThenByDescending(o => o.IN_TIME_STR).First().OUT_TIME_STR;
                    }
                }

                if (!String.IsNullOrEmpty(timeMax))
                {
                    try
                    {
                        var dateTime = Convert.ToDateTime(timeMax);
                        if (dateTime != null)
                        {
                            cultureName = "vi";
                        }
                    }
                    catch (Exception ex)
                    {
                        Inventec.Common.Logging.LogSystem.Error(ex);
                        cultureName = "en";
                    }
                }

                CustomProvider provider = new CustomProvider(cultureName);
                Inventec.Common.Logging.LogSystem.Info("end time format");
                foreach (var item in treatmentImport)
                {
                    if (item == null)
                        continue;

                    if (string.IsNullOrEmpty(item.IN_TIME_STR.Trim())
                        && string.IsNullOrEmpty(item.OUT_TIME_STR.Trim())
                        && string.IsNullOrEmpty(item.TDL_HEIN_CARD_NUMBER.Trim())
                        && string.IsNullOrEmpty(item.TDL_PATIENT_CODE.Trim())
                        && string.IsNullOrEmpty(item.TDL_PATIENT_NAME.Trim())
                        && string.IsNullOrEmpty(item.TREATMENT_CODE.Trim())) continue;

                    HisTreatmentView1ImportFilter.TreatmentImportFilter filter = new HisTreatmentView1ImportFilter.TreatmentImportFilter();
                    Inventec.Common.Mapper.DataObjectMapper.Map<HisTreatmentView1ImportFilter.TreatmentImportFilter>(filter, item);

                    if (!string.IsNullOrEmpty(item.IN_TIME_STR))
                    {
                        try
                        {
                            var dateTime = Convert.ToDateTime(item.IN_TIME_STR, provider);
                            filter.IN_TIME = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(dateTime);
                            item.IN_TIME = filter.IN_TIME;
                        }
                        catch (Exception)
                        {
                            error += string.Format("Ngày vào {0} không hợp lệ|", item.IN_TIME_STR);
                        }
                    }

                    if (!string.IsNullOrEmpty(item.OUT_TIME_STR))
                    {
                        try
                        {
                            var dateTime = Convert.ToDateTime(item.OUT_TIME_STR, provider);
                            filter.OUT_TIME = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(dateTime);
                            item.OUT_TIME = filter.OUT_TIME;
                        }
                        catch (Exception)
                        {
                            error += string.Format("Ngày ra {0} không hợp lệ|", item.OUT_TIME_STR);
                        }
                    }

                    if (!string.IsNullOrEmpty(item.TDL_PATIENT_CODE))
                    {
                        if (item.TDL_PATIENT_CODE.Length < 10 && checkDigit(item.TDL_PATIENT_CODE))
                        {
                            filter.TDL_PATIENT_CODE = string.Format("{0:0000000000}", Convert.ToInt64(item.TDL_PATIENT_CODE));
                            item.TDL_PATIENT_CODE = string.Format("{0:0000000000}", Convert.ToInt64(item.TDL_PATIENT_CODE));
                        }
                        else
                        {
                            filter.TDL_PATIENT_CODE = item.TDL_PATIENT_CODE;
                        }
                    }

                    if (!string.IsNullOrEmpty(item.TREATMENT_CODE))
                    {
                        if (item.TREATMENT_CODE.Length < 12 && checkDigit(item.TREATMENT_CODE))
                        {
                            filter.TREATMENT_CODE = string.Format("{0:000000000000}", Convert.ToInt64(item.TREATMENT_CODE));
                            item.TREATMENT_CODE = string.Format("{0:000000000000}", Convert.ToInt64(item.TREATMENT_CODE));
                        }
                        else
                        {
                            filter.TREATMENT_CODE = item.TREATMENT_CODE;
                        }
                    }

                    result.Add(filter);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
                return null;
            }
            if (result.Count == 0)
                return null;
            return result;
        }

        private bool checkDigit(string s)
        {
            bool result = false;
            try
            {
                for (int i = 0; i < s.Length; i++)
                {
                    if (char.IsDigit(s[i]) == true) result = true;
                    else result = false;
                }
                return result;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return result;
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                var source = System.IO.Path.Combine(Application.StartupPath
                + "/Tmp/Imp", "IMPORT_TREATMENT_XML.xlsx");

                if (File.Exists(source))
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                    saveFileDialog1.Title = "Save File";
                    saveFileDialog1.FileName = "IMPORT_TREATMENT_XML";
                    saveFileDialog1.DefaultExt = "xlsx";
                    saveFileDialog1.Filter = "Excel files (*.xlsx)|All files (*.*)";
                    saveFileDialog1.FilterIndex = 2;
                    saveFileDialog1.RestoreDirectory = true;

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        File.Copy(source, saveFileDialog1.FileName, true);
                        DevExpress.XtraEditors.XtraMessageBox.Show(Resources.ResourceMessageLang.TaiFileVeMayTramThanhCong);
                    }
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(Resources.ResourceMessageLang.KhongTimThayFileImport);
                }
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(Resources.ResourceMessageLang.TaiFileVeMayTramThatBai);
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void cboStatusFeeLockOrEndTreatment_Click(object sender, EventArgs e)
        {
            try
            {
                cboStatusFeeLockOrEndTreatment.ShowDropDown();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void cboFilterType_Click(object sender, EventArgs e)
        {
            try
            {
                cboFilterType.ShowDropDown();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
        private void txtPatientCode_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (String.IsNullOrEmpty(txtPatientCode.Text))
                    {
                        txtPatientCode.Focus();
                        txtPatientCode.SelectAll();
                    }
                    else
                    {
                        this.btnFind_Click(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void btnPath_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    this.SavePath = fbd.SelectedPath;
                    txtSavePath.Text = this.SavePath;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void InitControlState()
        {
            try
            {
                isNotLoadWhileChangeControlStateInFirst = true;
                this.controlStateWorker = new HIS.Desktop.Library.CacheClient.ControlStateWorker();
                this.currentControlStateRDO = controlStateWorker.GetData(moduleLink);
                if (this.currentControlStateRDO != null && this.currentControlStateRDO.Count > 0)
                {
                    foreach (var item in this.currentControlStateRDO)
                    {
                        if (item.KEY == txtSavePath.Name)
                        {
                            txtSavePath.Text = item.VALUE;
                            this.SavePath = item.VALUE;
                        }
                        else if (item.KEY == btnSettingConfigSync.Name)
                        {
                            configSync = !String.IsNullOrWhiteSpace(item.VALUE) ? Newtonsoft.Json.JsonConvert.DeserializeObject<ConfigSyncADO>(item.VALUE) : null;
                        }
                    }
                }
                isNotLoadWhileChangeControlStateInFirst = false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
        private void txtSavePath_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (isNotLoadWhileChangeControlStateInFirst)
                {
                    return;
                }
                WaitingManager.Show();
                HIS.Desktop.Library.CacheClient.ControlStateRDO csAddOrUpdate = (this.currentControlStateRDO != null && this.currentControlStateRDO.Count > 0) ? this.currentControlStateRDO.Where(o => o.KEY == txtSavePath.Name && o.MODULE_LINK == moduleLink).FirstOrDefault() : null;
                if (csAddOrUpdate != null)
                {
                    csAddOrUpdate.VALUE = txtSavePath.Text;
                }
                else
                {
                    csAddOrUpdate = new HIS.Desktop.Library.CacheClient.ControlStateRDO();
                    csAddOrUpdate.KEY = txtSavePath.Name;
                    csAddOrUpdate.VALUE = txtSavePath.Text;
                    csAddOrUpdate.MODULE_LINK = moduleLink;
                    if (this.currentControlStateRDO == null)
                        this.currentControlStateRDO = new List<HIS.Desktop.Library.CacheClient.ControlStateRDO>();
                    this.currentControlStateRDO.Add(csAddOrUpdate);
                }
                this.controlStateWorker.SetData(this.currentControlStateRDO);
                WaitingManager.Hide();
            }
            catch (Exception ex)
            {
                WaitingManager.Hide();
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void cboPatientType_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                GridCheckMarksSelection gridCheckMark = sender is GridLookUpEdit ? (sender as GridLookUpEdit).Properties.Tag as GridCheckMarksSelection : (sender as DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit).Tag as GridCheckMarksSelection;
                if (gridCheckMark == null || gridCheckMark.Selection == null || gridCheckMark.Selection.Count == 0)
                {
                    e.DisplayText = "";
                    return;
                }
                foreach (MOS.EFMODEL.DataModels.HIS_PATIENT_TYPE rv in gridCheckMark.Selection)
                {
                    if (sb.ToString().Length > 0) { sb.Append(", "); }

                    sb.Append(rv.PATIENT_TYPE_NAME.ToString());
                }
                e.DisplayText = sb.ToString();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void cboFilterTreatmentType_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                GridCheckMarksSelection gridCheckMark = sender is GridLookUpEdit ? (sender as GridLookUpEdit).Properties.Tag as GridCheckMarksSelection : (sender as DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit).Tag as GridCheckMarksSelection;
                if (gridCheckMark == null || gridCheckMark.Selection == null || gridCheckMark.Selection.Count == 0)
                {
                    e.DisplayText = "";
                    return;
                }
                foreach (MOS.EFMODEL.DataModels.HIS_TREATMENT_TYPE rv in gridCheckMark.Selection)
                {
                    if (sb.ToString().Length > 0) { sb.Append(", "); }

                    sb.Append(rv.TREATMENT_TYPE_NAME.ToString());
                }
                e.DisplayText = sb.ToString();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void btnLock_Click(object sender, EventArgs e)
        {
            try
            {
                if (!btnLock.Enabled || this.currentTreatment == null)
                    return;
                CommonParam param = new CommonParam();
                bool success = false;
                WaitingManager.Show();
                HisTreatmentLockHeinSDO sdo = new HisTreatmentLockHeinSDO();
                sdo.TreatmentId = this.currentTreatment.ID;
                if (dtHeinLockTime.EditValue != null)
                {
                    sdo.HeinLockTime = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(dtHeinLockTime.DateTime);
                }
                else
                {
                    sdo.HeinLockTime = null;
                }
                var rs = new Inventec.Common.Adapter.BackendAdapter(param).Post<HIS_TREATMENT>(HisRequestUriStore.HIS_TREATMENT_LOCK_HEIN, ApiConsumers.MosConsumer, sdo, param);
                if (rs != null)
                {
                    success = true;
                    currentTreatment.IS_LOCK_HEIN = rs.IS_LOCK_HEIN;
                    currentTreatment.HEIN_LOCK_TIME = rs.HEIN_LOCK_TIME;
                    FillDataToGridTreatment();
                }
                WaitingManager.Hide();

                MessageManager.Show(this.ParentForm, param, success);
                SessionManager.ProcessTokenLost(param);
            }
            catch (Exception ex)
            {
                WaitingManager.Hide();
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            CommonParam param = new CommonParam();
            bool success = false;
            try
            {
                if (currentTreatment != null)
                {
                    WaitingManager.Show();
                    var result = new Inventec.Common.Adapter.BackendAdapter(param).Post<HIS_TREATMENT>("api/HisTreatment/UnlockHein", ApiConsumers.MosConsumer, currentTreatment.ID, param);

                    WaitingManager.Hide();
                    if (result != null)
                    {
                        success = true;
                        dtHeinLockTime.DateTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(Inventec.Common.DateTime.Get.Now() ?? 0) ?? DateTime.MinValue;
                        currentTreatment.IS_LOCK_HEIN = null;
                        currentTreatment.HEIN_LOCK_TIME = null;
                        FillDataToGridTreatment();
                    }
                    WaitingManager.Hide();
                    #region Hien thi message thong bao
                    MessageManager.Show(this.ParentForm, param, success);
                    #endregion
                }
                #region Neu phien lam viec bi mat, phan mem tu dong logout va tro ve trang login
                SessionManager.ProcessTokenLost(param);
                #endregion
            }
            catch (Exception ex)
            {
                WaitingManager.Hide();
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void gridViewTreatment_Click(object sender, EventArgs e)
        {
            try
            {
                var rowData = (MOS.EFMODEL.DataModels.V_HIS_TREATMENT_1)gridViewTreatment.GetFocusedRow();
                if (rowData != null)
                {
                    currentTreatment = rowData;
                    btnLock.Enabled = rowData.IS_LOCK_HEIN != 1 && rowData.IS_ACTIVE == 0;
                    btnUnlock.Enabled = rowData.IS_LOCK_HEIN == 1;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void gridViewTreatment_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    short xml130Result = Inventec.Common.TypeConvert.Parse.ToInt16((gridViewTreatment.GetRowCellValue(e.RowHandle, "XML130_RESULT") ?? "").ToString());
                    string xml130Desc = (gridViewTreatment.GetRowCellValue(e.RowHandle, "XML130_DESC") ?? "").ToString();
                    if (e.Column.FieldName == "ErrorLine")
                    {
                        if (xml130Result == 1)
                        {
                            if (string.IsNullOrEmpty(xml130Desc))
                                e.RepositoryItem = Btn_Failed;
                            else
                                e.RepositoryItem = Btn_ErrorLine;
                        }
                        else if (xml130Result == 2)
                        {
                            e.RepositoryItem = Btn_Success;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void btnAutoSync_Click(object sender, EventArgs e)
        {
            try
            {
                if (configSync == null)
                {
                    XtraMessageBox.Show(Resources.ResourceMessageLang.VuiLongThietLapDieuKienGuiHoSoTruocKhiThucHien, Resources.ResourceMessageLang.ThongBao);
                    ConfigSyncADO tempConfigSync = new ConfigSyncADO();
                    tempConfigSync.branchIds = this.branchSelecteds.Select(o => o.ID).ToList();
                    tempConfigSync.patientTypeIds = this.patientTypeSelecteds.Select(o => o.ID).ToList(); ;
                    tempConfigSync.treatmentTypeIds = this.treatmentTypeSelecteds.Select(o => o.ID).ToList();
                    tempConfigSync.statusId = (int)cboStatus.EditValue;
                    tempConfigSync.period = 10;
                    frmSettingConfigSync frmSettingConfigSync = new frmSettingConfigSync(tempConfigSync, isAutoSync, UpdateConfigSign);
                    frmSettingConfigSync.ShowDialog(this.ParentForm);
                }
                if (!isAutoSync && this.configSync != null && this.configSync.period > 0)
                {
                    isAutoSync = true;
                    btnAutoSync.Text = Resources.ResourceMessageLang.DangDongBo;
                    btnAutoSync.ToolTip = Resources.ResourceMessageLang.DangChayTienTrinhDongBoDuLieuXml130LenCongBHYT;
                    this.StartTimer();
                }
                else
                {
                    isAutoSync = false;
                    autoSync.Stop();
                    btnAutoSync.Text = Resources.ResourceMessageLang.DongBoTD;
                    btnAutoSync.ToolTip = Resources.ResourceMessageLang.DongBoTuDong;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
        private void StartTimer()
        {
            try
            {
                autoSync.Interval = (int)(configSync.period * 60000);
                autoSync.Enabled = true;
                this.autoSync_Tick(null, null);
                autoSync.Start();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void UpdateConfigSign(ConfigSyncADO config)
        {
            try
            {
                if (config != null)
                {
                    this.configSync = config;

                    string value = Newtonsoft.Json.JsonConvert.SerializeObject(configSync);
                    HIS.Desktop.Library.CacheClient.ControlStateRDO csAddOrUpdate = (this.currentControlStateRDO != null && this.currentControlStateRDO.Count > 0) ? this.currentControlStateRDO.Where(o => o.KEY == btnSettingConfigSync.Name && o.MODULE_LINK == moduleLink).FirstOrDefault() : null;
                    if (csAddOrUpdate != null)
                    {
                        csAddOrUpdate.VALUE = value;
                    }
                    else
                    {
                        csAddOrUpdate = new HIS.Desktop.Library.CacheClient.ControlStateRDO();
                        csAddOrUpdate.KEY = btnSettingConfigSync.Name;
                        csAddOrUpdate.VALUE = value;
                        csAddOrUpdate.MODULE_LINK = moduleLink;
                        if (this.currentControlStateRDO == null)
                            this.currentControlStateRDO = new List<HIS.Desktop.Library.CacheClient.ControlStateRDO>();
                        this.currentControlStateRDO.Add(csAddOrUpdate);
                    }
                    this.controlStateWorker.SetData(this.currentControlStateRDO);
                    WaitingManager.Hide();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void btnSettingConfigSync_Click(object sender, EventArgs e)
        {
            try
            {
                if (configSync == null)
                {
                    ConfigSyncADO tempConfigSync = new ConfigSyncADO();
                    tempConfigSync.branchIds = this.branchSelecteds.Select(o => o.ID).ToList();
                    tempConfigSync.patientTypeIds = this.patientTypeSelecteds.Select(o => o.ID).ToList(); ;
                    tempConfigSync.treatmentTypeIds = this.treatmentTypeSelecteds.Select(o => o.ID).ToList();
                    tempConfigSync.statusId = (int)cboStatus.EditValue;
                    tempConfigSync.period = 10;
                    frmSettingConfigSync frmSettingConfigSync = new frmSettingConfigSync(tempConfigSync, isAutoSync, UpdateConfigSign);
                    frmSettingConfigSync.ShowDialog(this.ParentForm);
                }
                else
                {
                    frmSettingConfigSync frmSettingConfigSync = new frmSettingConfigSync(configSync, isAutoSync, UpdateConfigSign);
                    frmSettingConfigSync.ShowDialog(this.ParentForm);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void autoSync_Tick(object sender, EventArgs e)
        {
            try
            {
                if (timerTickIsRunning)
                {
                    LogSystem.Info("Tien trinh tu dong dong bo dang chay. Khong cho phep khoi tao tien trinh khac");
                    return;
                }
                timerTickIsRunning = true;

                LogSystem.Info("Begin Run Thread Auto Sync");



                listTreatmentSync = this.GetTreatment();
                if (listTreatmentSync != null && listTreatmentSync.Count > 0)
                {
                    LogSystem.Info("Thread Auto Sync. TreatmentCount: " + listTreatmentSync.Count);
                    backgroundWorker1.RunWorkerAsync();
                }
                else
                {
                    LogSystem.Info("Khong co ho so dieu tri nao. Khong thuc hien tu dong dong bo");
                }
                LogSystem.Info("End Run Thread Auto Auto Sync");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            timerTickIsRunning = false;
        }
        private List<V_HIS_TREATMENT_1> GetTreatment()
        {
            List<V_HIS_TREATMENT_1> result = null;
            try
            {
                if (configSync != null)
                {
                    DateTime dt = DateTime.Now.AddMinutes(-(double)configSync.period);
                    HisTreatmentView1Filter filter = new HisTreatmentView1Filter();
                    filter.FEE_LOCK_TIME_TO = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                    filter.FEE_LOCK_TIME_FROM = Convert.ToInt64(dt.ToString("yyyyMMddHHmmss"));
                    if (configSync.branchIds != null && configSync.branchIds.Count > 0)
                        filter.BRANCH_IDs = configSync.branchIds;
                    if (configSync.patientTypeIds != null && configSync.patientTypeIds.Count > 0)
                        filter.TDL_PATIENT_TYPE_IDs = configSync.patientTypeIds;
                    if (configSync.treatmentTypeIds != null && configSync.treatmentTypeIds.Count > 0)
                        filter.TDL_TREATMENT_TYPE_IDs = configSync.treatmentTypeIds;
                    if (configSync.statusId != null)
                    {
                        if (configSync.statusId == 1)
                        {
                            filter.IS_LOCK_HEIN = true;
                        }
                        else if (configSync.statusId == 2)
                        {
                            filter.IS_PAUSE = true;
                        }
                        else if (configSync.statusId == 3)
                        {
                            filter.HAS_IN_CODE = true;
                        }
                    }
                    LogSystem.Debug("Treatment Filter: " + LogUtil.TraceData("Filter", filter));
                    result = new BackendAdapter(new CommonParam()).Get<List<V_HIS_TREATMENT_1>>("api/HisTreatment/GetView1", ApiConsumers.MosConsumer, filter, null);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        private async void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                List<Task> lst = new List<Task>();
                lst.Add(ProcessSyncTreatment(listTreatmentSync));
                Task.WaitAll(lst.ToArray());
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                Inventec.Common.Logging.LogSystem.Info("Xong");
                FillDataToGridTreatment();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private async void btnSync_Click(object sender, EventArgs e)
        {
            try
            {
                if (listSelection == null || listSelection.Count == 0)
                {
                    XtraMessageBox.Show(Resources.ResourceMessageLang.BanChuaChonHoSoDeDongBo, Resources.ResourceMessageLang.ThongBao);
                    return;
                }
                var listTreatmentSynced = listSelection.Where(o => o.XML130_RESULT == 2).ToList();
                if (listTreatmentSynced != null && listTreatmentSynced.Count > 0)
                {
                    if (XtraMessageBox.Show(String.Format(Resources.ResourceMessageLang.CacHoSoDaDongBoThanhCongBanCoMuonDongBoLai, String.Join(", ", listTreatmentSynced.Select(o => o.TREATMENT_CODE).ToList())), Resources.ResourceMessageLang.ThongBao, MessageBoxButtons.YesNo) == DialogResult.No)
                        return;
                }
                WaitingManager.Show();
                callSyncSuccess = false;
                await ProcessSyncTreatment(listSelection);
                if (callSyncSuccess)
                {
                    if (listMessageError != null && listMessageError.Count > 0)
                    {
                        if (paramUpdateXml130.Messages != null && paramUpdateXml130.Messages.Count > 0)
                        {
                            listMessageError.AddRange(paramUpdateXml130.Messages);
                        }
                        XtraMessageBox.Show(Resources.ResourceMessageLang.XuLyThatBai + String.Join("\r\n", listMessageError), Resources.ResourceMessageLang.ThongBao);
                    }
                    else if (paramUpdateXml130.Messages != null && paramUpdateXml130.Messages.Count > 0)
                    {
                        MessageManager.Show(this.ParentForm, paramUpdateXml130, false);
                    }
                    else
                        MessageManager.Show(this.ParentForm, paramUpdateXml130, true);

                    FillDataToGridTreatment();
                }
                WaitingManager.Hide();

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
        private async Task ProcessSyncTreatment(List<V_HIS_TREATMENT_1> listTreatmentSync)
        {
            try
            {
                listMessageError = new List<string>();
                paramUpdateXml130 = new CommonParam();
                string connect_infor = HisConfigCFG.QD_130_BYT__CONNECTION_INFO;
                string username = null, password = null, address = null;
                List<string> connectInfors = new List<string>();
                if (string.IsNullOrEmpty(connect_infor))
                {
                    XtraMessageBox.Show("01 - Lỗi cấu hình hệ thống");
                    return;
                }
                else
                {
                    connectInfors = connect_infor.Split('|').ToList();
                    if (connectInfors.Count != 3 || string.IsNullOrEmpty(connectInfors[0]) || string.IsNullOrEmpty(connectInfors[1]) || string.IsNullOrEmpty(connectInfors[2]))
                    {
                        XtraMessageBox.Show("01 - Lỗi cấu hình hệ thống");
                        return;
                    }
                }
                address = connectInfors[0];
                username = connectInfors[1];
                password = connectInfors[2];
                Dictionary<string, List<string>> DicErrorMess = new Dictionary<string, List<string>>();
                if (listTreatmentSync != null && listTreatmentSync.Count > 0)
                {
                    listTreatmentSync = listTreatmentSync.GroupBy(o => o.TREATMENT_CODE).Select(s => s.First()).ToList();

                    this.NewConfig = GetNewConfig();
                    int skip = 0;
                    while (listTreatmentSync.Count - skip > 0)
                    {
                        var limit = listTreatmentSync.Skip(skip).Take(GlobalVariables.MAX_REQUEST_LENGTH_PARAM).ToList();
                        skip = skip + GlobalVariables.MAX_REQUEST_LENGTH_PARAM;
                        #region
                        ListPatientTypeAlter = new List<V_HIS_PATIENT_TYPE_ALTER>();
                        ListSereServ = new List<V_HIS_SERE_SERV_2>();
                        ListEkipUser = new List<HIS_EKIP_USER>();
                        ListBedlog = new List<V_HIS_BED_LOG>();
                        HisTreatments = new List<V_HIS_TREATMENT_12>();
                        ListDhst = new List<HIS_DHST>();
                        HisTrackings = new List<HIS_TRACKING>();
                        HisSereServTeins = new List<V_HIS_SERE_SERV_TEIN>();
                        HisSereServPttts = new List<V_HIS_SERE_SERV_PTTT>();
                        ListDebates = new List<HIS_DEBATE>();
                        ListBaby = new List<V_HIS_BABY>();
                        ListMedicalAssessment = new List<V_HIS_MEDICAL_ASSESSMENT>();
                        ListHivTreatment = new List<HIS_HIV_TREATMENT>();
                        CreateThreadGetData(limit);
                        Dictionary<long, List<V_HIS_PATIENT_TYPE_ALTER>> dicPatientTypeAlter = new Dictionary<long, List<V_HIS_PATIENT_TYPE_ALTER>>();
                        Dictionary<long, List<V_HIS_SERE_SERV_2>> dicSereServ = new Dictionary<long, List<V_HIS_SERE_SERV_2>>();
                        Dictionary<long, List<V_HIS_SERE_SERV_TEIN>> dicSereServTein = new Dictionary<long, List<V_HIS_SERE_SERV_TEIN>>();
                        Dictionary<long, List<V_HIS_SERE_SERV_PTTT>> dicSereServPttt = new Dictionary<long, List<V_HIS_SERE_SERV_PTTT>>();
                        Dictionary<long, List<V_HIS_BED_LOG>> dicBedLog = new Dictionary<long, List<V_HIS_BED_LOG>>();
                        Dictionary<long, List<HIS_TRACKING>> dicTracking = new Dictionary<long, List<HIS_TRACKING>>();
                        Dictionary<long, List<HIS_EKIP_USER>> dicEkipUser = new Dictionary<long, List<HIS_EKIP_USER>>();
                        Dictionary<long, List<V_HIS_BABY>> dicBaby = new Dictionary<long, List<V_HIS_BABY>>();
                        Dictionary<long, List<HIS_DEBATE>> dicDebate = new Dictionary<long, List<HIS_DEBATE>>();
                        Dictionary<long, List<HIS_DHST>> dicDhstList = new Dictionary<long, List<HIS_DHST>>();
                        Dictionary<long, List<V_HIS_MEDICAL_ASSESSMENT>> dicMedicalAssessment = new Dictionary<long, List<V_HIS_MEDICAL_ASSESSMENT>>();
                        Dictionary<long, HIS_HIV_TREATMENT> dicHivTreatment = new Dictionary<long, HIS_HIV_TREATMENT>();

                        if (ListPatientTypeAlter != null && ListPatientTypeAlter.Count > 0)
                        {
                            foreach (var item in ListPatientTypeAlter)
                            {
                                if (!dicPatientTypeAlter.ContainsKey(item.TREATMENT_ID))
                                    dicPatientTypeAlter[item.TREATMENT_ID] = new List<V_HIS_PATIENT_TYPE_ALTER>();
                                dicPatientTypeAlter[item.TREATMENT_ID].Add(item);
                            }
                        }

                        if (ListSereServ != null && ListSereServ.Count > 0)
                        {
                            foreach (var sereServ in ListSereServ)
                            {
                                if (sereServ.TDL_HEIN_SERVICE_TYPE_ID.HasValue && sereServ.AMOUNT > 0 && sereServ.PRICE > 0 && sereServ.IS_EXPEND != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE && sereServ.IS_NO_EXECUTE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE && sereServ.TDL_TREATMENT_ID.HasValue)
                                {
                                    if (!dicSereServ.ContainsKey(sereServ.TDL_TREATMENT_ID.Value))
                                        dicSereServ[sereServ.TDL_TREATMENT_ID.Value] = new List<V_HIS_SERE_SERV_2>();
                                    dicSereServ[sereServ.TDL_TREATMENT_ID.Value].Add(sereServ);
                                }

                                if (sereServ.EKIP_ID.HasValue && ListEkipUser != null && ListEkipUser.Count > 0 && sereServ.TDL_TREATMENT_ID.HasValue)
                                {
                                    var ekips = ListEkipUser.Where(o => o.EKIP_ID == sereServ.EKIP_ID).ToList();
                                    if (ekips != null && ekips.Count > 0)
                                    {
                                        foreach (var item in ekips)
                                        {
                                            if (!dicEkipUser.ContainsKey(sereServ.TDL_TREATMENT_ID.Value))
                                                dicEkipUser[sereServ.TDL_TREATMENT_ID.Value] = new List<HIS_EKIP_USER>();

                                            dicEkipUser[sereServ.TDL_TREATMENT_ID.Value].Add(item);
                                        }
                                    }
                                }
                            }
                        }

                        if (HisSereServTeins != null && HisSereServTeins.Count > 0)
                        {
                            foreach (var ssTein in HisSereServTeins)
                            {
                                if (!ssTein.TDL_TREATMENT_ID.HasValue) continue;

                                if (!dicSereServTein.ContainsKey(ssTein.TDL_TREATMENT_ID.Value))
                                    dicSereServTein[ssTein.TDL_TREATMENT_ID.Value] = new List<V_HIS_SERE_SERV_TEIN>();

                                dicSereServTein[ssTein.TDL_TREATMENT_ID.Value].Add(ssTein);
                            }
                        }

                        if (HisTrackings != null && HisTrackings.Count > 0)
                        {
                            foreach (var tracking in HisTrackings)
                            {
                                if (!dicTracking.ContainsKey(tracking.TREATMENT_ID))
                                    dicTracking[tracking.TREATMENT_ID] = new List<HIS_TRACKING>();

                                dicTracking[tracking.TREATMENT_ID].Add(tracking);
                            }
                        }
                        if (ListBaby != null && ListBaby.Count > 0)
                        {
                            foreach (var baby in ListBaby)
                            {
                                if (!dicBaby.ContainsKey(baby.TREATMENT_ID))
                                    dicBaby[baby.TREATMENT_ID] = new List<V_HIS_BABY>();

                                dicBaby[baby.TREATMENT_ID].Add(baby);
                            }
                        }
                        if (ListHivTreatment != null && ListHivTreatment.Count > 0)
                        {
                            ListHivTreatment = ListHivTreatment.OrderBy(o => o.ID).ToList();
                            foreach (var hivTreatment in ListHivTreatment)
                            {
                                dicHivTreatment[hivTreatment.TREATMENT_ID] = hivTreatment;
                            }
                        }
                        if (HisSereServPttts != null && HisSereServPttts.Count > 0)
                        {
                            foreach (var ssPttt in HisSereServPttts)
                            {
                                if (!ssPttt.TDL_TREATMENT_ID.HasValue) continue;

                                if (!dicSereServPttt.ContainsKey(ssPttt.TDL_TREATMENT_ID.Value))
                                    dicSereServPttt[ssPttt.TDL_TREATMENT_ID.Value] = new List<V_HIS_SERE_SERV_PTTT>();

                                dicSereServPttt[ssPttt.TDL_TREATMENT_ID.Value].Add(ssPttt);
                            }
                        }

                        if (ListDhst != null && ListDhst.Count > 0)
                        {
                            foreach (var item in ListDhst)
                            {
                                if (!dicDhstList.ContainsKey(item.TREATMENT_ID))
                                    dicDhstList[item.TREATMENT_ID] = new List<HIS_DHST>();

                                dicDhstList[item.TREATMENT_ID].Add(item);
                            }
                        }

                        if (ListBedlog != null && ListBedlog.Count > 0)
                        {
                            foreach (var bed in ListBedlog)
                            {
                                if (!dicBedLog.ContainsKey(bed.TREATMENT_ID))
                                    dicBedLog[bed.TREATMENT_ID] = new List<V_HIS_BED_LOG>();

                                dicBedLog[bed.TREATMENT_ID].Add(bed);
                            }
                        }

                        if (ListDebates != null && ListDebates.Count > 0)
                        {
                            foreach (var item in ListDebates)
                            {
                                if (!dicDebate.ContainsKey(item.TREATMENT_ID))
                                    dicDebate[item.TREATMENT_ID] = new List<HIS_DEBATE>();

                                dicDebate[item.TREATMENT_ID].Add(item);
                            }
                        }
                        if (ListMedicalAssessment != null && ListMedicalAssessment.Count > 0)
                        {
                            foreach (var item in ListMedicalAssessment)
                            {
                                if (!dicMedicalAssessment.ContainsKey(item.TREATMENT_ID))
                                    dicMedicalAssessment[item.TREATMENT_ID] = new List<V_HIS_MEDICAL_ASSESSMENT>();

                                dicMedicalAssessment[item.TREATMENT_ID].Add(item);
                            }
                        }
                        #endregion
                        foreach (var treatment in HisTreatments)
                        {
                            #region
                            InputADO ado = new InputADO();
                            ado.Treatment = treatment;
                            if (dicPatientTypeAlter.ContainsKey(treatment.ID))
                            {
                                ado.ListPatientTypeAlter = dicPatientTypeAlter[treatment.ID];
                            }

                            if (!dicSereServ.ContainsKey(treatment.ID))
                            {
                                var errorSereServ = "Hồ sơ không có dịch vụ";
                                if (!DicErrorMess.ContainsKey(errorSereServ))
                                {
                                    DicErrorMess[errorSereServ] = new List<string>();
                                }

                                DicErrorMess[errorSereServ].Add(treatment.TREATMENT_CODE);
                                continue;
                            }

                            ado.ListSereServ = dicSereServ.ContainsKey(treatment.ID) ? dicSereServ[treatment.ID] : null;

                            if (dicDhstList.ContainsKey(treatment.ID))
                            {
                                ado.ListDhst = dicDhstList[treatment.ID];
                            }

                            if (dicSereServTein.ContainsKey(treatment.ID))
                            {
                                ado.ListSereServTein = dicSereServTein[treatment.ID];
                            }

                            if (dicSereServPttt.ContainsKey(treatment.ID))
                            {
                                ado.ListSereServPttt = dicSereServPttt[treatment.ID];
                            }

                            if (dicBedLog.ContainsKey(treatment.ID))
                            {
                                ado.ListBedLog = dicBedLog[treatment.ID];
                            }

                            if (dicTracking.ContainsKey(treatment.ID))
                            {
                                ado.ListTracking = dicTracking[treatment.ID];
                            }

                            if (dicEkipUser.ContainsKey(treatment.ID))
                            {
                                ado.ListEkipUser = dicEkipUser[treatment.ID].Distinct().ToList();
                            }

                            if (dicDebate.ContainsKey(treatment.ID))
                            {
                                ado.ListDebate = dicDebate[treatment.ID];
                            }

                            if (dicBaby.ContainsKey(treatment.ID))
                            {
                                ado.ListBaby = dicBaby[treatment.ID];
                            }
                            if (dicMedicalAssessment.ContainsKey(treatment.ID))
                            {
                                ado.ListMedicalAssessment = dicMedicalAssessment[treatment.ID];
                            }
                            if (dicHivTreatment.ContainsKey(treatment.ID))
                            {
                                ado.HivTreatment = dicHivTreatment[treatment.ID];
                            }
                            ado.TotalMaterialTypeData = BackendDataWorker.Get<HIS_MATERIAL_TYPE>();
                            ado.TotalHeinMediOrgData = BackendDataWorker.Get<HIS_MEDI_ORG>();
                            ado.TotalConfigData = NewConfig;
                            ado.TotalPatientTypeData = BackendDataWorker.Get<HIS_PATIENT_TYPE>();
                            ado.TotalIcdData = BackendDataWorker.Get<HIS_ICD>();
                            ado.TotalSericeData = BackendDataWorker.Get<V_HIS_SERVICE>();
                            ado.TotalEmployeeData = BackendDataWorker.Get<HIS_EMPLOYEE>();
                            ado.serverInfo = new ServerInfo() { Username = username, Password = password, Address = address };
                            #endregion
                            His.Bhyt.ExportXml.XML130.CreateXmlProcessor xmlProcessor = new His.Bhyt.ExportXml.XML130.CreateXmlProcessor(ado);
                            SyncResultADO syncResult = await xmlProcessor.SyncData();
                            Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => syncResult), syncResult));
                            if (syncResult != null)
                            {
                                string errorCode = syncResult.ErrorCode;
                                if (errorCode == "01" || errorCode == "02" || errorCode == "03")
                                {
                                    XtraMessageBox.Show(String.Format("{0} - {1}", errorCode, syncResult.Message), Resources.ResourceMessageLang.ThongBao);
                                    autoSync.Stop();
                                    isAutoSync = false;
                                    return;
                                }
                                else
                                {
                                    callSyncSuccess = true;
                                    if (!syncResult.Success)
                                    {
                                        listMessageError.Add(String.Format("{0}: {1} - {2}", treatment.TREATMENT_CODE, syncResult.ErrorCode, syncResult.Message));
                                    }
                                    HisTreatmentXmlResultSDO xmlResultSDO = new HisTreatmentXmlResultSDO();
                                    xmlResultSDO.TreatmentId = treatment.ID;
                                    xmlResultSDO.XmlResult = syncResult.Success ? 2 : 1;
                                    xmlResultSDO.Description = syncResult.Message;
                                    xmlResultSDO.CheckCode = syncResult.CheckCode;
                                    Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => xmlResultSDO), xmlResultSDO));
                                    var rs = new Inventec.Common.Adapter.BackendAdapter(paramUpdateXml130).Post<bool>("api/HisTreatment/UpdateXml130Info", ApiConsumers.MosConsumer, xmlResultSDO, paramUpdateXml130);

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
