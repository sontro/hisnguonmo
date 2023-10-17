using DevExpress.XtraEditors;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Plugins.ExamServiceReqExecute.ADO;
using HIS.Desktop.Utility;
using Inventec.Common.Adapter;
using Inventec.Core;
using Inventec.Desktop.Common.Message;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.ExamServiceReqExecute
{
    public partial class ExamServiceReqExecuteControl : UserControlBase
    {
        private HIS_TREATMENT GetTreatmentById(long id)
        {
            HIS_TREATMENT rs = null;
            try
            {
                CommonParam param = new CommonParam();
                HisTreatmentFilter filter = new HisTreatmentFilter();
                filter.ID = id;

                var rsApi = new BackendAdapter(param).Get<List<HIS_TREATMENT>>("api/HisTreatment/Get", ApiConsumers.MosConsumer, filter, param);
                if (rsApi != null && rsApi.Count > 0)
                {
                    rs = rsApi.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
                return null;
            }

            return rs;
        }

        private List<HIS_TREATMENT_END_TYPE_EXT> GetTreatmentEndTypeExt()
        {
            List<HIS_TREATMENT_END_TYPE_EXT> rs = null;
            try
            {
                CommonParam param = new CommonParam();
                HisTreatmentEndTypeExtFilter filter = new HisTreatmentEndTypeExtFilter();
                filter.IS_ACTIVE = 1;
                rs = new BackendAdapter(param).Get<List<HIS_TREATMENT_END_TYPE_EXT>>("api/HisTreatmentEndTypeExt/Get", ApiConsumers.MosConsumer, filter, param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
                return null;
            }
            return rs;
        }

        List<HIS_SERE_SERV> GetSereServWithMinDuration(long patientId, List<long> serviceIds)
        {
            List<HIS_SERE_SERV> results = new List<HIS_SERE_SERV>();
            try
            {
                if (serviceIds == null || serviceIds.Count == 0)
                {
                    Inventec.Common.Logging.LogSystem.Debug("Khong truyen danh sach serviceids");
                    return null;
                }

                List<V_HIS_SERVICE> services = HIS.Desktop.LocalStorage.BackendData.BackendDataWorker.Get<V_HIS_SERVICE>()
                    .Where(o => serviceIds.Contains(o.ID) && o.MIN_DURATION.HasValue).ToList();
                if (services == null)
                    return null;

                List<ServiceDuration> serviceDurations = new List<ServiceDuration>();
                foreach (var item in services)
                {
                    ServiceDuration sd = new ServiceDuration();
                    sd.MinDuration = item.MIN_DURATION.Value;
                    sd.ServiceId = item.ID;
                    serviceDurations.Add(sd);
                }

                CommonParam param = new CommonParam();
                HisSereServMinDurationFilter hisSereServMinDurationFilter = new HisSereServMinDurationFilter();
                hisSereServMinDurationFilter.ServiceDurations = serviceDurations;
                hisSereServMinDurationFilter.PatientId = patientId;
                hisSereServMinDurationFilter.InstructionTime = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(DateTime.Now) ?? 0;

                results = new BackendAdapter(param).Get<List<HIS_SERE_SERV>>("api/HisSereServ/GetExceedMinDuration", ApiConsumer.ApiConsumers.MosConsumer, hisSereServMinDurationFilter, param);

                if (results != null && results.Count > 0)
                {
                    var listSereServResultTemp = from SereServResult in results
                                                 group SereServResult by SereServResult.SERVICE_ID into g
                                                 orderby g.Key
                                                 select g.FirstOrDefault();
                    results = listSereServResultTemp.ToList();
                }
            }
            catch (Exception ex)
            {
                results = null;
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return results;
        }

        private bool IsLessThan1YearOldOr6YearOld(long dob)
        {
            bool result = false;
            try
            {
                this.IsRequiredWeightOption = HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<long>(SdaConfigKeys.IS_REQUIRED_WEIGHT_OPTION);

                DateTime dtNgSinh = Inventec.Common.TypeConvert.Parse.ToDateTime(Inventec.Common.DateTime.Convert.TimeNumberToTimeString(dob));
                TimeSpan diff = DateTime.Now - dtNgSinh;
                double tongSoNgay = diff.TotalDays;
                if (tongSoNgay < 366 && IsRequiredWeightOption == 1)
                {
                    return true;
                }
                else if (tongSoNgay < 2196 && IsRequiredWeightOption == 2)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }

        private bool IsThan16YearOld(long dob)
        {
            bool result = false;
            try
            {
                DateTime dtNgSinh = Inventec.Common.TypeConvert.Parse.ToDateTime(Inventec.Common.DateTime.Convert.TimeNumberToTimeString(dob));
                TimeSpan diff = DateTime.Now - dtNgSinh;
                long tongsogiay = diff.Ticks;
                DateTime newDate = new DateTime(tongsogiay);
                int nam = newDate.Year - 1;
                if (nam > 16)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }
        private bool IsThan16YearOldByTreatment()
        {
            bool result = false;
            try
            {
                var nam = Int64.Parse(treatment.IN_TIME.ToString().Substring(0, 4)) - Int64.Parse(treatment.TDL_PATIENT_DOB.ToString().Substring(0,4));
                if (nam < 16)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }

        private void OutFocus()
        {
            try
            {
                cboKskCode.Focus();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void LoadDataToControlFromTemp(HIS_EXAM_SERVICE_TEMP examServiceTemp)
        {
            try
            {
                if (examServiceTemp != null)
                {
                    txtHospitalizationReason.Text = examServiceTemp.HOSPITALIZATION_REASON;
                    txtPathologicalProcess.Text = examServiceTemp.PATHOLOGICAL_PROCESS;
                    txtPathologicalHistory.Text = examServiceTemp.PATHOLOGICAL_HISTORY;
                    txtPathologicalHistoryFamily.Text = examServiceTemp.PATHOLOGICAL_HISTORY_FAMILY;
                    txtKhamToanThan.Text = examServiceTemp.FULL_EXAM;
                    txtKhamBoPhan.Text = examServiceTemp.PART_EXAM;
                    txtTuanHoan.Text = examServiceTemp.PART_EXAM_CIRCULATION;
                    txtHoHap.Text = examServiceTemp.PART_EXAM_RESPIRATORY;
                    txtTieuHoa.Text = examServiceTemp.PART_EXAM_DIGESTION;
                    txtThanTietNieu.Text = examServiceTemp.PART_EXAM_KIDNEY_UROLOGY;
                    txtPartExamMental.Text = examServiceTemp.PART_EXAM_MENTAL;
                    txtPartExamNutrition.Text = examServiceTemp.PART_EXAM_NUTRITION;
                    txtPartExamMotion.Text = examServiceTemp.PART_EXAM_MOTION;
                    txtPartExanObstetric.Text = examServiceTemp.PART_EXAM_OBSTETRIC;
                    txtThanKinh.Text = examServiceTemp.PART_EXAM_NEUROLOGICAL;
                    txtCoXuongKhop.Text = examServiceTemp.PART_EXAM_MUSCLE_BONE;
                    txtRHM.Text = examServiceTemp.PART_EXAM_STOMATOLOGY;
                    txtMat.Text = examServiceTemp.PART_EXAM_EYE;

                    txtNoiTiet.Text = examServiceTemp.PART_EXAM_OEND;
                    txtSubclinical.Text = examServiceTemp.DESCRIPTION;
                    txtTreatmentInstruction.Text = examServiceTemp.CONCLUDE;
                    //txtNextTreatmentInstruction.Text = examServiceTemp.NOTE;
                    txtTai.Text = examServiceTemp.PART_EXAM_EAR;
                    txtMui.Text = examServiceTemp.PART_EXAM_NOSE;
                    txtHong.Text = examServiceTemp.PART_EXAM_THROAT;
                    txtPART_EXAM_EAR_RIGHT_NORMAL.Text = examServiceTemp.PART_EXAM_EAR_RIGHT_NORMAL;
                    txtPART_EXAM_EAR_RIGHT_WHISPER.Text = examServiceTemp.PART_EXAM_EAR_RIGHT_WHISPER;
                    txtPART_EXAM_EAR_LEFT_NORMAL.Text = examServiceTemp.PART_EXAM_EAR_LEFT_NORMAL;
                    txtPART_EXAM_EAR_LEFT_WHISPER.Text = examServiceTemp.PART_EXAM_EAR_LEFT_WHISPER;                 
                    txtPART_EXAM_UPPER_JAW.Text = examServiceTemp.PART_EXAM_UPPER_JAW;
                    txtPART_EXAM_LOWER_JAW.Text = examServiceTemp.PART_EXAM_LOWER_JAW;

                    txtNhanApTrai.Text = examServiceTemp.PART_EXAM_EYE_TENSION_LEFT;
                    txtNhanApPhai.Text = examServiceTemp.PART_EXAM_EYE_TENSION_RIGHT;
                    txtThiLucKhongKinhTrai.Text = examServiceTemp.PART_EXAM_EYESIGHT_LEFT;
                    txtThiLucKhongKinhPhai.Text = examServiceTemp.PART_EXAM_EYESIGHT_RIGHT;
                    txtThiLucCoKinhTrai.Text=examServiceTemp.PART_EXAM_EYESIGHT_GLASS_LEFT;
                    txtThiLucCoKinhPhai.Text = examServiceTemp.PART_EXAM_EYESIGHT_GLASS_RIGHT;
                    if (examServiceTemp.PART_EXAM_HORIZONTAL_SIGHT == 1)
                        chkPART_EXAM_HORIZONTAL_SIGHT__BT.Checked = true;
                    else if (examServiceTemp.PART_EXAM_HORIZONTAL_SIGHT == 2)
                        chkPART_EXAM_HORIZONTAL_SIGHT__HC.Checked = true;
                    else
                    {
                        chkPART_EXAM_HORIZONTAL_SIGHT__BT.Checked = false;
                        chkPART_EXAM_HORIZONTAL_SIGHT__HC.Checked = false;
                    }

                    if (examServiceTemp.PART_EXAM_VERTICAL_SIGHT == 1)
                        chkPART_EXAM_VERTICAL_SIGHT__BT.Checked = true;
                    else if (examServiceTemp.PART_EXAM_VERTICAL_SIGHT == 2)
                        chkPART_EXAM_VERTICAL_SIGHT__HC.Checked = true;
                    else
                    {
                        chkPART_EXAM_VERTICAL_SIGHT__BT.Checked = false;
                        chkPART_EXAM_VERTICAL_SIGHT__HC.Checked = false;
                    }

                    //- Sắc giác: PART_EXAM_EYE_BLIND_COLOR lưu vào HIS_SERVICE_REQ: 1-bình thường, 2- mù màu tòan bộ , 3- mù màu đỏ, 4- mù màu xanh lá, 5- mù màu vàng, 6- mù màu đỏ +xanh lá, 7 - mù màu đỏ + vàng, 8- mù màu xanh lá + vàng, 9- mù màu đỏ + xanh lá + vàng
                    //* nếu tích bình thường --->không được tính mù màu + mù màu 1 phần.
                    //* nếu tích mù toàn bộ--->cũng không được tính mù 1 phần + bình thường
                    //* nếu k tích 2 TH bình thường và mù màu toàn bộ - >thì 3 cái mù ở dưới cho phép chọn nhiều

                    chkPART_EXAM_EYE_BLIND_COLOR__BT.Checked = (examServiceTemp.PART_EXAM_EYE_BLIND_COLOR == 1);
                    chkPART_EXAM_EYE_BLIND_COLOR__MMTB.Checked = (examServiceTemp.PART_EXAM_EYE_BLIND_COLOR == 2);

                    chkPART_EXAM_EYE_BLIND_COLOR__MMD.Checked = (examServiceTemp.PART_EXAM_EYE_BLIND_COLOR == 3 || examServiceTemp.PART_EXAM_EYE_BLIND_COLOR == 6 || examServiceTemp.PART_EXAM_EYE_BLIND_COLOR == 7 || examServiceTemp.PART_EXAM_EYE_BLIND_COLOR == 9);
                    chkPART_EXAM_EYE_BLIND_COLOR__MMXLC.Checked = (examServiceTemp.PART_EXAM_EYE_BLIND_COLOR == 4 || examServiceTemp.PART_EXAM_EYE_BLIND_COLOR == 6 || examServiceTemp.PART_EXAM_EYE_BLIND_COLOR == 8 || examServiceTemp.PART_EXAM_EYE_BLIND_COLOR == 9);
                    chkPART_EXAM_EYE_BLIND_COLOR__MMV.Checked = (examServiceTemp.PART_EXAM_EYE_BLIND_COLOR == 5 || examServiceTemp.PART_EXAM_EYE_BLIND_COLOR == 7 || examServiceTemp.PART_EXAM_EYE_BLIND_COLOR == 8 || examServiceTemp.PART_EXAM_EYE_BLIND_COLOR == 9);

                    txtDaLieu.Text = examServiceTemp.PART_EXAM_DERMATOLOGY;
                    if (!String.IsNullOrWhiteSpace(txtKhamBoPhan.Text.Trim()))
                        xtraTabPageChung.PageVisible = true;

                    if (!string.IsNullOrWhiteSpace(txtTuanHoan.Text.Trim()))
                        xtraTabTuanHoan.PageVisible = true;

                    if (!string.IsNullOrWhiteSpace(txtHoHap.Text.Trim()))
                        xtraTabHoHap.PageVisible = true;

                    if (!string.IsNullOrWhiteSpace(txtTieuHoa.Text.Trim()))
                        xtraTabTieuHoa.PageVisible = true;

                    if (!string.IsNullOrWhiteSpace(txtThanTietNieu.Text.Trim()))
                        xtraTabThanTietNieu.PageVisible = true;

                    if (!string.IsNullOrWhiteSpace(txtThanKinh.Text.Trim()))
                        xtraTabThanKinh.PageVisible = true;

                    if (!string.IsNullOrWhiteSpace(txtCoXuongKhop.Text.Trim()))
                        xtraTabCoXuongKhop.PageVisible = true;

                    if (!string.IsNullOrWhiteSpace(txtRHM.Text.Trim()))
                        xtraTabRangHamMat.PageVisible = true;

                    if (!string.IsNullOrWhiteSpace(txtNoiTiet.Text.Trim()))
                        xtraTabNoiTiet.PageVisible = true;

                    if (!string.IsNullOrWhiteSpace(txtPartExanObstetric.Text.Trim()))
                        xtraTabSanPhuKhoa.PageVisible = true;

                    if (!string.IsNullOrWhiteSpace(txtPartExamMotion.Text.Trim()))
                        xtraTabVanDong.PageVisible = true;

                    if (!string.IsNullOrWhiteSpace(txtPartExamNutrition.Text.Trim()))
                        xtraTabDinhDuong.PageVisible = true;

                    if (!string.IsNullOrWhiteSpace(txtPartExamMental.Text.Trim()))
                        xtraTabTamThan.PageVisible = true;

                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }

        }

        private void LoadTreatmentByPatient()
        {
            try
            {
                CommonParam param = new CommonParam();
                HisTreatmentLView2Filter treatmentFilter = new HisTreatmentLView2Filter();
                treatmentFilter.PATIENT_ID = this.HisServiceReqView.TDL_PATIENT_ID;
                this.treatmentByPatients = new BackendAdapter(param)
                    .Get<List<MOS.EFMODEL.DataModels.L_HIS_TREATMENT_2>>("api/HisTreatment/GetLView2", ApiConsumers.MosConsumer, treatmentFilter, param);
                this.ltreatment2 = this.treatmentByPatients.FirstOrDefault(o => o.ID == this.HisServiceReqView.TREATMENT_ID);

                HisTreatmentFilter treatment = new HisTreatmentFilter();
                treatment.ID = this.HisServiceReqView.TREATMENT_ID;
                this.treatment = new BackendAdapter(param)
                    .Get<List<MOS.EFMODEL.DataModels.HIS_TREATMENT>>("api/HisTreatment/Get", ApiConsumers.MosConsumer, treatment, param).FirstOrDefault();
                if (this.treatment != null)
                {
                    UpdateNeedSickLeaveCertControl(this.treatment.NEED_SICK_LEAVE_CERT);
                    this.icdDefaultFinish.ICD_CODE = this.treatment.ICD_CODE;
                    this.icdDefaultFinish.ICD_NAME = this.treatment.ICD_NAME;
                    if (this.treatment.TREATMENT_END_TYPE_EXT_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE_EXT.ID__HEN_MO)
                    {
                        ReLoadPrintTreatmentEndTypeExt(PrintType.IN_PHIEU_HEN_MO);
                    }
                }

                EnableControlFastCreateTracking();
            //CommonParam param = new CommonParam();
            //HisTreatmentFilter treatmentFilter = new HisTreatmentFilter();
            //treatmentFilter.PATIENT_ID = this.HisServiceReqView.TDL_PATIENT_ID;
            //this.treatmentByPatients = new BackendAdapter(param)
            //    .Get<List<MOS.EFMODEL.DataModels.HIS_TREATMENT>>("api/HisTreatment/Get", ApiConsumers.MosConsumer, treatmentFilter, param);

            //this.treatment = this.treatmentByPatients.FirstOrDefault(o => o.ID == this.HisServiceReqView.TREATMENT_ID);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void LoadTreatmentHistoryTogrid()
        {
            try
            {
                LoadTreatmentHistory();
                if (this.TreatmentHistorys != null && this.TreatmentHistorys.Count > 0)
                {
                    gridControlTreatmentHistory.DataSource = this.TreatmentHistorys;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void LoadTreatmentHistory()
        {
            try
            {
                this.TreatmentHistorys = new List<TreatmentExamADO>();
                if (this.treatmentByPatients != null && this.treatmentByPatients.Count > 0)
                {
                    List<L_HIS_TREATMENT_2> treatmentHTs = this.treatmentByPatients.Where(o => o.ID != this.HisServiceReqView.TREATMENT_ID && o.TDL_FIRST_EXAM_ROOM_ID.HasValue).OrderByDescending(o => o.IN_TIME).ToList();
                    if (treatmentHTs != null && treatmentHTs.Count > 0)
                    {
                        List<HIS_EXECUTE_ROOM> executeRooms = HIS.Desktop.LocalStorage.BackendData.BackendDataWorker.Get<HIS_EXECUTE_ROOM>();
                        foreach (var item in treatmentHTs)
                        {
                            AutoMapper.Mapper.CreateMap<MOS.EFMODEL.DataModels.L_HIS_TREATMENT_2, TreatmentExamADO>();
                            TreatmentExamADO treatmentHistoryADO = AutoMapper.Mapper.Map<L_HIS_TREATMENT_2, TreatmentExamADO>(item);
                            HIS_EXECUTE_ROOM executeRoom = executeRooms.FirstOrDefault(o => o.ROOM_ID == item.TDL_FIRST_EXAM_ROOM_ID);
                            treatmentHistoryADO.FIRST_EXAM_ROOM_NAME = (executeRoom != null ? executeRoom.EXECUTE_ROOM_NAME : "");
                            this.TreatmentHistorys.Add(treatmentHistoryADO);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private async Task LoadgridControlDKPresent()
        {
            try
            {
                CommonParam param = new CommonParam();
                HisServiceReqViewFilter filter = new HisServiceReqViewFilter();
                setfilter(ref filter);
                List<V_HIS_SERVICE_REQ> data = new List<V_HIS_SERVICE_REQ>();
                gridViewDKPresent.BeginUpdate();
                data = await new BackendAdapter(param).GetAsync<List<V_HIS_SERVICE_REQ>>("api/HisServiceReq/GetView", ApiConsumers.MosConsumer, filter, param);
                if (data != null)
                {
                    gridControlDKPresent.DataSource = data;
                }
                gridViewDKPresent.EndUpdate();
            }
            catch (Exception ex)
            {

                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void setfilter(ref HisServiceReqViewFilter filter)
        {
            try
            {

                filter.SERVICE_REQ_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH;
                filter.HAS_EXECUTE = true;
                filter.TREATMENT_ID = this.treatmentId;
                filter.ORDER_DIRECTION = "DESC";
                filter.ORDER_FIELD = "INTRUCTION_TIME";
            }
            catch (Exception ex)
            {

                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void LoadTreatmentInfomation()
        {
            try
            {
                Inventec.Common.Logging.LogSystem.Debug("LoadTreatmentInfomation .Start!");
                // Tự động hiển thị thông tin khám theo lần khám trước đó trong trường hợp điều trị ngoại trú (TH xử lý khám đầu tiên START_TIME = MODIFY_TIME hoặc START_TIME + 1 = MODIFY_TIME)
                AutoMapper.Mapper.CreateMap<V_HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                HIS_SERVICE_REQ serviceReq = AutoMapper.Mapper.Map<V_HIS_SERVICE_REQ, HIS_SERVICE_REQ>(HisServiceReqView);
                if (this.HisServiceReqView != null 
                    && Config.HisConfigCFG.IsAutoSetExamInforByPreviousTreatmentInCaseOfOutPatient
                    && serviceReq.START_TIME != null
                    && (serviceReq.START_TIME == serviceReq.MODIFY_TIME || serviceReq.START_TIME + 1 == serviceReq.MODIFY_TIME))
                {
                    var treatment = this.treatment;
                    Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData("treatment", treatment));
                    Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData("treatment.TDL_TREATMENT_TYPE_ID", treatment.TDL_TREATMENT_TYPE_ID));
                    if (treatment != null && treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU)
                    {
                        HIS_TREATMENT previousTreatment = GetPreviousTreatmentByPatientID(serviceReq.TDL_PATIENT_ID, treatment.ID);
                        Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData("previousTreatment", previousTreatment));
                        if (previousTreatment != null && previousTreatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU)
                        {
                            V_HIS_SERVICE_REQ serviceReq_KhamChinh = GetServiceReq_KhamChinh_ByTreatmentID(previousTreatment.ID);
                            Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData("serviceReq_KhamChinh", serviceReq_KhamChinh));
                            if (serviceReq_KhamChinh !=null)
                            {
                                SetExamInforByPreviousTreatmentInCaseOfOutPatient(serviceReq_KhamChinh);
                            }
                        }
                    }
                }

                this.LoadDataFromTreatment();
                this.FillDataToControlEditor();
                Inventec.Common.Logging.LogSystem.Debug("LoadTreatmentInfomation .Ended!");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void SetExamInforByPreviousTreatmentInCaseOfOutPatient(V_HIS_SERVICE_REQ serviceReq)
        {
            try
            {
                if (this.HisServiceReqView == null || serviceReq == null)
                    return;
                Inventec.Common.Logging.LogSystem.Debug("SetExamInforByPreviousTreatmentInCaseOfOutPatient()");

                this.HisServiceReqView.HOSPITALIZATION_REASON = serviceReq.HOSPITALIZATION_REASON;
                this.HisServiceReqView.SICK_DAY = serviceReq.SICK_DAY;
                this.HisServiceReqView.PATHOLOGICAL_PROCESS = serviceReq.PATHOLOGICAL_PROCESS;
                this.HisServiceReqView.FULL_EXAM = serviceReq.FULL_EXAM;
                this.HisServiceReqView.PART_EXAM = serviceReq.PART_EXAM;
                this.HisServiceReqView.PART_EXAM_CIRCULATION = serviceReq.PART_EXAM_CIRCULATION;
                this.HisServiceReqView.PART_EXAM_RESPIRATORY = serviceReq.PART_EXAM_RESPIRATORY;
                this.HisServiceReqView.PART_EXAM_DIGESTION = serviceReq.PART_EXAM_DIGESTION;
                this.HisServiceReqView.PART_EXAM_KIDNEY_UROLOGY = serviceReq.PART_EXAM_KIDNEY_UROLOGY;
                this.HisServiceReqView.PART_EXAM_MENTAL = serviceReq.PART_EXAM_MENTAL;
                this.HisServiceReqView.PART_EXAM_NUTRITION = serviceReq.PART_EXAM_NUTRITION;
                this.HisServiceReqView.PART_EXAM_MOTION = serviceReq.PART_EXAM_MOTION;
                this.HisServiceReqView.PART_EXAM_OBSTETRIC = serviceReq.PART_EXAM_OBSTETRIC;
                this.HisServiceReqView.PART_EXAM_NEUROLOGICAL = serviceReq.PART_EXAM_NEUROLOGICAL;
                this.HisServiceReqView.PART_EXAM_MUSCLE_BONE = serviceReq.PART_EXAM_MUSCLE_BONE;
                this.HisServiceReqView.PART_EXAM_EAR = serviceReq.PART_EXAM_EAR;
                this.HisServiceReqView.PART_EXAM_EAR_RIGHT_NORMAL = serviceReq.PART_EXAM_EAR_RIGHT_NORMAL;
                this.HisServiceReqView.PART_EXAM_EAR_RIGHT_WHISPER = serviceReq.PART_EXAM_EAR_RIGHT_WHISPER;
                this.HisServiceReqView.PART_EXAM_EAR_LEFT_NORMAL = serviceReq.PART_EXAM_EAR_LEFT_NORMAL;
                this.HisServiceReqView.PART_EXAM_EAR_LEFT_WHISPER = serviceReq.PART_EXAM_EAR_LEFT_WHISPER;
                this.HisServiceReqView.PART_EXAM_HORIZONTAL_SIGHT = serviceReq.PART_EXAM_HORIZONTAL_SIGHT;
                this.HisServiceReqView.PART_EXAM_VERTICAL_SIGHT = serviceReq.PART_EXAM_VERTICAL_SIGHT;
                this.HisServiceReqView.PART_EXAM_EYE_BLIND_COLOR = serviceReq.PART_EXAM_EYE_BLIND_COLOR;
                this.HisServiceReqView.PART_EXAM_UPPER_JAW = serviceReq.PART_EXAM_UPPER_JAW;
                this.HisServiceReqView.PART_EXAM_LOWER_JAW = serviceReq.PART_EXAM_LOWER_JAW;
                this.HisServiceReqView.PART_EXAM_NOSE = serviceReq.PART_EXAM_NOSE;
                this.HisServiceReqView.PART_EXAM_THROAT = serviceReq.PART_EXAM_THROAT;
                this.HisServiceReqView.PART_EXAM_STOMATOLOGY = serviceReq.PART_EXAM_STOMATOLOGY;
                this.HisServiceReqView.PART_EXAM_EYE = serviceReq.PART_EXAM_EYE;
                this.HisServiceReqView.PART_EXAM_EYE_TENSION_LEFT = serviceReq.PART_EXAM_EYE_TENSION_LEFT;
                this.HisServiceReqView.PART_EXAM_EYE_TENSION_RIGHT = serviceReq.PART_EXAM_EYE_TENSION_RIGHT;
                this.HisServiceReqView.PART_EXAM_EYESIGHT_LEFT = serviceReq.PART_EXAM_EYESIGHT_LEFT;
                this.HisServiceReqView.PART_EXAM_EYESIGHT_RIGHT = serviceReq.PART_EXAM_EYESIGHT_RIGHT;
                this.HisServiceReqView.PART_EXAM_EYESIGHT_GLASS_LEFT = serviceReq.PART_EXAM_EYESIGHT_GLASS_LEFT;
                this.HisServiceReqView.PART_EXAM_EYESIGHT_GLASS_RIGHT = serviceReq.PART_EXAM_EYESIGHT_GLASS_RIGHT;
                this.HisServiceReqView.PART_EXAM_OEND = serviceReq.PART_EXAM_OEND;
                this.HisServiceReqView.SUBCLINICAL = serviceReq.SUBCLINICAL;
                this.HisServiceReqView.TREATMENT_INSTRUCTION = serviceReq.TREATMENT_INSTRUCTION;
                this.HisServiceReqView.NOTE = serviceReq.NOTE;
                this.HisServiceReqView.PART_EXAM_DERMATOLOGY = serviceReq.PART_EXAM_DERMATOLOGY;
                this.HisServiceReqView.PART_EXAM_DERMATOLOGY = serviceReq.PART_EXAM_DERMATOLOGY;
                this.HisServiceReqView.PROVISIONAL_DIAGNOSIS = serviceReq.PROVISIONAL_DIAGNOSIS;
                this.HisServiceReqView.PATHOLOGICAL_HISTORY_FAMILY = serviceReq.PATHOLOGICAL_HISTORY_FAMILY;
                this.HisServiceReqView.PATHOLOGICAL_HISTORY = serviceReq.PATHOLOGICAL_HISTORY;

                this.HisServiceReqView.NEXT_TREAT_INTR_CODE = serviceReq.NEXT_TREAT_INTR_CODE;
                this.HisServiceReqView.NEXT_TREATMENT_INSTRUCTION = serviceReq.NEXT_TREATMENT_INSTRUCTION;

                this.HisServiceReqView.HEALTH_EXAM_RANK_ID = serviceReq.HEALTH_EXAM_RANK_ID;

                this.HisServiceReqView.ICD_CODE = serviceReq.ICD_CODE;
                this.HisServiceReqView.ICD_NAME = serviceReq.ICD_NAME;
                this.HisServiceReqView.ICD_SUB_CODE = serviceReq.ICD_SUB_CODE;
                this.HisServiceReqView.ICD_CAUSE_CODE = serviceReq.ICD_CAUSE_CODE;
                this.HisServiceReqView.ICD_CAUSE_NAME = serviceReq.ICD_CAUSE_NAME;
                this.HisServiceReqView.ICD_TEXT = serviceReq.ICD_TEXT;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private HIS_TREATMENT GetPreviousTreatmentByPatientID(long patientID, long currentTreatmentID)
        {
            HIS_TREATMENT result = null;
            try
            {
                if (patientID <= 0)
                    return null;
                HisTreatmentFilter filter = new HisTreatmentFilter();
                filter.PATIENT_ID = patientID;
                var apiResult = new BackendAdapter(new CommonParam()).Get<List<HIS_TREATMENT>>("api/HisTreatment/Get", ApiConsumers.MosConsumer, filter, new CommonParam());
                if (apiResult !=null && apiResult.Count()>0)
                {
                    result = apiResult.Where(o => o.ID != currentTreatmentID).OrderByDescending(s => s.OUT_TIME).First();
                }
            }
            catch (Exception ex)
            {
                result = null;
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }

        private V_HIS_SERVICE_REQ GetServiceReq_KhamChinh_ByTreatmentID(long treatmentID)
        {
            V_HIS_SERVICE_REQ result = null;
            try
            {
                if (treatmentID <= 0)
                    return null;
                HisServiceReqViewFilter filter = new HisServiceReqViewFilter();
                filter.TREATMENT_ID = treatmentID;
                filter.SERVICE_REQ_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH;
                var apiResult = new BackendAdapter(new CommonParam()).Get<List<V_HIS_SERVICE_REQ>>("api/HisServiceReq/GetView", ApiConsumers.MosConsumer, filter, new CommonParam());
                if (apiResult != null && apiResult.Count() > 0)
                {
                    result = apiResult.Where(o => o.IS_MAIN_EXAM == 1).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                result = null;
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }

        private void LoadClsSereServ()
        {
            try
            {
                WaitingManager.Show();
                HisSereServFilter ClsSereServFilter = new HisSereServFilter();
                ClsSereServFilter.TREATMENT_ID = this.HisServiceReqView.TREATMENT_ID;
                ClsSereServFilter.TDL_SERVICE_TYPE_IDs = new List<long>() { IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__XN,
                IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__CDHA,
                IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__NS,
                IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__SA,
                IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__TDCN,
                };
                ClsSereServFilter.HAS_EXECUTE = true;
                CommonParam param = new CommonParam();
                this.ClsSereServ = new BackendAdapter(param)
                    .Get<List<MOS.EFMODEL.DataModels.HIS_SERE_SERV>>("api/HisSereServ/Get", ApiConsumers.MosConsumer, ClsSereServFilter, param);
                WaitingManager.Hide();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private async Task FillDataAllergenic()
        {
            try
            {
                CommonParam param = new CommonParam();
                HisAllergenicViewFilter filter = new HisAllergenicViewFilter();
                filter.TDL_PATIENT_ID = this.HisServiceReqView.TDL_PATIENT_ID;
                List<V_HIS_ALLERGENIC> listAllergenic = await new BackendAdapter(param).GetAsync<List<V_HIS_ALLERGENIC>>("api/HisAllergenic/GetView", ApiConsumers.MosConsumer, filter, param);

                gridControlDiUng.BeginUpdate();
                gridControlDiUng.DataSource = listAllergenic;
                gridControlDiUng.EndUpdate();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
     
        private void FillDataToControlEditor()
        {
            try
            {
                if (this.HisServiceReqView != null)
                {
                    EnableButtonByServiceReq(this.HisServiceReqView.SERVICE_REQ_STT_ID);
                    Inventec.Common.Logging.LogSystem.Debug("this.HisServiceReqView.HOSPITALIZATION_REASON" + this.HisServiceReqView.HOSPITALIZATION_REASON);
                    Inventec.Common.Logging.LogSystem.Debug("this.treatment.HOSPITALIZATION_REASON" + this.treatment.HOSPITALIZATION_REASON);
                    if (this.treatment != null)
                    {
                        refreshClick(this.treatment.ID);
                    }
                    
                    if (string.IsNullOrEmpty(txtHospitalizationReason.Text.Trim()) && !string.IsNullOrEmpty(this.HisServiceReqView.HOSPITALIZATION_REASON))
                        txtHospitalizationReason.Text = this.HisServiceReqView.HOSPITALIZATION_REASON;
                    //else if (this.treatment != null && !string.IsNullOrEmpty(this.treatment.HOSPITALIZATION_REASON))
                    //{
                    //    txtHospitalizationReason.Text = this.treatment.HOSPITALIZATION_REASON;
                    //}

                    if (this.HisServiceReqView.SICK_DAY.HasValue)
                        spinNgayThuCuaBenh.EditValue = this.HisServiceReqView.SICK_DAY;//TODO

                    if (!string.IsNullOrEmpty(this.HisServiceReqView.PATHOLOGICAL_PROCESS))
                        txtPathologicalProcess.Text = this.HisServiceReqView.PATHOLOGICAL_PROCESS;

                    if (!string.IsNullOrEmpty(this.HisServiceReqView.FULL_EXAM))
                        txtKhamToanThan.Text = this.HisServiceReqView.FULL_EXAM;
                    if (!string.IsNullOrEmpty(this.HisServiceReqView.PART_EXAM))
                        txtKhamBoPhan.Text = this.HisServiceReqView.PART_EXAM;
                    if (!string.IsNullOrEmpty(this.HisServiceReqView.PART_EXAM_CIRCULATION))
                        txtTuanHoan.Text = this.HisServiceReqView.PART_EXAM_CIRCULATION;
                    if (!string.IsNullOrEmpty(this.HisServiceReqView.PART_EXAM_RESPIRATORY))
                        txtHoHap.Text = this.HisServiceReqView.PART_EXAM_RESPIRATORY;
                    if (!string.IsNullOrEmpty(this.HisServiceReqView.PART_EXAM_DIGESTION))
                        txtTieuHoa.Text = this.HisServiceReqView.PART_EXAM_DIGESTION;
                    if (!string.IsNullOrEmpty(this.HisServiceReqView.PART_EXAM_KIDNEY_UROLOGY))
                        txtThanTietNieu.Text = this.HisServiceReqView.PART_EXAM_KIDNEY_UROLOGY;
                    if (!string.IsNullOrEmpty(this.HisServiceReqView.PART_EXAM_MENTAL))
                        txtPartExamMental.Text = this.HisServiceReqView.PART_EXAM_MENTAL;
                    if (!string.IsNullOrEmpty(this.HisServiceReqView.PART_EXAM_NUTRITION))
                        txtPartExamNutrition.Text = this.HisServiceReqView.PART_EXAM_NUTRITION;
                    if (!string.IsNullOrEmpty(this.HisServiceReqView.PART_EXAM_MOTION))
                        txtPartExamMotion.Text = this.HisServiceReqView.PART_EXAM_MOTION;
                    if (!string.IsNullOrEmpty(this.HisServiceReqView.PART_EXAM_OBSTETRIC))
                        txtPartExanObstetric.Text = this.HisServiceReqView.PART_EXAM_OBSTETRIC;
                    if (!string.IsNullOrEmpty(this.HisServiceReqView.PART_EXAM_NEUROLOGICAL))
                        txtThanKinh.Text = this.HisServiceReqView.PART_EXAM_NEUROLOGICAL;
                    if (!string.IsNullOrEmpty(this.HisServiceReqView.PART_EXAM_MUSCLE_BONE))
                        txtCoXuongKhop.Text = this.HisServiceReqView.PART_EXAM_MUSCLE_BONE;
                    if (!string.IsNullOrEmpty(this.HisServiceReqView.PART_EXAM_EAR))
                        txtTai.Text = this.HisServiceReqView.PART_EXAM_EAR;

                    if (!string.IsNullOrEmpty(this.HisServiceReqView.PART_EXAM_EAR_RIGHT_NORMAL))
                        txtPART_EXAM_EAR_RIGHT_NORMAL.Text = this.HisServiceReqView.PART_EXAM_EAR_RIGHT_NORMAL;
                    if (!string.IsNullOrEmpty(this.HisServiceReqView.PART_EXAM_EAR_RIGHT_WHISPER))
                        txtPART_EXAM_EAR_RIGHT_WHISPER.Text = this.HisServiceReqView.PART_EXAM_EAR_RIGHT_WHISPER;
                    if (!string.IsNullOrEmpty(this.HisServiceReqView.PART_EXAM_EAR_LEFT_NORMAL))
                        txtPART_EXAM_EAR_LEFT_NORMAL.Text = this.HisServiceReqView.PART_EXAM_EAR_LEFT_NORMAL;
                    if (!string.IsNullOrEmpty(this.HisServiceReqView.PART_EXAM_EAR_LEFT_WHISPER))
                        txtPART_EXAM_EAR_LEFT_WHISPER.Text = this.HisServiceReqView.PART_EXAM_EAR_LEFT_WHISPER;

                    if (this.HisServiceReqView.PART_EXAM_HORIZONTAL_SIGHT == 1)
                        chkPART_EXAM_HORIZONTAL_SIGHT__BT.Checked = true;
                    else if (this.HisServiceReqView.PART_EXAM_HORIZONTAL_SIGHT == 2)
                        chkPART_EXAM_HORIZONTAL_SIGHT__HC.Checked = true;
                    else
                    {
                        chkPART_EXAM_HORIZONTAL_SIGHT__BT.Checked = false;
                        chkPART_EXAM_HORIZONTAL_SIGHT__HC.Checked = false;
                    }

                    if (this.HisServiceReqView.PART_EXAM_VERTICAL_SIGHT == 1)
                        chkPART_EXAM_VERTICAL_SIGHT__BT.Checked = true;
                    else if (this.HisServiceReqView.PART_EXAM_VERTICAL_SIGHT == 2)
                        chkPART_EXAM_VERTICAL_SIGHT__HC.Checked = true;
                    else
                    {
                        chkPART_EXAM_VERTICAL_SIGHT__BT.Checked = false;
                        chkPART_EXAM_VERTICAL_SIGHT__HC.Checked = false;
                    }

                    //- Sắc giác: PART_EXAM_EYE_BLIND_COLOR lưu vào HIS_SERVICE_REQ: 1-bình thường, 2- mù màu tòan bộ , 3- mù màu đỏ, 4- mù màu xanh lá, 5- mù màu vàng, 6- mù màu đỏ +xanh lá, 7 - mù màu đỏ + vàng, 8- mù màu xanh lá + vàng, 9- mù màu đỏ + xanh lá + vàng
                    //* nếu tích bình thường --->không được tính mù màu + mù màu 1 phần.
                    //* nếu tích mù toàn bộ--->cũng không được tính mù 1 phần + bình thường
                    //* nếu k tích 2 TH bình thường và mù màu toàn bộ - >thì 3 cái mù ở dưới cho phép chọn nhiều

                    chkPART_EXAM_EYE_BLIND_COLOR__BT.Checked = (this.HisServiceReqView.PART_EXAM_EYE_BLIND_COLOR == 1);
                    chkPART_EXAM_EYE_BLIND_COLOR__MMTB.Checked = (this.HisServiceReqView.PART_EXAM_EYE_BLIND_COLOR == 2);

                    chkPART_EXAM_EYE_BLIND_COLOR__MMD.Checked = (this.HisServiceReqView.PART_EXAM_EYE_BLIND_COLOR == 3 || this.HisServiceReqView.PART_EXAM_EYE_BLIND_COLOR == 6 || this.HisServiceReqView.PART_EXAM_EYE_BLIND_COLOR == 7 || this.HisServiceReqView.PART_EXAM_EYE_BLIND_COLOR == 9);
                    chkPART_EXAM_EYE_BLIND_COLOR__MMXLC.Checked = (this.HisServiceReqView.PART_EXAM_EYE_BLIND_COLOR == 4 || this.HisServiceReqView.PART_EXAM_EYE_BLIND_COLOR == 6 || this.HisServiceReqView.PART_EXAM_EYE_BLIND_COLOR == 8 || this.HisServiceReqView.PART_EXAM_EYE_BLIND_COLOR == 9);
                    chkPART_EXAM_EYE_BLIND_COLOR__MMV.Checked = (this.HisServiceReqView.PART_EXAM_EYE_BLIND_COLOR == 5 || this.HisServiceReqView.PART_EXAM_EYE_BLIND_COLOR == 7 || this.HisServiceReqView.PART_EXAM_EYE_BLIND_COLOR == 8 || this.HisServiceReqView.PART_EXAM_EYE_BLIND_COLOR == 9);

                    if (!string.IsNullOrEmpty(this.HisServiceReqView.PART_EXAM_UPPER_JAW))
                        txtPART_EXAM_UPPER_JAW.Text = this.HisServiceReqView.PART_EXAM_UPPER_JAW;
                    if (!string.IsNullOrEmpty(this.HisServiceReqView.PART_EXAM_LOWER_JAW))
                        txtPART_EXAM_LOWER_JAW.Text = this.HisServiceReqView.PART_EXAM_LOWER_JAW;

                    if (!string.IsNullOrEmpty(this.HisServiceReqView.PART_EXAM_NOSE))
                        txtMui.Text = this.HisServiceReqView.PART_EXAM_NOSE;
                    if (!string.IsNullOrEmpty(this.HisServiceReqView.PART_EXAM_THROAT))
                        txtHong.Text = this.HisServiceReqView.PART_EXAM_THROAT;
                    if (!string.IsNullOrEmpty(this.HisServiceReqView.PART_EXAM_STOMATOLOGY))
                        txtRHM.Text = this.HisServiceReqView.PART_EXAM_STOMATOLOGY;
                    if (!string.IsNullOrEmpty(this.HisServiceReqView.PART_EXAM_EYE))
                        txtMat.Text = this.HisServiceReqView.PART_EXAM_EYE;
                    if (!string.IsNullOrEmpty(this.HisServiceReqView.PART_EXAM_EYE_TENSION_LEFT))
                        txtNhanApTrai.Text = this.HisServiceReqView.PART_EXAM_EYE_TENSION_LEFT;
                    if (!string.IsNullOrEmpty(this.HisServiceReqView.PART_EXAM_EYE_TENSION_RIGHT))
                        txtNhanApPhai.Text = this.HisServiceReqView.PART_EXAM_EYE_TENSION_RIGHT;
                    if (!string.IsNullOrEmpty(this.HisServiceReqView.PART_EXAM_EYESIGHT_LEFT))
                        txtThiLucKhongKinhTrai.Text = this.HisServiceReqView.PART_EXAM_EYESIGHT_LEFT;
                    if (!string.IsNullOrEmpty(this.HisServiceReqView.PART_EXAM_EYESIGHT_RIGHT))
                        txtThiLucKhongKinhPhai.Text = this.HisServiceReqView.PART_EXAM_EYESIGHT_RIGHT;
                    if (!string.IsNullOrEmpty(this.HisServiceReqView.PART_EXAM_EYESIGHT_GLASS_LEFT))
                        txtThiLucCoKinhTrai.Text = this.HisServiceReqView.PART_EXAM_EYESIGHT_GLASS_LEFT;
                    if (!string.IsNullOrEmpty(this.HisServiceReqView.PART_EXAM_EYESIGHT_GLASS_RIGHT))
                        txtThiLucCoKinhPhai.Text = this.HisServiceReqView.PART_EXAM_EYESIGHT_GLASS_RIGHT;
                    if (!string.IsNullOrEmpty(this.HisServiceReqView.PART_EXAM_OEND))
                        txtNoiTiet.Text = this.HisServiceReqView.PART_EXAM_OEND;
                    if (!string.IsNullOrEmpty(this.HisServiceReqView.SUBCLINICAL))
                        txtSubclinical.Text = this.HisServiceReqView.SUBCLINICAL;
                    if (!string.IsNullOrEmpty(this.HisServiceReqView.TREATMENT_INSTRUCTION))
                        txtTreatmentInstruction.Text = this.HisServiceReqView.TREATMENT_INSTRUCTION;
                    if (!string.IsNullOrEmpty(this.HisServiceReqView.NOTE))
                        txtResultNote.Text = this.HisServiceReqView.NOTE;

                    if (!string.IsNullOrEmpty(this.HisServiceReqView.PART_EXAM_DERMATOLOGY))
                        txtDaLieu.Text = this.HisServiceReqView.PART_EXAM_DERMATOLOGY;

                    if (!string.IsNullOrEmpty(this.HisServiceReqView.PROVISIONAL_DIAGNOSIS))
                    {
                        txtProvisionalDianosis.Text = this.HisServiceReqView.PROVISIONAL_DIAGNOSIS;
                    }

                    if (!string.IsNullOrEmpty(this.HisServiceReqView.PATHOLOGICAL_HISTORY_FAMILY))
                        txtPathologicalHistoryFamily.Text = this.HisServiceReqView.PATHOLOGICAL_HISTORY_FAMILY;
                    else if (ltreatment2 != null && !string.IsNullOrEmpty(ltreatment2.PT_PATHOLOGICAL_HISTORY_FAMILY))
                        txtPathologicalHistoryFamily.Text = ltreatment2.PT_PATHOLOGICAL_HISTORY_FAMILY;

                    if (!string.IsNullOrEmpty(this.HisServiceReqView.PATHOLOGICAL_HISTORY))
                        txtPathologicalHistory.Text = this.HisServiceReqView.PATHOLOGICAL_HISTORY;
                    else if (ltreatment2 != null && !string.IsNullOrEmpty(ltreatment2.PT_PATHOLOGICAL_HISTORY))
                        txtPathologicalHistory.Text = ltreatment2.PT_PATHOLOGICAL_HISTORY;

                    txtHospitalizationReason.Focus();
                    txtHospitalizationReason.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void refreshClick(long id)
        {
            try
            {
                Inventec.Common.Logging.LogSystem.Debug("FillDataToControlEditor: ");
                CommonParam param = new CommonParam();
                HisTreatmentFilter filter = new HisTreatmentFilter();
                filter.ID = id;
                var treatment = new BackendAdapter(param).Get<List<HIS_TREATMENT>>("api/HisTreatment/Get", ApiConsumers.MosConsumer, filter, param).ToList();
                if (treatment != null && treatment.Count > 0)
                {
                    txtHospitalizationReason.Text = treatment.FirstOrDefault().HOSPITALIZATION_REASON;
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public async Task LoadDHSTFromServiceReq()
        {
            try
            {
                if (this.HisServiceReqView.DHST_ID.HasValue)
                {
                    WaitingManager.Show();
                    HisDhstFilter dhstFilter = new HisDhstFilter();
                    dhstFilter.ID = this.HisServiceReqView.DHST_ID;
                    dhstFilter.ORDER_FIELD = "EXECUTE_TIME";
                    dhstFilter.ORDER_DIRECTION = "DESC";
                    HIS_DHST currentDhst = new HIS_DHST();
                    CommonParam param = new CommonParam();
                    var listDHST = await new BackendAdapter(param)
                .GetAsync<List<MOS.EFMODEL.DataModels.HIS_DHST>>("api/HisDHST/Get", ApiConsumers.MosConsumer, dhstFilter, param);
                    currentDhst = listDHST != null ? listDHST.FirstOrDefault() : null;
                    WaitingManager.Hide();
                    //dhstProcessor.SetValue(ucDHST, currentDhst);
                    this.DHSTSetValue(currentDhst);
                    Inventec.Common.Logging.LogSystem.Debug("Get dhst from HisServiceReq");
                }
                else if (this.treatment != null)
                {
                    WaitingManager.Show();
                    HisDhstFilter dhstFilter = new HisDhstFilter();
                    dhstFilter.TREATMENT_ID = this.treatment.ID;
                    dhstFilter.ORDER_FIELD = "EXECUTE_TIME";
                    dhstFilter.ORDER_DIRECTION = "DESC";
                    HIS_DHST currentDhst = new HIS_DHST();
                    CommonParam param = new CommonParam();
                    var listDHST = await new BackendAdapter(param)
                .GetAsync<List<MOS.EFMODEL.DataModels.HIS_DHST>>("api/HisDHST/Get", ApiConsumers.MosConsumer, dhstFilter, param);
                    currentDhst = listDHST != null ? listDHST.FirstOrDefault() : null;
                    WaitingManager.Hide();
                    //dhstProcessor.SetValue(ucDHST, currentDhst);
                    this.DHSTSetValue(currentDhst);
                    Inventec.Common.Logging.LogSystem.Debug("Get dhst from treatment");
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }


        private void LoadNextTreatmentIntructionToControl(string nextTreaIntrCode, string nextTreaIntrName)
        {
            try
            {
                //UC.NextTreatmentInstruction.ADO.NextTreatmentInstructionInputADO nextTreatIntr = new UC.NextTreatmentInstruction.ADO.NextTreatmentInstructionInputADO();
                //nextTreatIntr.NEXT_TREA_INTR_CODE = nextTreaIntrCode;
                //nextTreatIntr.NEXT_TREA_INTR_NAME = nextTreaIntrName;
                //if (this.ucNextTreatmentIntruction != null)
                //{
                //    this.nextTreatmentIntructionProcessor.Reload(this.ucNextTreatmentIntruction, nextTreatIntr);
                //}
                this.txtNextTreatmentInstructionCode.Text = nextTreaIntrCode;
                this.cboNextTreatmentInstructions.Text = nextTreaIntrName;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void LoadSdaConfig()
        {
            try
            {
                this.requiredControl = Inventec.Common.TypeConvert.Parse.ToInt64(HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(SdaConfigKeys.CONTROL_REQUIRED));
                this.checkSameHeinCFG = Inventec.Common.TypeConvert.Parse.ToInt64(HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(SdaConfigKeys.CHECK_SAME_HEIN));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private HIS_SERVICE_REQ GetServiceReq()
        {
            HIS_SERVICE_REQ result = null;
            try
            {
                CommonParam param = new CommonParam();
                HisServiceReqFilter serviceReqFilter = new HisServiceReqFilter();
                serviceReqFilter.ID = this.HisServiceReqView.ID;
                result = new BackendAdapter(param)
                    .Get<List<MOS.EFMODEL.DataModels.HIS_SERVICE_REQ>>("api/HisServiceReq/Get", ApiConsumers.MosConsumer, serviceReqFilter, param).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }

        private void LoadUCToPanelExecuteExt(UserControl uc, CheckEdit checkEdit)
        {
            try
            {
                this.panelExecuteExt.Controls.Clear();
                if (uc != null && checkEdit != null)
                {
                    lciNotice.Visibility = checkEdit.Checked ? DevExpress.XtraLayout.Utils.LayoutVisibility.Never : DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    lciIconNotice.Visibility = checkEdit.Checked ? DevExpress.XtraLayout.Utils.LayoutVisibility.Never : DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                    uc.Dock = DockStyle.Fill;
                    this.panelExecuteExt.Controls.Add(uc);
                }
                else if (uc == null && checkEdit != null && !checkEdit.Checked)
                {
                    lciNotice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    lciIconNotice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
                else
                {
                    lciNotice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lciIconNotice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }

                this.CreateThreadProcessHideControl();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ResetPrintExecuteExt()
        {
            try
            {
                btnSaveFinish.Enabled = true;
                btnPrint_ExamService.DropDownControl = null;
                FillDataToButtonPrintAndAutoPrint();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void EnableButtonByServiceReq(long serviceReqSTTId)
        {
            try
            {
                if (serviceReqSTTId != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT)
                {
                    lblCaptionHospitalizationReason.Enabled = true;
                    lblCaptionNgayThuCuaBenh.Enabled = true;
                    lblCaptionPathologicalProcess.Enabled = true;
                    lblCaptionPathologicalHistory.Enabled = true;
                    lblCaptionPathologicalHistoryFamily.Enabled = true;
                    lciKhamToanThan.Enabled = true;
                    lblInformationExam.Enabled = true;
                    lblCaptionDiagnostic.Enabled = true;
                    lblCaptionConclude.Enabled = true;
                    panelNextTreatmentIntruction.Enabled = true;
                    lciChuY.Enabled = true;
                    UcSecondaryIcdReadOnly(false);
                    groupControlTreatmentFinish.Enabled = true;
                    drBtnOther.Enabled = true;
                    btnChooseTemplate.Enabled = true;
                    btnAssignService.Enabled = true;
                    btnAssignPre.Enabled = true;
                    btnKeDonYHCT.Enabled = true;
                    btnTuTruc.Enabled = true;
                    btnAssignPaan.Enabled = true;
                    btnServiceReqList.Enabled = true;
                    btnContentSubclinical.Enabled = true;
                    btnSaveFinish.Enabled = true;
                    lcgDHST.Enabled = true;
                    lblSick.Enabled = true;
                    gridControlTreatmentHistory.Enabled = true;
                    gridControlDiUng.Enabled = true;
                    panelIcd.Enabled = true;
                    panelControlCauseIcd.Enabled = true;
                    btnViewInformationExam.Enabled = true;
                }
                else
                {
                    lcgDHST.Enabled = false;
                    lblCaptionHospitalizationReason.Enabled = false;
                    lblCaptionNgayThuCuaBenh.Enabled = false;
                    lblCaptionPathologicalProcess.Enabled = false;
                    lblCaptionPathologicalHistory.Enabled = false;
                    lblCaptionPathologicalHistoryFamily.Enabled = false;
                    lciKhamToanThan.Enabled = false;
                    lblInformationExam.Enabled = false;
                    lblCaptionDiagnostic.Enabled = false;
                    lblCaptionConclude.Enabled = false;
                    panelNextTreatmentIntruction.Enabled = false;
                    lciChuY.Enabled = false;
                    lblSick.Enabled = false;
                    panelIcd.Enabled = false;
                    panelControlCauseIcd.Enabled = false;
                    groupControlTreatmentFinish.Enabled = false;
                    //drBtnOther.Enabled = false;
                    btnChooseTemplate.Enabled = false;
                    gridControlTreatmentHistory.Enabled = false;
                    gridControlDiUng.Enabled = false;
                    btnAssignService.Enabled = false;
                    btnAssignPre.Enabled = false;
                    btnKeDonYHCT.Enabled = false;
                    btnTuTruc.Enabled = false;
                    btnServiceReqList.Enabled = false;
                    btnContentSubclinical.Enabled = false;
                    btnSaveFinish.Enabled = false;
                    btnAssignPaan.Enabled = false;
                    UcSecondaryIcdReadOnly(true);
                    btnViewInformationExam.Enabled = false;
                }
                KeyAllowToEnableIcdSubCode();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

    }
}
