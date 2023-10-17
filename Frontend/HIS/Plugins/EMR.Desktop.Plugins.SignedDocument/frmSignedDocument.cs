using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.LocalStorage.ConfigApplication;
using HIS.Desktop.LocalStorage.Location;
using System.Configuration;
using Inventec.Desktop.Common.Modules;
using Inventec.Core;
using EMR.EFMODEL.DataModels;
using EMR.Filter;
using HIS.Desktop.Common;
using Inventec.Common.Adapter;
using System.Collections;
using DevExpress.XtraGrid.Views.Base;
using Inventec.Fss.Client;
using HIS.Desktop.ADO;
using Inventec.Desktop.Common.LanguageManager;
using System.IO;
using DevExpress.XtraEditors;
using Inventec.Common.SignLibrary;
using HIS.Desktop.Plugins.Library.EmrGenerate;
using HIS.Desktop.LocalStorage.LocalData;

namespace EMR.Desktop.Plugins.SignedDocument
{
    public partial class frmSignedDocument : HIS.Desktop.Utility.FormBase
    {
        #region Declare
        Module currentModule;
        string hisCode = null;
        string treatmentCode = null;
        EmrDocumentInfoADO emrAdo;
        #endregion

        public frmSignedDocument(Module _Module, EmrDocumentInfoADO _EmrAdo)
            : base(_Module)
        {
            InitializeComponent();
            this.emrAdo = _EmrAdo;
            this.currentModule = _Module;
            this.hisCode = _EmrAdo.HisCode;
            this.treatmentCode = _EmrAdo.TreatmentCode;
            try
            {
                this.Icon = Icon.ExtractAssociatedIcon(System.IO.Path.Combine(ApplicationStoreLocation.ApplicationStartupPath, ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]));

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void frmSignedDocument_Load(object sender, EventArgs e)
        {
            try
            {
                SetDefaultLanguage();
                FillDataGrid();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
        private void SetDefaultLanguage()
        {
            try
            {
                //HIS.Desktop.Plugins.SignedDocument.Resources.ResourceLanguageManager.LanguageResource = new System.Resources.ResourceManager("HIS.Desktop.Plugins.SignedDocument.Resources.Lang", typeof(frmSignedDocument).Assembly);
                //this.gridColumn1.Caption = Inventec.Common.Resource.Get.Value("IVT_LANGUAGE_KEY__FRM_APPROVESERVICEREQCLS__RIGHT_ROIVT_LANGUAGE_KEY__FRM_NAME", HIS.Desktop.Plugins.SignedDocument.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
            }
            catch (Exception ex)
            {

                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
        private void FillDataGrid()
        {
            try
            {
                CommonParam param = new CommonParam();
                EmrDocumentViewFilter filter = new EmrDocumentViewFilter();
                filter.HAS_SIGNERS = true;
                filter.IS_REJECTER_NOT_NULL = false;
                filter.IS_DELETE = false;
                filter.TREATMENT_CODE__EXACT = treatmentCode;
                gridView1.BeginUpdate();
                Inventec.Common.Logging.LogSystem.Debug("____________" +
                      hisCode + " __ " + treatmentCode);
                var apiResult = new BackendAdapter(param).Get<List<V_EMR_DOCUMENT>>("api/EmrDocument/GetView", ApiConsumers.EmrConsumer, filter, null);
                if (apiResult != null)
                {
                    var data = apiResult.Where(o => o.HIS_CODE != null && o.HIS_CODE.Contains(hisCode)).ToList();
                    Inventec.Common.Logging.LogSystem.Debug("____________" +
                        hisCode + " __ " + treatmentCode
                        + "___ DANH SÁCH VĂN BẢN ĐÃ KÝ:" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data));

                    if (data != null)
                    {
                        gridView1.GridControl.DataSource = data;
                    }
                    else
                    {
                        gridView1.GridControl.DataSource = null;
                    }
                }
                gridView1.EndUpdate();

            }
            catch (Exception ex)
            {

                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void gridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            try
            {
                if (e.IsGetData)
                {
                    var data = (V_EMR_DOCUMENT)((IList)((BaseView)sender).DataSource)[e.ListSourceRowIndex];
                    Inventec.Common.Logging.LogSystem.Debug("____________####" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data));


                    if (e.Column.FieldName == "DOCUMENT_NAME_STR")
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(data.DOCUMENT_NAME))
                            {
                                e.Value = data.DOCUMENT_NAME;
                            }
                            else
                            {
                                e.Value = "";
                            }
                        }
                        catch (Exception ex)
                        {
                            Inventec.Common.Logging.LogSystem.Warn("Loi set gia tri cho cot ngay tao DOCUMENT_NAME", ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    var rowData = (V_EMR_DOCUMENT)gridView1.GetFocusedRow();
                    if (rowData != null)
                    {
                        ChangedDataRow(rowData);

                        SetFocusEditor();
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
        private void ChangedDataRow(V_EMR_DOCUMENT data)
        {
            if (data != null)
            {
                LoadPdfViewer(data);
            }
        }
        private void SetFocusEditor()
        {
            try
            {
                //TODO

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Debug(ex);
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
        }
        private void FillDataToEditorControl(V_EMR_DOCUMENT data)
        {
            try
            {
                List<V_EMR_DOCUMENT> listEmr = new List<V_EMR_DOCUMENT>();
                listEmr.Add(data);
                this.ucViewEmrDocument1.ShowBar = true;
                this.ucViewEmrDocument1.ReloadDocument(listEmr);

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            try
            {

                var rowData = (V_EMR_DOCUMENT)gridView1.GetFocusedRow();
                if (rowData != null)
                {
                    ChangedDataRow(rowData);

                    SetFocusEditor();
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
        private void LoadPdfViewer(V_EMR_DOCUMENT data)
        {
            try
            {
                SignLibraryGUIProcessor libraryProcessor = new SignLibraryGUIProcessor();
                Inventec.Common.SignLibrary.ADO.InputADO inputADO = new EmrGenerateProcessor().GenerateInputADO(data.TREATMENT_CODE, data.DOCUMENT_CODE, data.DOCUMENT_NAME, currentModule.RoomId);

                inputADO.IsSign = false;
                inputADO.IsSave = false;
                inputADO.IsPrint = false;
                inputADO.IsExport = false;

                if (data.WIDTH != null && data.HEIGHT != null && data.RAW_KIND != null)
                {
                    inputADO.PaperSizeDefault = new System.Drawing.Printing.PaperSize(data.PAPER_NAME, (int)data.WIDTH, (int)data.HEIGHT);
                    if (data.RAW_KIND != null)
                    {
                        inputADO.PaperSizeDefault.RawKind = (int)data.RAW_KIND;
                    }
                }
                Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => inputADO.PaperSizeDefault), inputADO.PaperSizeDefault));

                CommonParam paramCommon = new CommonParam();
                EmrVersionFilter filter = new EmrVersionFilter();
                filter.DOCUMENT_ID = data.ID;
                filter.ORDER_DIRECTION = "DESC";
                filter.ORDER_FIELD = "ID";
                List<EMR_VERSION> apiResult = new BackendAdapter(paramCommon).Get<List<EMR_VERSION>>("api/EmrVersion/Get", ApiConsumers.EmrConsumer, filter, paramCommon);
                if (apiResult != null && apiResult.Count > 0)
                {
                    Inventec.Common.Logging.LogSystem.Info("apiResult.FirstOrDefault().URL: " + apiResult.FirstOrDefault().URL);
                    var stream = Inventec.Fss.Client.FileDownload.GetFile(apiResult.FirstOrDefault().URL);
                    byte[] b;

                    using (BinaryReader br = new BinaryReader(stream))
                    {
                        b = br.ReadBytes((int)stream.Length);
                    }
                    string base64FileContent = Convert.ToBase64String(b);

                    var uc = libraryProcessor.GetUC(base64FileContent, FileType.Pdf, inputADO);
                    if (uc != null)
                    {
                        uc.Dock = DockStyle.Fill;
                        this.ucViewEmrDocument1.Controls.Clear();
                        this.ucViewEmrDocument1.Controls.Add(uc);

                        string message = "Xem văn bản. Mã văn bản: " + data.DOCUMENT_CODE + ", TREATMENT_CODE: " + data.TREATMENT_CODE + ". Thời gian xem: " + Inventec.Common.DateTime.Convert.SystemDateTimeToTimeSeparateString(DateTime.Now) + ". Người xem: " + Inventec.UC.Login.Base.ClientTokenManagerStore.ClientTokenManager.GetLoginName();
                        His.EventLog.Logger.Log(GlobalVariables.APPLICATION_CODE, Inventec.UC.Login.Base.ClientTokenManagerStore.ClientTokenManager.GetLoginName(), message, Inventec.UC.Login.Base.ClientTokenManagerStore.ClientTokenManager.GetLoginAddress());
                    }
                    else
                    {
                        this.ucViewEmrDocument1.Controls.Clear();
                    }
                }
                else
                {
                    this.ucViewEmrDocument1.Controls.Clear();
                }

            }
            catch (Exception ex)
            {
                this.ucViewEmrDocument1.Controls.Clear();
                this.ucViewEmrDocument1 = new HIS.UC.ViewEmrDocument.UcEmrDocument.UcViewEmrDocument();
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
