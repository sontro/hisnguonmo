using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using HIS.Desktop.Utility;
//using HIS.Desktop.Plugins.Library.EmrGenerate;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.LocalStorage.LocalData;

using HIS.Desktop.Plugins.PatientDocumentIssued;

using Inventec.Common.Adapter;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary;
using Inventec.Core;
using Inventec.Desktop.Common.Message;

using EMR.EFMODEL.DataModels;
using EMR.Filter;

namespace HIS.Desktop.Plugins.PatientDocumentIssued.Form
{
    public partial class frmViewEmrDocument : FormBase
    {
        V_EMR_DOCUMENT documentData;
        bool IsSigning = false;

        public frmViewEmrDocument(V_EMR_DOCUMENT _documentData)
        {
            this.documentData = _documentData;
            InitializeComponent();
        }

        private void frmViewEmrDocument_Load(object sender, EventArgs e)
        {
            LoadPdfViewer(this.documentData);
        }

        private void LoadPdfViewer(V_EMR_DOCUMENT data)
        {
            try
            {
                SignLibraryGUIProcessor libraryProcessor = new SignLibraryGUIProcessor();
                Inventec.Common.SignLibrary.ADO.InputADO inputADO = new HIS.Desktop.Plugins.Library.EmrGenerate.EmrGenerateProcessor().GenerateInputADO(data.TREATMENT_CODE, data.DOCUMENT_CODE, data.DOCUMENT_NAME, 0);


                if (this.IsSigning && data.NEXT_SIGNER == Inventec.UC.Login.Base.ClientTokenManagerStore.ClientTokenManager.GetLoginName())
                {
                    inputADO.IsSign = true;
                }
                else
                    inputADO.IsSign = false;

                inputADO.IsSave = false;
                inputADO.IsPrint = false;
                inputADO.IsExport = false;
                //inputADO.RoomCode = room != null ? room.ROOM_CODE : "";
                //inputADO.RoomTypeCode = room != null ? room.ROOM_TYPE_CODE : "";
                //inputADO.RoomName = room != null ? room.ROOM_NAME : "";
                inputADO.ActPrintSuccess = UpdateIsPatientIsssued;

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
                        this.panel1.Controls.Clear();
                        this.panel1.Controls.Add(uc);

                        string message = "Xem văn bản. Mã văn bản: " + data.DOCUMENT_CODE + ", TREATMENT_CODE: " + data.TREATMENT_CODE + ". Thời gian xem: " + Inventec.Common.DateTime.Convert.SystemDateTimeToTimeSeparateString(DateTime.Now) + ". Người xem: " + Inventec.UC.Login.Base.ClientTokenManagerStore.ClientTokenManager.GetLoginName();
                        His.EventLog.Logger.Log(GlobalVariables.APPLICATION_CODE, Inventec.UC.Login.Base.ClientTokenManagerStore.ClientTokenManager.GetLoginName(), message, Inventec.UC.Login.Base.ClientTokenManagerStore.ClientTokenManager.GetLoginAddress());
                    }
                    else
                    {
                        this.panel1.Controls.Clear();
                    }
                }
                else
                {
                    this.panel1.Controls.Clear();
                }

            }
            catch (Exception ex)
            {
                this.panel1.Controls.Clear();
                this.panel1 = new Panel();
                WaitingManager.Hide();
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void UpdateIsPatientIsssued()
        {
            try
            {
                WaitingManager.Show();
                CommonParam param = new CommonParam();
                LogSystem.Debug(documentData.ID.ToString());
                long idEmrDpcument = documentData.ID;
                var apiResult = new BackendAdapter(param).Post<EMR_DOCUMENT>(RequestUriStore.EMR_DOCUMENT_ISSUED_UPDATE, ApiConsumers.EmrConsumer, idEmrDpcument, param);
                WaitingManager.Hide();
            }
            catch (Exception ex)
            {
                WaitingManager.Hide();
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
