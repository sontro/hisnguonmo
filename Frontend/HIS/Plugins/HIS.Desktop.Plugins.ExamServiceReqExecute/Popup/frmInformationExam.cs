using DevExpress.Utils.Menu;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using HIS.Desktop.LocalStorage.ConfigApplication;
using HIS.Desktop.Plugins.ExamServiceReqExecute.Config;
using HIS.Desktop.Utility;
using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.ExamServiceReqExecute.Popup
{
    public partial class frmInformationExam : FormBase
    {
        Action<HisServiceReqExamUpdateSDO> ActionSendSDO { get; set; }
        HisServiceReqExamUpdateSDO examServiceReqUpdateSDO { get; set; }
        V_HIS_SERVICE_REQ HisServiceReqView { get; set; }
        int indexTabPageSelected { get; set; }
        public frmInformationExam(V_HIS_SERVICE_REQ HisServiceReqView, HisServiceReqExamUpdateSDO examServiceReqUpdateSDO, Action<HisServiceReqExamUpdateSDO> ActionSendSDO,int tabPageSelected)
        {
            InitializeComponent();
            try
            {
                string iconPath = System.IO.Path.Combine(HIS.Desktop.LocalStorage.Location.ApplicationStoreLocation.ApplicationStartupPath, System.Configuration.ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]);
                this.Icon = Icon.ExtractAssociatedIcon(iconPath);
                this.ActionSendSDO = ActionSendSDO;
                this.examServiceReqUpdateSDO = examServiceReqUpdateSDO;
                this.HisServiceReqView = HisServiceReqView;
                this.indexTabPageSelected = tabPageSelected;
            }
            catch (Exception ex)
            {

                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private const int CP_NOCLOSE_BUTTON = 0x200;
        private const int WS_CAPTION = 0x00C00000;

        // Removes the close button in the caption bar
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;

                // This disables the close button
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;

                // this appears to completely remove the close button
                //myCp.Style &= WS_CAPTION;

                return myCp;
            }
        }

        private void EnableViaKeyDisablePartExamByExecutor()
        {
            try
            {
                var loginName = Inventec.UC.Login.Base.ClientTokenManagerStore.ClientTokenManager.GetLoginName();
                if (HisConfigCFG.isDisablePartExamByExecutor)
                {

                    this.txtKhamBoPhan.Enabled = string.IsNullOrEmpty(HisServiceReqView.PAEX_LOGINNAME) || HisServiceReqView.PAEX_LOGINNAME == loginName;
                    this.txtTuanHoan.Enabled = string.IsNullOrEmpty(HisServiceReqView.PAEX_CIRC_LOGINNAME) || HisServiceReqView.PAEX_CIRC_LOGINNAME == loginName;
                    this.txtHoHap.Enabled = string.IsNullOrEmpty(HisServiceReqView.PAEX_RESP_LOGINNAME) || HisServiceReqView.PAEX_RESP_LOGINNAME == loginName;
                    this.txtTieuHoa.Enabled = string.IsNullOrEmpty(HisServiceReqView.PAEX_DIGE_LOGINNAME) || HisServiceReqView.PAEX_DIGE_LOGINNAME == loginName;
                    this.txtThanTietNieu.Enabled = string.IsNullOrEmpty(HisServiceReqView.PAEX_KIDN_LOGINNAME) || HisServiceReqView.PAEX_KIDN_LOGINNAME == loginName;
                    this.txtThanKinh.Enabled = string.IsNullOrEmpty(HisServiceReqView.PAEX_NEUR_LOGINNAME) || HisServiceReqView.PAEX_NEUR_LOGINNAME == loginName;
                    this.txtCoXuongKhop.Enabled = string.IsNullOrEmpty(HisServiceReqView.PAEX_MUSC_LOGINNAME) || HisServiceReqView.PAEX_MUSC_LOGINNAME == loginName;
                    txtTai.Enabled = txtMui.Enabled = txtHong.Enabled = txtPART_EXAM_EAR_RIGHT_NORMAL.Enabled = txtPART_EXAM_EAR_RIGHT_WHISPER.Enabled = txtPART_EXAM_EAR_LEFT_NORMAL.Enabled = txtPART_EXAM_EAR_LEFT_WHISPER.Enabled = string.IsNullOrEmpty(HisServiceReqView.PAEX_ENT_LOGINNAME) || HisServiceReqView.PAEX_ENT_LOGINNAME == loginName;
                    txtPART_EXAM_UPPER_JAW.Enabled = txtPART_EXAM_LOWER_JAW.Enabled = txtRHM.Enabled = string.IsNullOrEmpty(HisServiceReqView.PAEX_STOM_LOGINNAME) || HisServiceReqView.PAEX_STOM_LOGINNAME == loginName;
                    txtMat.Enabled = chkPART_EXAM_EYE_BLIND_COLOR__BT.Enabled = chkPART_EXAM_EYE_BLIND_COLOR__MMTB.Enabled = chkPART_EXAM_EYE_BLIND_COLOR__MMD.Enabled = chkPART_EXAM_EYE_BLIND_COLOR__MMXLC.Enabled = chkPART_EXAM_EYE_BLIND_COLOR__MMV.Enabled = txtNhanApPhai.Enabled = txtThiLucKhongKinhPhai.Enabled = txtThiLucCoKinhPhai.Enabled = txtNhanApTrai.Enabled = txtThiLucKhongKinhTrai.Enabled = txtThiLucCoKinhTrai.Enabled = chkPART_EXAM_HORIZONTAL_SIGHT__BT.Enabled = chkPART_EXAM_HORIZONTAL_SIGHT__HC.Enabled = chkPART_EXAM_VERTICAL_SIGHT__BT.Enabled = chkPART_EXAM_VERTICAL_SIGHT__HC.Enabled = string.IsNullOrEmpty(HisServiceReqView.PAEX_EYE_LOGINNAME) || HisServiceReqView.PAEX_EYE_LOGINNAME == loginName;
                    this.txtNoiTiet.Enabled = string.IsNullOrEmpty(HisServiceReqView.PAEX_OEND_LOGINNAME) || HisServiceReqView.PAEX_OEND_LOGINNAME == loginName;
                    this.txtPartExamMental.Enabled = string.IsNullOrEmpty(HisServiceReqView.PAEX_MENT_LOGINNAME) || HisServiceReqView.PAEX_MENT_LOGINNAME == loginName;
                    this.txtPartExamNutrition.Enabled = string.IsNullOrEmpty(HisServiceReqView.PAEX_NUTR_LOGINNAME) || HisServiceReqView.PAEX_NUTR_LOGINNAME == loginName;
                    this.txtPartExamMotion.Enabled = string.IsNullOrEmpty(HisServiceReqView.PAEX_MOTI_LOGINNAME) || HisServiceReqView.PAEX_MOTI_LOGINNAME == loginName;
                    this.txtPartExanObstetric.Enabled = string.IsNullOrEmpty(HisServiceReqView.PAEX_OBST_LOGINNAME) || HisServiceReqView.PAEX_OBST_LOGINNAME == loginName;
                    this.txtDaLieu.Enabled = string.IsNullOrEmpty(HisServiceReqView.PAEX_DERM_LOGINNAME) || HisServiceReqView.PAEX_DERM_LOGINNAME == loginName;
                    //}
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void SetTabPageVisible(XtraTabControl tab)
        {
            try
            {
                long tabNum = ConfigApplicationWorker.Get<long>(AppConfigKeys.CONFIG_KEY__EXAM_SERVICE_REQ_EXCUTE__TAB_INFO_SHOW_DEFAULT);
                XtraTabPage activeTab = new XtraTabPage();
                switch (tabNum)
                {
                    case 1:
                        activeTab = xtraTabPageChung;
                        break;
                    case 2:
                        activeTab = xtraTabTuanHoan;
                        break;
                    case 3:
                        activeTab = xtraTabHoHap;
                        break;
                    case 4:
                        activeTab = xtraTabTieuHoa;
                        break;
                    case 5:
                        activeTab = xtraTabThanTietNieu;
                        break;
                    case 6:
                        activeTab = xtraTabThanKinh;
                        break;
                    case 7:
                        activeTab = xtraTabCoXuongKhop;
                        break;
                    case 8:
                        activeTab = xtraTabTaiMuiHong;
                        break;
                    case 9:
                        activeTab = xtraTabRangHamMat;
                        break;
                    case 10:
                        activeTab = xtraTabMat;
                        break;
                    case 11:
                        activeTab = xtraTabNoiTiet;
                        break;
                    case 12:
                        activeTab = xtraTabTamThan;
                        break;
                    case 13:
                        activeTab = xtraTabDinhDuong;
                        break;
                    case 14:
                        activeTab = xtraTabVanDong;
                        break;
                    case 15:
                        activeTab = xtraTabSanPhuKhoa;
                        break;
                    case 16:
                        activeTab = xtraTabDaLieu;
                        break;
                    default:
                        activeTab = null;
                        break;
                }

                long key = ConfigApplicationWorker.Get<long>(AppConfigKeys.CONFIG_KEY__EXAM_SERVICE_REQ_EXCUTE_HIDE_TABS_INFOMATION__APPLICATION);
                if (key == 1)
                {
                    for (int i = 0; i < tab.TabPages.Count; i++)
                    {
                        tab.TabPages[i].PageVisible = false;
                    }
                }

                activeTab.PageVisible = true;
                tab.SelectedTabPage = activeTab;

                //Gán boder cho layout control trong tab page mắt vùng thông tin thị trường
                //lcForTabpageMat__ThiTruong
                lcForTabpageMat__ThiTruong.OptionsView.ShareLookAndFeelWithChildren = false;
                lcForTabpageMat__ThiTruong.LookAndFeel.SetFlatStyle();
                lcForTabpageMat__ThiTruong.OptionsView.DrawItemBorders = true;
                lcForTabpageMat__ThiTruong.OptionsView.ItemBorderColor = System.Drawing.Color.Black;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                examServiceReqUpdateSDO = new HisServiceReqExamUpdateSDO();
                examServiceReqUpdateSDO.PartExam = !string.IsNullOrEmpty(txtKhamBoPhan.Text.Trim()) ? txtKhamBoPhan.Text.Trim() : null;
                examServiceReqUpdateSDO.PartExamCirculation = !string.IsNullOrEmpty(txtTuanHoan.Text.Trim()) ? txtTuanHoan.Text.Trim() : null;
                examServiceReqUpdateSDO.PartExamDigestion = !string.IsNullOrEmpty(txtTieuHoa.Text.Trim()) ? txtTieuHoa.Text.Trim() : null;
                examServiceReqUpdateSDO.PartExamEar = !string.IsNullOrEmpty(txtTai.Text.Trim()) ? txtTai.Text.Trim() : null;//TODO
                                                                                                              //TODO
                examServiceReqUpdateSDO.PartExamEarRightNormal = !string.IsNullOrEmpty(txtPART_EXAM_EAR_RIGHT_NORMAL.Text.Trim()) ? txtPART_EXAM_EAR_RIGHT_NORMAL.Text.Trim() : null;
                examServiceReqUpdateSDO.PartExamEarRightWhisper = !string.IsNullOrEmpty(txtPART_EXAM_EAR_RIGHT_WHISPER.Text.Trim()) ? txtPART_EXAM_EAR_RIGHT_WHISPER.Text.Trim() : null;
                examServiceReqUpdateSDO.PartExamEarLeftNormal = !string.IsNullOrEmpty(txtPART_EXAM_EAR_LEFT_NORMAL.Text.Trim()) ? txtPART_EXAM_EAR_LEFT_NORMAL.Text.Trim() : null;
                examServiceReqUpdateSDO.PartExamEarLeftWhisper = !string.IsNullOrEmpty(txtPART_EXAM_EAR_LEFT_WHISPER.Text.Trim()) ? txtPART_EXAM_EAR_LEFT_WHISPER.Text.Trim() : null;

                if (chkPART_EXAM_HORIZONTAL_SIGHT__BT.Checked)
                    examServiceReqUpdateSDO.PartExamHorizontalSight = 1;
                else if (chkPART_EXAM_HORIZONTAL_SIGHT__HC.Checked)
                    examServiceReqUpdateSDO.PartExamHorizontalSight = 2;
                else
                {
                    examServiceReqUpdateSDO.PartExamHorizontalSight = null;
                }

                if (chkPART_EXAM_VERTICAL_SIGHT__BT.Checked)
                    examServiceReqUpdateSDO.PartExamVerticalSight = 1;
                else if (chkPART_EXAM_VERTICAL_SIGHT__HC.Checked)
                    examServiceReqUpdateSDO.PartExamVerticalSight = 2;
                else
                {
                    examServiceReqUpdateSDO.PartExamVerticalSight = null;
                }

                if (chkPART_EXAM_EYE_BLIND_COLOR__BT.Checked)
                    examServiceReqUpdateSDO.PartExamEyeBlindColor = 1;
                else if (chkPART_EXAM_EYE_BLIND_COLOR__MMTB.Checked)
                    examServiceReqUpdateSDO.PartExamEyeBlindColor = 2;
                else
                {
                    if (chkPART_EXAM_EYE_BLIND_COLOR__MMD.Checked && chkPART_EXAM_EYE_BLIND_COLOR__MMXLC.Checked && chkPART_EXAM_EYE_BLIND_COLOR__MMV.Checked)
                        examServiceReqUpdateSDO.PartExamEyeBlindColor = 9;
                    else if (chkPART_EXAM_EYE_BLIND_COLOR__MMXLC.Checked && chkPART_EXAM_EYE_BLIND_COLOR__MMV.Checked)
                        examServiceReqUpdateSDO.PartExamEyeBlindColor = 8;
                    else if (chkPART_EXAM_EYE_BLIND_COLOR__MMD.Checked && chkPART_EXAM_EYE_BLIND_COLOR__MMV.Checked)
                        examServiceReqUpdateSDO.PartExamEyeBlindColor = 7;
                    else if (chkPART_EXAM_EYE_BLIND_COLOR__MMD.Checked && chkPART_EXAM_EYE_BLIND_COLOR__MMXLC.Checked)
                        examServiceReqUpdateSDO.PartExamEyeBlindColor = 6;
                    else if (chkPART_EXAM_EYE_BLIND_COLOR__MMV.Checked)
                        examServiceReqUpdateSDO.PartExamEyeBlindColor = 5;
                    else if (chkPART_EXAM_EYE_BLIND_COLOR__MMXLC.Checked)
                        examServiceReqUpdateSDO.PartExamEyeBlindColor = 4;
                    else if (chkPART_EXAM_EYE_BLIND_COLOR__MMD.Checked)
                        examServiceReqUpdateSDO.PartExamEyeBlindColor = 3;
                    else
                        examServiceReqUpdateSDO.PartExamEyeBlindColor = null;
                }

                examServiceReqUpdateSDO.PartExamEye = !string.IsNullOrEmpty(txtMat.Text.Trim()) ? txtMat.Text.Trim() : null;

                examServiceReqUpdateSDO.PartExamUpperJaw = !string.IsNullOrEmpty(txtPART_EXAM_UPPER_JAW.Text.Trim()) ? txtPART_EXAM_UPPER_JAW.Text.Trim() : null;
                examServiceReqUpdateSDO.PartExamLowerJaw = !string.IsNullOrEmpty(txtPART_EXAM_LOWER_JAW.Text.Trim()) ? txtPART_EXAM_LOWER_JAW.Text.Trim() : null;
                examServiceReqUpdateSDO.PartExamStomatology = !string.IsNullOrEmpty(txtRHM.Text.Trim()) ? txtRHM.Text.Trim() : null;

                examServiceReqUpdateSDO.PartExamNose = !string.IsNullOrEmpty(txtMui.Text.Trim()) ? txtMui.Text.Trim() : null;
                examServiceReqUpdateSDO.PartExamThroat = !string.IsNullOrEmpty(txtHong.Text.Trim()) ? txtHong.Text.Trim() : null;
                examServiceReqUpdateSDO.PartExamEyeTensionRight = !string.IsNullOrEmpty(txtNhanApPhai.Text.Trim()) ? txtNhanApPhai.Text.Trim() : null;
                examServiceReqUpdateSDO.PartExamEyeTensionLeft = !string.IsNullOrEmpty(txtNhanApTrai.Text.Trim()) ? txtNhanApTrai.Text.Trim() : null;
                examServiceReqUpdateSDO.PartExamEyeSightRight = !string.IsNullOrEmpty(txtThiLucKhongKinhPhai.Text.Trim()) ? txtThiLucKhongKinhPhai.Text.Trim() : null;
                examServiceReqUpdateSDO.PartExamEyeSightLeft = !string.IsNullOrEmpty(txtThiLucKhongKinhTrai.Text.Trim()) ? txtThiLucKhongKinhTrai.Text.Trim() : null;
                examServiceReqUpdateSDO.PartExamEyeSightGlassRight = !string.IsNullOrEmpty(txtThiLucCoKinhPhai.Text.Trim()) ? txtThiLucCoKinhPhai.Text.Trim() : null;
                examServiceReqUpdateSDO.PartExamEyeSightGlassLeft = !string.IsNullOrEmpty(txtThiLucCoKinhTrai.Text.Trim()) ? txtThiLucCoKinhTrai.Text.Trim() : null;
                examServiceReqUpdateSDO.PartExamKidneyUrology = !string.IsNullOrEmpty(txtThanTietNieu.Text.Trim()) ? txtThanTietNieu.Text.Trim() : null;

                examServiceReqUpdateSDO.PartExamDermatology = !string.IsNullOrEmpty(txtDaLieu.Text.Trim()) ? txtDaLieu.Text.Trim() : null;

                examServiceReqUpdateSDO.PartExamMental = !string.IsNullOrEmpty(txtPartExamMental.Text.Trim()) ? txtPartExamMental.Text.Trim() : null;
                examServiceReqUpdateSDO.PartExamNutrition = !string.IsNullOrEmpty(txtPartExamNutrition.Text.Trim()) ? txtPartExamNutrition.Text.Trim() : null;
                examServiceReqUpdateSDO.PartExamMotion = !string.IsNullOrEmpty(txtPartExamMotion.Text.Trim()) ? txtPartExamMotion.Text.Trim() : null;
                examServiceReqUpdateSDO.PartExamObstetric = !string.IsNullOrEmpty(txtPartExanObstetric.Text.Trim()) ? txtPartExanObstetric.Text.Trim() : null;
                examServiceReqUpdateSDO.PartExamMuscleBone = !string.IsNullOrEmpty(txtCoXuongKhop.Text.Trim()) ? txtCoXuongKhop.Text.Trim() : null;
                examServiceReqUpdateSDO.PartExamNeurological = !string.IsNullOrEmpty(txtThanKinh.Text.Trim()) ? txtThanKinh.Text.Trim() : null;
                examServiceReqUpdateSDO.PartExamOend = !string.IsNullOrEmpty(txtNoiTiet.Text.Trim()) ? txtNoiTiet.Text.Trim() : null;
                examServiceReqUpdateSDO.PartExamRespiratory = !string.IsNullOrEmpty(txtHoHap.Text.Trim()) ? txtHoHap.Text.Trim() : null;
                ActionSendSDO(examServiceReqUpdateSDO);
                this.Close();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void frmInformationExam_Load(object sender, EventArgs e)
        {
            try
            {
                EnableViaKeyDisablePartExamByExecutor();
                SetTabPageVisible(tabControlDetailData);
                tabControlDetailData.SelectedTabPageIndex = indexTabPageSelected;
                txtKhamBoPhan.Text = examServiceReqUpdateSDO.PartExam;
                txtTuanHoan.Text = examServiceReqUpdateSDO.PartExamCirculation;
                txtTieuHoa.Text = examServiceReqUpdateSDO.PartExamDigestion;
                txtTai.Text = examServiceReqUpdateSDO.PartExamEar;
                txtPART_EXAM_EAR_RIGHT_NORMAL.Text = examServiceReqUpdateSDO.PartExamEarRightNormal;
                txtPART_EXAM_EAR_RIGHT_WHISPER.Text = examServiceReqUpdateSDO.PartExamEarRightWhisper;
                txtPART_EXAM_EAR_LEFT_NORMAL.Text = examServiceReqUpdateSDO.PartExamEarLeftNormal;
                txtPART_EXAM_EAR_LEFT_WHISPER.Text = examServiceReqUpdateSDO.PartExamEarLeftWhisper;
                if (examServiceReqUpdateSDO.PartExamHorizontalSight != null)
                {
                    chkPART_EXAM_HORIZONTAL_SIGHT__BT.Checked = examServiceReqUpdateSDO.PartExamHorizontalSight == 1;
                    chkPART_EXAM_HORIZONTAL_SIGHT__HC.Checked = examServiceReqUpdateSDO.PartExamHorizontalSight == 2;
                }
                if (examServiceReqUpdateSDO.PartExamVerticalSight != null) {
                    chkPART_EXAM_VERTICAL_SIGHT__BT.Checked = examServiceReqUpdateSDO.PartExamVerticalSight == 1;
                    chkPART_EXAM_VERTICAL_SIGHT__HC.Checked = examServiceReqUpdateSDO.PartExamVerticalSight == 2;
                }
                if (examServiceReqUpdateSDO.PartExamEyeBlindColor != null)
                {
                    switch (examServiceReqUpdateSDO.PartExamEyeBlindColor)
                    {
                        case 1:
                            chkPART_EXAM_EYE_BLIND_COLOR__BT.Checked = examServiceReqUpdateSDO.PartExamEyeBlindColor == 1;
                            break;
                        case 2:
                            chkPART_EXAM_EYE_BLIND_COLOR__MMTB.Checked = examServiceReqUpdateSDO.PartExamEyeBlindColor == 2;
                            break;
                        case 3:
                            chkPART_EXAM_EYE_BLIND_COLOR__MMD.Checked = examServiceReqUpdateSDO.PartExamEyeBlindColor == 3;
                            break;
                        case 4:
                            chkPART_EXAM_EYE_BLIND_COLOR__MMXLC.Checked = examServiceReqUpdateSDO.PartExamEyeBlindColor == 4;
                            break;
                        case 5:
                            chkPART_EXAM_EYE_BLIND_COLOR__MMV.Checked = examServiceReqUpdateSDO.PartExamEyeBlindColor == 5;
                            break;
                        case 6:
                            chkPART_EXAM_EYE_BLIND_COLOR__MMD.Checked = chkPART_EXAM_EYE_BLIND_COLOR__MMXLC.Checked = examServiceReqUpdateSDO.PartExamEyeBlindColor == 6;
                            break;
                        case 7:
                            chkPART_EXAM_EYE_BLIND_COLOR__MMD.Checked = chkPART_EXAM_EYE_BLIND_COLOR__MMV.Checked = examServiceReqUpdateSDO.PartExamEyeBlindColor == 7;
                            break;
                        case 8:
                            chkPART_EXAM_EYE_BLIND_COLOR__MMXLC.Checked = chkPART_EXAM_EYE_BLIND_COLOR__MMV.Checked = examServiceReqUpdateSDO.PartExamEyeBlindColor == 8;
                            break;
                        case 9:
                            chkPART_EXAM_EYE_BLIND_COLOR__MMD.Checked = chkPART_EXAM_EYE_BLIND_COLOR__MMXLC.Checked = chkPART_EXAM_EYE_BLIND_COLOR__MMV.Checked = examServiceReqUpdateSDO.PartExamEyeBlindColor == 9;
                            break;
                        default:
                            chkPART_EXAM_EYE_BLIND_COLOR__MMD.Checked = chkPART_EXAM_EYE_BLIND_COLOR__MMXLC.Checked = chkPART_EXAM_EYE_BLIND_COLOR__MMV.Checked = chkPART_EXAM_EYE_BLIND_COLOR__BT.Checked = chkPART_EXAM_EYE_BLIND_COLOR__MMTB.Checked = false;
                            break;
                    }
                }
                txtMat.Text = examServiceReqUpdateSDO.PartExamEye;

                txtPART_EXAM_UPPER_JAW.Text = examServiceReqUpdateSDO.PartExamUpperJaw;
                txtPART_EXAM_LOWER_JAW.Text = examServiceReqUpdateSDO.PartExamLowerJaw;
                txtRHM.Text = examServiceReqUpdateSDO.PartExamStomatology;

                txtMui.Text = examServiceReqUpdateSDO.PartExamNose;
                txtHong.Text = examServiceReqUpdateSDO.PartExamThroat;
                txtNhanApPhai.Text = examServiceReqUpdateSDO.PartExamEyeTensionRight;
                txtNhanApTrai.Text = examServiceReqUpdateSDO.PartExamEyeTensionLeft;
                txtThiLucKhongKinhPhai.Text = examServiceReqUpdateSDO.PartExamEyeSightRight;
                txtThiLucKhongKinhTrai.Text = examServiceReqUpdateSDO.PartExamEyeSightLeft;
                txtThiLucCoKinhPhai.Text = examServiceReqUpdateSDO.PartExamEyeSightGlassRight;
                txtThiLucCoKinhTrai.Text = examServiceReqUpdateSDO.PartExamEyeSightGlassLeft;
                txtThanTietNieu.Text = examServiceReqUpdateSDO.PartExamKidneyUrology;

                txtDaLieu.Text = examServiceReqUpdateSDO.PartExamDermatology;

                txtPartExamMental.Text = examServiceReqUpdateSDO.PartExamMental;
                txtPartExamNutrition.Text = examServiceReqUpdateSDO.PartExamNutrition;
                txtPartExamMotion.Text = examServiceReqUpdateSDO.PartExamMotion;
                txtPartExanObstetric.Text = examServiceReqUpdateSDO.PartExamObstetric;
                txtCoXuongKhop.Text = examServiceReqUpdateSDO.PartExamMuscleBone;
                txtThanKinh.Text = examServiceReqUpdateSDO.PartExamNeurological;
                txtNoiTiet.Text = examServiceReqUpdateSDO.PartExamOend;
                txtHoHap.Text = examServiceReqUpdateSDO.PartExamRespiratory;
            }
            catch (Exception ex)
            {

                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void bbtn0_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                ShortCutCtrl0();
            }
            catch (Exception ex)
            {

                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void bbtn1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                ShortCutCtrl1();
            }
            catch (Exception ex)
            {

                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void bbtn2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                ShortCutCtrl2();
            }
            catch (Exception ex)
            {

                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void bbtn3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                ShortCutCtrl3();
            }
            catch (Exception ex)
            {

                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void bbtn4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                ShortCutCtrl4();
            }
            catch (Exception ex)
            {

                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void bbtn5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                ShortCutCtrl5();
            }
            catch (Exception ex)
            {

                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void bbtn6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                ShortCutCtrl6();
            }
            catch (Exception ex)
            {

                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void bbtn7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                ShortCutCtrl7();
            }
            catch (Exception ex)
            {

                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void bbtn8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                ShortCutCtrl8();
            }
            catch (Exception ex)
            {

                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void bbtn9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                ShortCutCtrl9();
            }
            catch (Exception ex)
            {

                Inventec.Common.Logging.LogSystem.Warn(ex);
            }

        }

        private void bbtnQ_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                ShortCutCtrlQ();
            }
            catch (Exception ex)
            {

                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void bbtnW_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                ShortCutCtrlW();
            }
            catch (Exception ex)
            {

                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void bbtnU_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                ShortCutCtrlU();
            }
            catch (Exception ex)
            {

                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void bbtnR_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                ShortCutCtrlR();
            }
            catch (Exception ex)
            {

                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void bbtnT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                ShortCutCtrlT();
            }
            catch (Exception ex)
            {

                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void bbtnI_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                ShortCutCtrlI();
            }
            catch (Exception ex)
            {

                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        #region Shortcut Tab

        public void ShortCutCtrl0()
        {
            xtraTabPageChung.PageVisible = true;
            tabControlDetailData.SelectedTabPage = xtraTabPageChung;
            txtKhamBoPhan.Focus();
            txtKhamBoPhan.SelectAll();
        }

        public void ShortCutCtrl1()
        {
            xtraTabTuanHoan.PageVisible = true;
            tabControlDetailData.SelectedTabPage = xtraTabTuanHoan;
            txtTuanHoan.Focus();
            txtTuanHoan.SelectAll();
        }
        public void ShortCutCtrl2()
        {
            xtraTabHoHap.PageVisible = true;
            tabControlDetailData.SelectedTabPage = xtraTabHoHap;
            txtHoHap.Focus();
            txtHoHap.SelectAll();
        }

        public void ShortCutCtrl3()
        {
            xtraTabTieuHoa.PageVisible = true;
            tabControlDetailData.SelectedTabPage = xtraTabTieuHoa;
            txtTieuHoa.Focus();
            txtTieuHoa.SelectAll();
        }
        public void ShortCutCtrl4()
        {
            xtraTabThanTietNieu.PageVisible = true;
            tabControlDetailData.SelectedTabPage = xtraTabThanTietNieu;
            txtThanTietNieu.Focus();
            txtThanTietNieu.SelectAll();
        }

        public void ShortCutCtrl5()
        {
            xtraTabThanKinh.PageVisible = true;
            tabControlDetailData.SelectedTabPage = xtraTabThanKinh;
            txtThanKinh.Focus();
            txtThanKinh.SelectAll();
        }
        public void ShortCutCtrl6()
        {
            xtraTabCoXuongKhop.PageVisible = true;
            tabControlDetailData.SelectedTabPage = xtraTabCoXuongKhop;
            txtCoXuongKhop.Focus();
            txtCoXuongKhop.SelectAll();
        }

        public void ShortCutCtrl7()
        {
            xtraTabTaiMuiHong.PageVisible = true;
            tabControlDetailData.SelectedTabPage = xtraTabTaiMuiHong;
            txtTai.Focus();
            txtTai.SelectAll();
        }
        public void ShortCutCtrl8()
        {
            xtraTabRangHamMat.PageVisible = true;
            tabControlDetailData.SelectedTabPage = xtraTabRangHamMat;
            txtRHM.Focus();
            txtRHM.SelectAll();
        }

        public void ShortCutCtrl9()
        {
            xtraTabMat.PageVisible = true;
            tabControlDetailData.SelectedTabPage = xtraTabMat;
            txtMat.Focus();
            txtMat.SelectAll();
        }
        public void ShortCutCtrlQ()
        {
            xtraTabNoiTiet.PageVisible = true;
            tabControlDetailData.SelectedTabPage = xtraTabNoiTiet;
            txtNoiTiet.Focus();
            txtNoiTiet.SelectAll();
        }

        public void ShortCutCtrlW()
        {
            xtraTabTamThan.PageVisible = true;
            tabControlDetailData.SelectedTabPage = xtraTabTamThan;
            txtPartExamMental.Focus();
            txtPartExamMental.SelectAll();
        }

        public void ShortCutCtrlU()
        {
            xtraTabDinhDuong.PageVisible = true;
            tabControlDetailData.SelectedTabPage = xtraTabDinhDuong;
            txtPartExamNutrition.Focus();
            txtPartExamNutrition.SelectAll();
        }

        public void ShortCutCtrlR()
        {
            xtraTabVanDong.PageVisible = true;
            tabControlDetailData.SelectedTabPage = xtraTabVanDong;
            txtPartExamMotion.Focus();
            txtPartExamMotion.SelectAll();
        }

        public void ShortCutCtrlT()
        {
            xtraTabSanPhuKhoa.PageVisible = true;
            tabControlDetailData.SelectedTabPage = xtraTabSanPhuKhoa;
            txtPartExanObstetric.Focus();
            txtPartExanObstetric.SelectAll();
        }

        public void ShortCutCtrlI()
        {
            xtraTabDaLieu.PageVisible = true;
            tabControlDetailData.SelectedTabPage = xtraTabDaLieu;
            txtDaLieu.Focus();
            txtDaLieu.SelectAll();
        }

        #endregion

        private void tabControlDetailData_CustomHeaderButtonClick(object sender, DevExpress.XtraTab.ViewInfo.CustomHeaderButtonEventArgs e)
        {
            try
            {
                if (e.Button.Kind == ButtonPredefines.Down)
                {
                    DXPopupMenu _menu = new DevExpress.Utils.Menu.DXPopupMenu();

                    foreach (XtraTabPage item in tabControlDetailData.TabPages)
                    {
                        if (item.PageVisible == false)
                        {
                            DXMenuItem itemTuanHoan = new DXMenuItem();
                            itemTuanHoan.Caption = item.Text.Trim();
                            itemTuanHoan.Click += item_click;
                            itemTuanHoan.Tag = item;
                            _menu.Items.Add(itemTuanHoan);
                        }
                    }

                    DevExpress.XtraBars.BarManager mobjBarMgr = new DevExpress.XtraBars.BarManager();
                    mobjBarMgr.Form = this;
                    DevExpress.Utils.Menu.MenuManagerHelper.ShowMenu(_menu, tabControlDetailData.LookAndFeel, mobjBarMgr, tabControlDetailData, tabControlDetailData.PointToClient(Control.MousePosition));
                }
                else if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    XtraTabPage selectedTab = tabControlDetailData.SelectedTabPage;
                    if (selectedTab != null)
                    {
                        selectedTab.PageVisible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
        private void item_click(object sender, EventArgs e)
        {
            try
            {
                DXMenuItem item = sender as DXMenuItem;
                if (item.Tag is XtraTabPage)
                {
                    XtraTabPage tab = item.Tag as XtraTabPage;
                    tab.PageVisible = true;
                    tabControlDetailData.SelectedTabPage = tab;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void txtPART_EXAM_EAR_RIGHT_NORMAL_KeyPress(object sender, KeyPressEventArgs e)
        {
            SpinKeyPress(sender, e);
        }

        private void txtPART_EXAM_EAR_RIGHT_WHISPER_KeyPress(object sender, KeyPressEventArgs e)
        {
            SpinKeyPress(sender, e);
        }

        private void txtPART_EXAM_EAR_LEFT_NORMAL_KeyPress(object sender, KeyPressEventArgs e)
        {
            SpinKeyPress(sender, e);
        }

        private void txtPART_EXAM_EAR_LEFT_WHISPER_KeyPress(object sender, KeyPressEventArgs e)
        {
            SpinKeyPress(sender, e);
        }
        private void SpinKeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
       (e.KeyChar != '.') && (e.KeyChar != ','))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void chkPART_EXAM_EYE_BLIND_COLOR__BT_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkPART_EXAM_EYE_BLIND_COLOR__BT.Checked)
                {
                    chkPART_EXAM_EYE_BLIND_COLOR__MMD.Checked = chkPART_EXAM_EYE_BLIND_COLOR__MMXLC.Checked = chkPART_EXAM_EYE_BLIND_COLOR__MMV.Checked = false;
                    chkPART_EXAM_EYE_BLIND_COLOR__MMTB.Checked = !chkPART_EXAM_EYE_BLIND_COLOR__BT.Checked;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void chkPART_EXAM_EYE_BLIND_COLOR__MMTB_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkPART_EXAM_EYE_BLIND_COLOR__MMTB.Checked)
                {
                    chkPART_EXAM_EYE_BLIND_COLOR__MMD.Checked = chkPART_EXAM_EYE_BLIND_COLOR__MMXLC.Checked = chkPART_EXAM_EYE_BLIND_COLOR__MMV.Checked = false;
                    chkPART_EXAM_EYE_BLIND_COLOR__BT.Checked = !chkPART_EXAM_EYE_BLIND_COLOR__MMTB.Checked;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void chkPART_EXAM_EYE_BLIND_COLOR__MMD_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkPART_EXAM_EYE_BLIND_COLOR__MMD.Checked)
                    chkPART_EXAM_EYE_BLIND_COLOR__BT.Checked = chkPART_EXAM_EYE_BLIND_COLOR__MMTB.Checked = false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void chkPART_EXAM_EYE_BLIND_COLOR__MMXLC_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkPART_EXAM_EYE_BLIND_COLOR__MMXLC.Checked)
                    chkPART_EXAM_EYE_BLIND_COLOR__BT.Checked = chkPART_EXAM_EYE_BLIND_COLOR__MMTB.Checked = false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void chkPART_EXAM_EYE_BLIND_COLOR__MMV_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkPART_EXAM_EYE_BLIND_COLOR__MMV.Checked)
                    chkPART_EXAM_EYE_BLIND_COLOR__BT.Checked = chkPART_EXAM_EYE_BLIND_COLOR__MMTB.Checked = false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }


        private void txtNhanApPhai_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                lblMatPhai.Text = "";
                if (!String.IsNullOrWhiteSpace(txtNhanApPhai.Text.Trim()))
                {
                    bool isNum = true;
                    foreach (var item in txtNhanApPhai.Text.Trim())
                    {
                        if (!char.IsNumber(item))
                        {
                            isNum = false;
                            break;
                        }
                    }

                    if (isNum)
                    {
                        lblMatPhai.Text = "mmHg";
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void txtThiLucKhongKinhPhai_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                LblKoKinhPhai.Text = "";
                if (!String.IsNullOrWhiteSpace(txtThiLucKhongKinhPhai.Text.Trim()))
                {
                    bool isNum = true;
                    foreach (var item in txtThiLucKhongKinhPhai.Text.Trim())
                    {
                        if (!char.IsNumber(item))
                        {
                            isNum = false;
                            break;
                        }
                    }

                    if (isNum)
                    {
                        LblKoKinhPhai.Text = "/10";
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void txtThiLucCoKinhPhai_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                LblCoKinhPhai.Text = "";
                if (!String.IsNullOrWhiteSpace(txtThiLucCoKinhPhai.Text.Trim()))
                {
                    bool isNum = true;
                    foreach (var item in txtThiLucCoKinhPhai.Text.Trim())
                    {
                        if (!char.IsNumber(item))
                        {
                            isNum = false;
                            break;
                        }
                    }

                    if (isNum)
                    {
                        LblCoKinhPhai.Text = "/10";
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void txtNhanApTrai_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                LblMatTrai.Text = "";
                if (!String.IsNullOrWhiteSpace(txtNhanApTrai.Text.Trim()))
                {
                    bool isNum = true;
                    foreach (var item in txtNhanApTrai.Text.Trim())
                    {
                        if (!char.IsNumber(item))
                        {
                            isNum = false;
                            break;
                        }
                    }

                    if (isNum)
                    {
                        LblMatTrai.Text = "mmHg";
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void txtThiLucKhongKinhTrai_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                LblKoKinhTrai.Text = "";
                if (!String.IsNullOrWhiteSpace(txtThiLucKhongKinhTrai.Text.Trim()))
                {
                    bool isNum = true;
                    foreach (var item in txtThiLucKhongKinhTrai.Text.Trim())
                    {
                        if (!char.IsNumber(item))
                        {
                            isNum = false;
                            break;
                        }
                    }

                    if (isNum)
                    {
                        LblKoKinhTrai.Text = "/10";
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void txtThiLucCoKinhTrai_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                LblCoKinhTrai.Text = "";
                if (!String.IsNullOrWhiteSpace(txtThiLucCoKinhTrai.Text.Trim()))
                {
                    bool isNum = true;
                    foreach (var item in txtThiLucCoKinhTrai.Text.Trim())
                    {
                        if (!char.IsNumber(item))
                        {
                            isNum = false;
                            break;
                        }
                    }

                    if (isNum)
                    {
                        LblCoKinhTrai.Text = "/10";
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
