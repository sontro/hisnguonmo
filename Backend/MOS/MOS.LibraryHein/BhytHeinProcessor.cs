using Inventec.Common.Logging;
using MOS.LibraryHein.Bhyt.HeinJoin5Year;
using MOS.LibraryHein.Bhyt.HeinLevel;
using MOS.LibraryHein.Bhyt.HeinLiveArea;
using MOS.LibraryHein.Bhyt.HeinObject;
using MOS.LibraryHein.Bhyt.HeinPaid6Month;
using MOS.LibraryHein.Bhyt.HeinRatio;
using MOS.LibraryHein.Bhyt.HeinRightRoute;
using MOS.LibraryHein.Bhyt.HeinTreatmentType;
using MOS.LibraryHein.Bhyt.HeinUpToStandard;
using MOS.LibraryHein.Common;
using MOS.UTILITY;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.LibraryHein.Bhyt
{
    public class BhytHeinProcessor
    {
        private decimal baseSalary;
        private decimal secondStentPaidRatio;

        private decimal minTotal;
        private decimal maxTotalPackage;
        private  bool noConstraintRoomWithMaterialPackage;
        private List<string> noLimitHighHeinServicePrefixs;
        private List<string> noLimitMaterialMedicinePrefixs;
        private List<string> acceptedMediOrgCodes;//Cac ma KCB ma vien chap nhan la dung tuyen. De phuc vu kiem tra dung tuyen trong truong hop cap cuu, hen kham, gioi thieu. Do trong cac truong hop nay neu trai tuyen thi he thong van dang luu loai la "dung tuyen"
        private bool isNoLimitSecondStentForSpecials;
        
        public BhytHeinProcessor()
        {
            //Tham so mac dinh
            this.baseSalary = 1800000;
            this.secondStentPaidRatio = 0.5m;
            this.minTotal = baseSalary * 0.15m;
            this.maxTotalPackage = baseSalary * 45;
            this.acceptedMediOrgCodes = null;
        }

        public BhytHeinProcessor(decimal _baseSalary, decimal _minTotalBySalary, decimal _maxTotalPackageBySalary, decimal _secondStentPaidRatio, bool _noConstraintRoomWithMaterialPackage, List<string> _noLimitHighHeinServicePrefixs, List<string> _noLimitMaterialMedicinePrefixs, bool _isNoLimitSecondStentForSpecials, List<string> _acceptedMediOrgCodes)
        {
            this.baseSalary = _baseSalary;
            this.secondStentPaidRatio = _secondStentPaidRatio;

            this.minTotal = baseSalary * _minTotalBySalary;
            this.maxTotalPackage = baseSalary * _maxTotalPackageBySalary;
            this.isNoLimitSecondStentForSpecials = _isNoLimitSecondStentForSpecials;
            this.noLimitHighHeinServicePrefixs = _noLimitHighHeinServicePrefixs;
            this.noLimitMaterialMedicinePrefixs = _noLimitMaterialMedicinePrefixs;
            this.noConstraintRoomWithMaterialPackage = _noConstraintRoomWithMaterialPackage;
            this.acceptedMediOrgCodes = _acceptedMediOrgCodes;
        }

        /// <summary>
        /// Kiem tra so the BHYT co hop le hay khong
        /// </summary>
        /// <param name="heinCardNumber">So the BHYT</param>
        /// <returns>True: hop le; False: neu khong hop le</returns>
        public bool IsValidHeinCardNumber(string heinCardNumber)
        {
            //Sua de cho phep xu ly voi the BHYT moi (chi in 10 so)
            return !string.IsNullOrWhiteSpace(heinCardNumber)
                && (heinCardNumber.Length == 10 || (heinCardNumber.Length == BhytConstant.HEIN_NUMBER_LENGTH && HeinObjectBenefitStore.GetHeinBenefitFromCardNumber(heinCardNumber) != null));
        }

        public decimal? GetDefaultHeinRatio(string treatmentTypeCode, string heinCardNumber, string levelCode, string rightRouteCode, decimal totalPrice)
        {
            bool notOverLimit = totalPrice < this.minTotal;
            return this.GetDefaultHeinRatio(treatmentTypeCode, heinCardNumber, levelCode, rightRouteCode, notOverLimit);
        }

        public decimal? GetDefaultHeinRatio(string treatmentTypeCode, string heinCardNumber, string levelCode, string rightRouteCode, bool notOverLimit)
        {
            BhytPatientTypeData patientTypeData = new BhytPatientTypeData();
            patientTypeData.JOIN_5_YEAR = HeinJoin5YearCode.FALSE; //gia tri mac dinh la chua dat 5 nam/6 thang
            patientTypeData.PAID_6_MONTH = HeinPaid6MonthCode.FALSE; //gia tri mac dinh la chua dat 5 nam/6 thang
            patientTypeData.LEVEL_CODE = levelCode;
            patientTypeData.RIGHT_ROUTE_CODE = rightRouteCode;
            return this.GetHeinRatio(treatmentTypeCode, heinCardNumber, HeinRatioTypeCode.NORMAL, patientTypeData, notOverLimit, null);
        }

        /// <summary>
        /// Lay muc huong tuong ung voi the
        /// </summary>
        /// <param name="treatmentTypeCode"></param>
        /// <param name="heinCardNumber"></param>
        /// <param name="levelCode"></param>
        /// <param name="rightRouteCode"></param>
        /// <returns></returns>
        public decimal? GetDefaultHeinRatio(string treatmentTypeCode, string heinCardNumber, string levelCode, string rightRouteCode)
        {
            return this.GetDefaultHeinRatio(treatmentTypeCode, heinCardNumber, levelCode, rightRouteCode, false);
        }

        public decimal? GetDefaultHeinRatio(string heinCardNumber)
        {
            return this.GetDefaultHeinRatio(HeinTreatmentTypeCode.EXAM, heinCardNumber, HeinLevelCode.DISTRICT, HeinRightRouteCode.TRUE, false);
        }

        /// <summary>
        /// Lay ti le BHYT tuong ung voi cac tham so truyen vao
        /// </summary>
        /// <param name="heinCardNumber"></param>
        /// <param name="serviceData"></param>
        /// <param name="patientTypeData"></param>
        /// <returns></returns>
        public decimal? GetDefaultHeinRatio(string treatmentTypeCode, string heinCardNumber, BhytPatientTypeData patientTypeData, decimal totalPrice)
        {
            BhytServiceRequestData data = new BhytServiceRequestData(patientTypeData, HeinRatioTypeCode.NORMAL);
            //Lay ra hein_object_id dua vao so the BHYT
            HeinBenefitData heinBenefitData = HeinObjectBenefitStore.GetHeinBenefitFromCardNumber(heinCardNumber);
            if (heinBenefitData == null)
            {
                LogSystem.Error("Khong ton tai ma quyen loi tuong ung voi so the: " + heinCardNumber);
                return null;
            }
            bool notOverLimit = totalPrice < this.minTotal;
            return GetHeinRatio(treatmentTypeCode, heinBenefitData.HeinBenefitCode, data, notOverLimit, null);
        }

        /// <summary>
        /// Lay ti le BHYT tuong ung voi cac tham so truyen vao
        /// </summary>
        /// <param name="heinCardNumber"></param>
        /// <param name="serviceData"></param>
        /// <param name="patientTypeData"></param>
        /// <returns></returns>
        private decimal? GetHeinRatio(string treatmentTypeCode, string heinCardNumber, string heinRatioTypeCode, BhytPatientTypeData patientTypeData, bool notOverLimit, BhytServiceRequestData service)
        {
            BhytServiceRequestData data = null;
            if (service == null)
            {
                data = new BhytServiceRequestData(patientTypeData, heinRatioTypeCode);
            }
            else
            {
                data = service;
                data.PatientTypeData = patientTypeData;
                data.HeinRatioTypeCode = heinRatioTypeCode;
            }

            //Lay ra hein_object_id dua vao so the BHYT
            HeinBenefitData heinBenefitData = HeinObjectBenefitStore.GetHeinBenefitFromCardNumber(heinCardNumber);
            if (heinBenefitData == null)
            {
                LogSystem.Error("Khong ton tai ma quyen loi tuong ung voi so the: " + heinCardNumber);
                return null;
            }
            return GetHeinRatio(treatmentTypeCode, heinBenefitData.HeinBenefitCode, data, notOverLimit, acceptedMediOrgCodes);
        }

        /// <summary>
        /// Lay ti le BHYT tuong ung voi cac tham so truyen vao
        /// </summary>
        /// <param name="benefitCode">Ma quyen loi</param>
        /// <param name="serviceData"></param>
        /// <param name="patientTypeData"></param>
        /// <param name="notOverLimit"></param>
        /// <returns></returns>
        private static decimal? GetHeinRatio(string treatmentTypeCode, string benefitCode, BhytServiceRequestData service, bool notOverLimit, List<string> acceptedMediOrgCodes)
        {

            //cac thong tin nay bat buoc nhap
            if (string.IsNullOrEmpty(benefitCode) || service == null || service.HeinRatioTypeCode == null || service.PatientTypeData == null)
            {
                return 0;
            }

            BhytPatientTypeData patientTypeData = service.PatientTypeData;

            string heinRightRouteCode = GetRightRouteCode(patientTypeData, treatmentTypeCode);

            decimal? result = null;

            //Neu ko truyen vao thi lay theo ngay dien doi tuong
            if (service.InstructionTime == 0)
            {
                //Neu dien doi tuong ko co ngay thi lay theo ngay hien tai
                if (patientTypeData.LOG_TIME == 0)
                {
                    patientTypeData.LOG_TIME = Inventec.Common.DateTime.Get.Now().Value;
                }
                service.InstructionTime = patientTypeData.LOG_TIME;
            }

            //Tu 01/01/2021 ap dung muc huong moi voi BN trai tuyen tuyen tinh
            if (service.InstructionTime >= 20210101000000)
            {
                result = HeinRatioStoreNew.HEIN_RATIO_STORE
                .Where(o => o.HeinBenefitCode.Equals(benefitCode)
                    && o.HeinRatioTypeCode.Equals(service.HeinRatioTypeCode)
                    && o.HeinLevel.Equals(patientTypeData.LEVEL_CODE)
                    && (o.TreatmentTypeCode == null || o.TreatmentTypeCode.Trim().Equals("") || o.TreatmentTypeCode.Equals(treatmentTypeCode))
                    && (o.UpToStandardCode == null || o.UpToStandardCode.Trim().Equals("") || o.UpToStandardCode == HeinUpToStandardStore.GetHeinUpToStandardCode(patientTypeData.JOIN_5_YEAR, patientTypeData.PAID_6_MONTH, patientTypeData.RIGHT_ROUTE_TYPE_CODE, patientTypeData.HEIN_MEDI_ORG_CODE, acceptedMediOrgCodes))
                    && (o.RightRouteCode == null || o.RightRouteCode.Trim().Equals("") || o.RightRouteCode == heinRightRouteCode)
                    && (o.NotOverLimit == null || o.NotOverLimit.Value == notOverLimit))
                .Select(o => o.Ratio)
                .OrderByDescending(o => o)
                .FirstOrDefault();
            }
            else
            {
                result = HeinRatioStore.HEIN_RATIO_STORE
                .Where(o => o.HeinBenefitCode.Equals(benefitCode)
                    && o.HeinRatioTypeCode.Equals(service.HeinRatioTypeCode)
                    && o.HeinLevel.Equals(patientTypeData.LEVEL_CODE)
                    && (o.TreatmentTypeCode == null || o.TreatmentTypeCode.Trim().Equals("") || o.TreatmentTypeCode.Equals(treatmentTypeCode))
                    && (o.UpToStandardCode == null || o.UpToStandardCode.Trim().Equals("") || o.UpToStandardCode == HeinUpToStandardStore.GetHeinUpToStandardCode(patientTypeData.JOIN_5_YEAR, patientTypeData.PAID_6_MONTH, patientTypeData.RIGHT_ROUTE_TYPE_CODE, patientTypeData.HEIN_MEDI_ORG_CODE, acceptedMediOrgCodes))
                    && (o.RightRouteCode == null || o.RightRouteCode.Trim().Equals("") || o.RightRouteCode == heinRightRouteCode)
                    && (o.NotOverLimit == null || o.NotOverLimit.Value == notOverLimit))
                .Select(o => o.Ratio)
                .OrderByDescending(o => o)
                .FirstOrDefault();
            }
            return result;
        }

        /// <summary>
        /// Lay ti le BHYT tuong ung voi cac tham so truyen vao
        /// </summary>
        /// <param name="benefitCode">Ma quyen loi</param>
        /// <param name="serviceData"></param>
        /// <param name="patientTypeData"></param>
        /// <param name="notOverLimit"></param>
        /// <returns></returns>
        private static decimal? GetSecondStentHeinRatio(BhytPatientTypeData patientTypeData, BhytServiceRequestData service, string treatmentTypeCode)
        {
            //cac thong tin nay bat buoc nhap
            if (patientTypeData == null)
            {
                return 0;
            }

            string heinRightRouteCode = GetRightRouteCode(patientTypeData, treatmentTypeCode);

            decimal? result = null;

            //Neu ko truyen vao thi lay theo ngay dien doi tuong
            if (service.InstructionTime == 0)
            {
                //Neu dien doi tuong ko co ngay thi lay theo ngay hien tai
                if (patientTypeData.LOG_TIME == 0)
                {
                    patientTypeData.LOG_TIME = Inventec.Common.DateTime.Get.Now().Value;
                }
                service.InstructionTime = patientTypeData.LOG_TIME;
            }

            //Tu 01/01/2021 ap dung muc huong moi voi BN trai tuyen tuyen tinh
            if (service.InstructionTime >= 20210101000000)
            {
                result = HeinRatioStoreNew.SECOND_STENT_HEIN_RATIO_STORE
                .Where(o => o.HeinLevel.Equals(patientTypeData.LEVEL_CODE)
                    && (o.RightRouteCode == null || o.RightRouteCode.Trim().Equals("") || o.RightRouteCode == heinRightRouteCode))
                .Select(o => o.Ratio)
                .OrderByDescending(o => o)
                .FirstOrDefault();
            }
            else
            {
                result = HeinRatioStore.SECOND_STENT_HEIN_RATIO_STORE
                .Where(o => o.HeinLevel.Equals(patientTypeData.LEVEL_CODE)
                    && (o.RightRouteCode == null || o.RightRouteCode.Trim().Equals("") || o.RightRouteCode == heinRightRouteCode))
                .Select(o => o.Ratio)
                .OrderByDescending(o => o)
                .FirstOrDefault();
            }
            
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="treatmentTypeCode"></param>
        /// <param name="mustSameRoomWithMaterialPackage">co bat buoc phong xu ly dich vu chi dinh vat tu thi moi cho vao goi VTYT hay ko</param>
        /// <param name="noLimitHighHeinServicePrefixs">Cac dau the khong ap dung tran goi vat tu y te</param>
        /// /// <param name="noLimitMaterialMedicinePrefixs">Cac dau the khong ap dung tran thanh toan doi voi thuoc/vat tu</param>
        /// <param name="requestDatas"></param>
        /// <returns></returns>
        public bool UpdateHeinInfo(string treatmentTypeCode, List<BhytServiceRequestData> requestDatas)
        {
            bool result = true;
            try
            {
                List<BhytServiceRequestData> heinServiceDatas = requestDatas != null ? requestDatas.Where(o => o.Amount > 0).ToList() : null;
                //Duyet toan bo dich vu, chi lay ra cac dich vu ma BHYT thanh toan (ratio > 0) de xu ly
                List<BhytServiceRequestData> acceptedList = new List<BhytServiceRequestData>();

                //Tinh tong tien de so sanh 15% thang luong toi thieu
                decimal totalPrice = 0;
                foreach (BhytServiceRequestData data in heinServiceDatas)
                {
                    //Lay ra hein_object_id dua vao so the BHYT
                    string heinCardNumber = data.PatientTypeData != null ? data.PatientTypeData.HEIN_CARD_NUMBER : null;
                    HeinBenefitData heinBenefitData = HeinObjectBenefitStore.GetHeinBenefitFromCardNumber(heinCardNumber);
                    if (heinBenefitData == null)
                    {
                        LogSystem.Warn("Khong co du lieu HeinBenefitData tuong ung voi so the " + heinCardNumber);
                        continue;
                    }

                    if (BhytHeinProcessor.GetHeinRatio(treatmentTypeCode, heinBenefitData.HeinBenefitCode, data, true, acceptedMediOrgCodes) > 0)
                    {
                        acceptedList.Add(data);
                        //Tinh tong chi phi ma BHYT thanh toan cua cac dich vu
                        //Neu la thuoc/vat tu thi luon lay theo don gia goc chu ko lay theo gia tran
                        //Neu lay theo gia tran se bi sai trong cac truong hop thuoc/vat tu thanh toan theo ty le
                        //Cac truong hop khac chua co cong van ghi cu the (va chua co vien nao phan hoi tinh sai) nen van giu nhu cu~
                        totalPrice += (data.LimitPrice.HasValue && data.ServiceType != ServiceTypeEnum.MEDICINE && data.ServiceType != ServiceTypeEnum.MATERIAL ? 
                            data.LimitPrice.Value : data.Price) * data.Amount;
                    }
                }

                //==>Da vuot so tien duoc cau hinh hay chua
                bool isNotOverLimit = totalPrice < this.minTotal;

                //Danh sach yeu cau ky thuat cao
                List<BhytServiceRequestData> highServices = new List<BhytServiceRequestData>();

                //Duyet danh sach acceptedList
                foreach (BhytServiceRequestData service in acceptedList)
                {
                    //Lay ra hein_object_id dua vao so the BHYT
                    string heinCardNumber = service.PatientTypeData != null ? service.PatientTypeData.HEIN_CARD_NUMBER : null;
                    HeinBenefitData heinBenefitData = HeinObjectBenefitStore.GetHeinBenefitFromCardNumber(heinCardNumber);

                    decimal? ratio = BhytHeinProcessor.GetHeinRatio(treatmentTypeCode, heinBenefitData.HeinBenefitCode, service, isNotOverLimit, acceptedMediOrgCodes);
                    service.HeinRatio = ratio.HasValue ? ratio.Value : 0;
                    service.HeinPrice = service.LimitPrice.HasValue && service.LimitPrice.Value < service.Price ? service.LimitPrice * ratio.Value : null;

                    //neu la dich vu ki thuat cao thi them vao danh sach rieng de xu ly tiep sau
                    if ((service.HeinRatioTypeCode != null && service.HeinRatioTypeCode.Equals(HeinRatioTypeCode.HIGH)) || service.IsHighService)
                    {
                        highServices.Add(service);
                    }
                }

                //Neu ton tai y/c ky thuat cao va doi tuong bao hiem co gioi han thanh toan BHYT cho dich vu
                //ky thuat cao thi thuc hien phan bo lai so tien BHYT chi tra cho dich vu ky thuat cao
                if (highServices != null && highServices.Count > 0)
                {
                    foreach (BhytServiceRequestData d in highServices)
                    {
                        string heinCardNumber = d.PatientTypeData != null ? d.PatientTypeData.HEIN_CARD_NUMBER : null;
                        this.ProcessHighService(d, treatmentTypeCode, heinCardNumber, heinServiceDatas, acceptedMediOrgCodes);
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private void ProcessHighService(BhytServiceRequestData parent, string treatmentTypeCode, string heinCardNumber, List<BhytServiceRequestData> heinServiceDatas, List<string> acceptedMediOrgCodes)
        {
            List<BhytServiceRequestData> attachments = heinServiceDatas != null ? heinServiceDatas
                .Where(o => o.ParentId == parent.Id
                    && o.ServiceType == ServiceTypeEnum.MATERIAL //chi lay vat tu vao goi de tinh
                    //chi lay cac vat tu do phong xu ly dv chi dinh
                    && (this.noConstraintRoomWithMaterialPackage || o.RequestRoomId == parent.ExecuteRoomId)
                    && o.Amount > 0
                ).ToList() : null;
            if (attachments != null && attachments.Count > 0)
            {
                //Lay ra danh sach cac vat tu la stent
                List<BhytServiceRequestData> stents = attachments
                    .Where(o => o.IsStent && o.Amount > 0)
                    .OrderBy(o => !o.StentOrder.HasValue)
                    .ThenBy(o => o.StentOrder)
                    .ThenBy(o => o.InstructionTime)
                    .ToList();

                //Lay ra danh sach cac dv dinh kem ko phai la stent
                List<BhytServiceRequestData> nonStentAttachments = attachments.Where(o => !o.IsStent).ToList();

                BhytServiceRequestData firstStent = stents != null && stents.Count > 0 ? stents[0] : null;
                BhytServiceRequestData secondStent = stents != null && stents.Count > 1 ? stents[1] : null;

                List<BhytServiceRequestData> otherStents = stents != null && stents.Count > 2 ? stents.Where(o => o.Id != firstStent.Id && o.Id != secondStent.Id).ToList() : null;

                //Luu danh sach dinh kem bao gom ca stent dau tien
                List<BhytServiceRequestData> attachmentWithFirstStents = new List<BhytServiceRequestData>();
                if (nonStentAttachments != null && nonStentAttachments.Count > 0)
                {
                    attachmentWithFirstStents.AddRange(nonStentAttachments);
                }
                if (firstStent != null)
                {
                    attachmentWithFirstStents.Add(firstStent);
                }

                //Tong chi phi BHYT chi tra cac dv khong tinh stent thu 2
                decimal totalAttachmentWithFirstStentHeinPrice = attachmentWithFirstStents.Sum(o => o.Amount * (o.HeinPrice.HasValue ? o.HeinPrice.Value : o.HeinRatio * o.Price));

                //Tong chi phi (tong thanh tien) cua dv khong tinh stent thu 2
                decimal totalAttachmentWithFirstStentPrice = attachmentWithFirstStents.Sum(o => o.Amount * (o.LimitPrice.HasValue ? o.LimitPrice.Value : o.Price));

                //Thuc hien cap nhat de dam bao tong so tien ma BHYT chi tra cho DV KTC (tong cua hein_price)
                //khong duoc vuot qua so tien toi da duoc quy dinh. Voi danh sach co n phan tu thi, n -1 phan tu
                //dau se duoc chia ti le thuan voi price (va co lam tron 4 chu so sau phan thap phan), va phan tu
                //cuoi cung thi se lay tong so tien max - tong so tien da duoc phan bo cho n - 1 phan tu truoc do.
                //Viec nay nham dam bao sau khi phan bo va lam tron thi tong so tien khong thay doi
                BhytPatientTypeData patientTypeData = attachments.Where(o => o.PatientTypeData != null).Select(o => o.PatientTypeData).LastOrDefault();

                decimal limit = this.GetLimitHighServicePrice(parent, treatmentTypeCode, heinCardNumber, patientTypeData, totalAttachmentWithFirstStentPrice);

                if (totalAttachmentWithFirstStentHeinPrice != limit && !this.IsNoLimitHighServicePriceTotal(heinCardNumber))
                {
                    decimal max = limit;
                    decimal sumApportionedPrice = 0;
                    for (int i = 0; i < attachmentWithFirstStents.Count - 1; i++)
                    {
                        BhytServiceRequestData t = attachmentWithFirstStents[i];
                        decimal currentHeinPrice = t.Amount * (t.HeinPrice.HasValue ? t.HeinPrice.Value : t.HeinRatio * t.Price);

                        t.HeinPrice = Math.Round((currentHeinPrice / (totalAttachmentWithFirstStentHeinPrice * t.Amount)) * max, BhytConstant.DECIMAL_PRECISION);
                        sumApportionedPrice += t.Amount * t.HeinPrice.Value;
                    }
                    BhytServiceRequestData last = attachmentWithFirstStents[attachmentWithFirstStents.Count - 1];
                    last.HeinPrice = Math.Round((max - sumApportionedPrice) / last.Amount, BhytConstant.DECIMAL_PRECISION);

                    //Tranh truong hop do cac tinh toan o tren co su dung lam tron, dan den dv cuoi cung bi am
                    last.HeinPrice = last.HeinPrice < 0 ? 0 : last.HeinPrice;
                }

                this.SetPatientPriceForServiceInPackage(heinCardNumber, attachmentWithFirstStents, totalAttachmentWithFirstStentPrice);

                if (secondStent != null)
                {
                    decimal? ratio = GetSecondStentHeinRatio(patientTypeData, secondStent, treatmentTypeCode);
                    if (ratio != null)
                    {
                        //Gan lai gia theo ti le thanh toan cho stent thu 2
                        if (this.isNoLimitSecondStentForSpecials && this.IsNoLimitMaterialMedicinePrice(heinCardNumber))
                        {
                            //Neu vat tu co gia tran thi gan lai theo original_price, con ko thi ko gan gia tran
                            secondStent.LimitPrice = secondStent.LimitPrice.HasValue ? (decimal?) secondStent.OriginalPrice : null;
                        }
                        else
                        {
                            secondStent.LimitPrice =  this.secondStentPaidRatio * secondStent.OriginalPrice;
                        }
                        
                        secondStent.HeinRatio = ratio.Value;
                        secondStent.HeinPrice = ratio.Value * (secondStent.LimitPrice.HasValue ? secondStent.LimitPrice.Value : secondStent.Price);
                    }
                }

                //Cac stent khac ko duoc thanh toan, set lai tien
                if (otherStents != null)
                {
                    foreach (BhytServiceRequestData data in otherStents)
                    {
                        data.HeinPrice = 0;
                        data.HeinRatio = 0;
                        data.LimitPrice = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Duyet lai de tinh lai so tien "benh nhan tu tra"
        /// </summary>
        /// <param name="heinCardNumber"></param>
        /// <param name="noLimitHighHeinServicePrefixs"></param>
        /// <param name="services"></param>
        private void SetPatientPriceForServiceInPackage(string heinCardNumber, List<BhytServiceRequestData> services, decimal totalHeinPrice)
        {
            //Tong tien BHYT (tinh theo tran chap nhan chi tra) ko tinh stent thu 2
            decimal totalPackageHein = !this.IsNoLimitHighServicePriceTotal(heinCardNumber) && totalHeinPrice > this.maxTotalPackage ? this.maxTotalPackage : totalHeinPrice;
            //Tinh lai so tien BHYT chi tra sau khi phan bo chi phi BHYT chi tra tung vat tu trong goi KTC
            decimal totalHeinPricePaid = services.Sum(o => o.Amount * (o.HeinPrice.HasValue ? o.HeinPrice.Value : o.HeinRatio * o.Price));

            //Chi xu ly khi tong tien > 0 va ko ton tai so luong nao <= 0 (de tranh loi phep chia cho 0)
            if (totalHeinPricePaid > 0 && !services.Exists(t => t.Amount <= 0))
            {
                //Lay ty le trung binh "BN cung chi tra"/ "BH thanh toan"
                decimal tmpRatio = (totalPackageHein - totalHeinPricePaid) / totalHeinPricePaid;
                decimal tmpSumPatientPrice = 0;
                for (int i = 0; i < services.Count - 1; i++)
                {
                    BhytServiceRequestData t = services[i];
                    if (t.HeinPrice.HasValue)
                    {
                        t.PatientPrice = Math.Round(tmpRatio * t.HeinPrice.Value, BhytConstant.DECIMAL_PRECISION);
                    }
                    else if (t.LimitPrice.HasValue)
                    {
                        t.PatientPrice = Math.Round(tmpRatio * t.HeinRatio * t.LimitPrice.Value, BhytConstant.DECIMAL_PRECISION);
                    }
                    else
                    {
                        t.PatientPrice = Math.Round(tmpRatio * t.HeinRatio * t.Price, BhytConstant.DECIMAL_PRECISION);
                    }
                    tmpSumPatientPrice += t.PatientPrice.Value * t.Amount;
                }

                BhytServiceRequestData last = services[services.Count - 1];
                last.PatientPrice = Math.Round((totalPackageHein - totalHeinPricePaid - tmpSumPatientPrice) / last.Amount, BhytConstant.DECIMAL_PRECISION);
            }
        }

        //Kiem tra xem the co bi gioi han tong so tien BHYT chi tra doi voi goi ky thuat cao hay khong
        private bool IsNoLimitHighServicePriceTotal(string heinCardNumber)
        {
            if (heinCardNumber != null && this.noLimitHighHeinServicePrefixs != null && this.noLimitHighHeinServicePrefixs.Count > 0)
            {
                foreach (string prefix in this.noLimitHighHeinServicePrefixs)
                {
                    if (heinCardNumber.StartsWith(prefix))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //Kiem tra xem the co bi gioi han tran thanh toan voi thuoc, vat tu hay khong
        private bool IsNoLimitMaterialMedicinePrice(string heinCardNumber)
        {
            if (heinCardNumber != null && this.noLimitMaterialMedicinePrefixs != null && this.noLimitMaterialMedicinePrefixs.Count > 0)
            {
                foreach (string prefix in this.noLimitMaterialMedicinePrefixs)
                {
                    if (heinCardNumber.StartsWith(prefix))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private decimal GetLimitHighServicePrice(BhytServiceRequestData parent, string treatmentTypeCode, string heinCardNumber, BhytPatientTypeData patientTypeData, decimal total)
        {
            decimal result = 0;
            if (heinCardNumber != null && patientTypeData != null)
            {
                //decimal? defaultRatio = this.GetDefaultHeinRatio(heinCardNumber);
                decimal? defaultRatio = this.GetHeinRatio(treatmentTypeCode, heinCardNumber, HeinRatioTypeCode.NORMAL, patientTypeData, false, parent);

                decimal limit = this.maxTotalPackage < total ? this.maxTotalPackage : total;

                if (defaultRatio != null)
                {
                    if (patientTypeData.JOIN_5_YEAR == HeinJoin5YearCode.TRUE && patientTypeData.PAID_6_MONTH == HeinPaid6MonthCode.TRUE)
                    {
                        result = limit;
                    }
                    else if (patientTypeData.JOIN_5_YEAR == HeinJoin5YearCode.TRUE
                        && BhytConstant.DEFAULT_RATIO_FOR_HIGH_HEIN_SERVICE_5_YEAR != null
                        && BhytConstant.DEFAULT_RATIO_FOR_HIGH_HEIN_SERVICE_5_YEAR.Contains(defaultRatio.Value)
                        && patientTypeData.RIGHT_ROUTE_CODE == HeinRightRouteCode.TRUE)
                        //&& total > this.maxTotalPackage) //chi xu ly trong truong hop vuot tran
                    {
                        decimal x = defaultRatio.Value * limit;
                        decimal y = limit - 6 * this.baseSalary;
                        result = x > y ? x : y;
                    }
                    else
                    {
                        result = limit * defaultRatio.Value;
                    }
                }
            }
            return result;
        }

        private static string GetRightRouteCode(BhytPatientTypeData patientTypeData, string treatmentTypeCode)
        {
            if (string.IsNullOrWhiteSpace(patientTypeData.LIVE_AREA_CODE))
            {
                return patientTypeData.RIGHT_ROUTE_CODE == HeinRightRouteCode.TRUE ? HeinRightRouteCode.TRUE : HeinRightRouteCode.FALSE;
            }
            else if (HeinLiveAreaStore.IsValidCode(patientTypeData.LIVE_AREA_CODE))
            {
                //Neu tuyen huyen/xa hoac dieu tri noi tru thi coi nhu dung tuyen
                if (patientTypeData.LEVEL_CODE == HeinLevelCode.COMMUNE
                    || patientTypeData.LEVEL_CODE == HeinLevelCode.DISTRICT
                    || treatmentTypeCode == HeinTreatmentTypeCode.TREAT
                    || patientTypeData.RIGHT_ROUTE_CODE == HeinRightRouteCode.TRUE)
                {
                    return HeinRightRouteCode.TRUE;
                }
            }
            return HeinRightRouteCode.FALSE;
        }

    }
}
