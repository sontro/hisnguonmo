using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServ;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisInvoiceDetail;
using MOS.MANAGER.Config;

namespace MOS.MANAGER.HisInvoice
{
    partial class HisInvoiceCreate : BusinessBase
    {
        private List<HIS_INVOICE> recentHisInvoices = new List<HIS_INVOICE>();

        private HIS_INVOICE hisInvoice;
        private HisSereServUpdate hisSereServUpdate;
        private HisInvoiceDetailCreate hisInvoiceDetailCreate;

        internal HisInvoiceCreate()
            : base()
        {
            this.Init();
        }

        internal HisInvoiceCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisSereServUpdate = new HisSereServUpdate(param);
            this.hisInvoiceDetailCreate = new HisInvoiceDetailCreate(param);
        }

        internal bool Create(HisInvoiceSDO data, ref V_HIS_INVOICE resultData)
        {
            bool result = false;
            try
            {
                if (this.ProcessInvoice(data))
                {
                    this.ProcessSereServ(data);
                    //load lai du lieu tu DB de lay cac thong tin nhu "ngay tao", "nguoi tao", ...
                    resultData = new HisInvoiceGet().GetViewById(this.hisInvoice.ID);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                this.RollbackData();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private bool ProcessInvoice(HisInvoiceSDO data)
        {
            Mapper.CreateMap<HisInvoiceSDO, HIS_INVOICE>();
            HIS_INVOICE invoice = Mapper.Map<HIS_INVOICE>(data);
            if (invoice.INVOICE_TIME <= 0)
            {
                invoice.INVOICE_TIME = Inventec.Common.DateTime.Get.Now().Value;
            }

            if (!this.Create(invoice))
            {
                return false;
            }
            this.hisInvoice = invoice;
            return true;
        }

        private void ProcessSereServ(HisInvoiceSDO data)
        {
            if (!IsNotNullOrEmpty(data.SereServIds) && (data.HIS_INVOICE_DETAIL == null || data.HIS_INVOICE_DETAIL.Count == 0))
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                throw new Exception("SereServIds va data.HIS_INVOICE_DETAIL null" + LogUtil.TraceData("data", data));
            }
            if (IsNotNullOrEmpty(data.SereServIds))
            {
                List<HIS_SERE_SERV> hisSereServs = new HisSereServGet().GetByIds(data.SereServIds);
                decimal totalPatientPrice = hisSereServs.Where(o => o.VIR_TOTAL_PATIENT_PRICE.HasValue).Sum(o => o.VIR_TOTAL_PATIENT_PRICE.Value);

                if (totalPatientPrice <= 0)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisInvoice_TongSoTienBenhNhanThanhToanPhaiLonHon0);
                    throw new Exception();
                }

                Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                List<HIS_SERE_SERV> toUpdates = Mapper.Map<List<HIS_SERE_SERV>>(hisSereServs);

                if (!this.hisSereServUpdate.UpdateInvoiceId(toUpdates, this.hisInvoice.ID))
                {
                    throw new Exception("Rollback du lieu, ket thuc nghiep vu.");
                }

                List<HIS_INVOICE_DETAIL> hisInvoiceDetails = new List<HIS_INVOICE_DETAIL>();

                //Neu he thong thuc hien cau hinh gom nhom thong tin thanh 1 dong trong chi tiet hoa don 
                if (HisInvoiceDetailCFG.GROUP_OPTION == HisInvoiceDetailCFG.GroupOption.ALL)
                {
                    HIS_INVOICE_DETAIL detail = new HIS_INVOICE_DETAIL();
                    detail.AMOUNT = 1;
                    detail.GOODS_NAME = HisInvoiceDetailCFG.GOODS_GROUP_NAME;
                    detail.GOODS_UNIT = HisInvoiceDetailCFG.GOODS_GROUP_UNIT;
                    detail.INVOICE_ID = this.hisInvoice.ID;
                    detail.PRICE = totalPatientPrice;
                    hisInvoiceDetails.Add(detail);
                }
                //Neu he thong cau hinh gom nhom theo loai dich vu va doi tuong thanh toan (BHYT va ko phai BHYT)
                else if (HisInvoiceDetailCFG.GROUP_OPTION == HisInvoiceDetailCFG.GroupOption.BY_TYPE)
                {
                    //Tien benh nhan dong chi tra BHYT
                    List<HIS_SERE_SERV> patientBhyts = hisSereServs.Where(o => o.VIR_TOTAL_PATIENT_PRICE_BHYT > 0).ToList();
                    //Tien benh nhan tu tra (tra tien chenh lech dich vu hoac tra do đối tượng thanh toán ko phải là BHYT)
                    List<HIS_SERE_SERV> patients = hisSereServs
                        .Where(o => (o.VIR_TOTAL_PATIENT_PRICE ?? 0) - (o.VIR_TOTAL_PATIENT_PRICE_BHYT ?? 0) > 0).ToList();

                    //Tao du lieu tuong ung voi tien BN đóng đồng chi trả BHYT
                    // Nội dung có dạng: "Đồng chi trả (20%): 100000đ (Siêu âm: 20000đ, Thuốc: 80000đ)"
                    // Trong trường hợp BN có nhiều mức hưởng thì sinh ra nhiều dòng tương ứng với đồng chi trả BHYT
                    if (IsNotNullOrEmpty(patientBhyts))
                    {
                        //Gom nhom theo mức hưởng
                        var groupByRatios = patientBhyts.GroupBy(o => o.VIR_TOTAL_PATIENT_PRICE_BHYT.Value / (o.VIR_TOTAL_PATIENT_PRICE_BHYT.Value + o.VIR_TOTAL_HEIN_PRICE.Value)).ToList();

                        foreach (var groupByRatio in groupByRatios)
                        {
                            List<HIS_SERE_SERV> ss = groupByRatio.ToList();
                            //Xu ly lay ra noi dung thong tin chi tiet
                            string detailContent = "";
                            var groups = ss.GroupBy(o => o.TDL_SERVICE_TYPE_ID).ToList();
                            foreach (var g in groups)
                            {
                                decimal price = g.ToList().Sum(o => o.VIR_TOTAL_PATIENT_PRICE_BHYT.Value);
                                HIS_SERVICE_TYPE type = HisServiceTypeCFG.DATA.Where(o => o.ID == g.Key).FirstOrDefault();
                                detailContent = string.Format("{0}{1}:{2}{3}, ", detailContent, type.SERVICE_TYPE_NAME, price, HisInvoiceDetailCFG.GOODS_GROUP_CURRENCY);
                            }

                            detailContent = detailContent.Length > 2 ? detailContent.Substring(0, detailContent.Length - 2) : detailContent;
                            decimal totalPatientPriceBhyt = ss.Sum(o => o.VIR_TOTAL_PATIENT_PRICE_BHYT.Value);
                            decimal ratio = 100 * groupByRatio.Key;
                            string ratioStr = string.Format("{0:#,##0.####}", ratio);
                            string goodsName = string.Format("{0} ({1}%): {2}{3} ({4})",
                                HisInvoiceDetailCFG.GOODS_GROUP_NAME__BHYT,
                                ratioStr,
                                totalPatientPriceBhyt,
                                HisInvoiceDetailCFG.GOODS_GROUP_CURRENCY,
                                detailContent
                                );


                            HIS_INVOICE_DETAIL detail = new HIS_INVOICE_DETAIL();
                            detail.AMOUNT = 1;
                            detail.GOODS_NAME = goodsName;
                            detail.GOODS_UNIT = HisInvoiceDetailCFG.GOODS_GROUP_UNIT;
                            detail.INVOICE_ID = this.hisInvoice.ID;
                            detail.PRICE = totalPatientPriceBhyt;
                            hisInvoiceDetails.Add(detail);
                        }
                    }

                    //Tao du lieu tuong ung voi tien BN tự trả (tiền chênh lệch, hoặc tiền đối tượng thanh toán ko phải BHYT)
                    // Du lieu se tra ve co dang:
                    // "Chi trả (100%): 100000đ (Siêu âm: 20000đ, Thuốc: 80000đ)"
                    if (IsNotNullOrEmpty(patients))
                    {
                        //Xu ly lay ra noi dung thong tin chi tiet
                        string detailContent = "";
                        var groups = patients.GroupBy(o => o.TDL_SERVICE_TYPE_ID).ToList();
                        foreach (var g in groups)
                        {
                            decimal price = g.ToList().Sum(o => (o.VIR_TOTAL_PATIENT_PRICE ?? 0) - (o.VIR_TOTAL_PATIENT_PRICE_BHYT ?? 0));
                            HIS_SERVICE_TYPE type = HisServiceTypeCFG.DATA.Where(o => o.ID == g.Key).FirstOrDefault();
                            detailContent = string.Format("{0}{1}:{2}{3}, ", detailContent, type.SERVICE_TYPE_NAME, price, HisInvoiceDetailCFG.GOODS_GROUP_CURRENCY);
                        }

                        //Bo ki tu dau phay va khoang trang o cuoi
                        detailContent = detailContent.Length > 2 ? detailContent.Substring(0, detailContent.Length - 2) : detailContent;
                        decimal totalPrice = patients.Sum(o => (o.VIR_TOTAL_PATIENT_PRICE ?? 0) - (o.VIR_TOTAL_PATIENT_PRICE_BHYT ?? 0));
                        string goodsName = string.Format("{0} (100%): {1}{2} ({3})",
                            HisInvoiceDetailCFG.GOODS_GROUP_NAME__NON_BHYT,
                            totalPrice,
                            HisInvoiceDetailCFG.GOODS_GROUP_CURRENCY,
                            detailContent
                            );

                        HIS_INVOICE_DETAIL detail = new HIS_INVOICE_DETAIL();
                        detail.AMOUNT = 1;
                        detail.GOODS_NAME = goodsName;
                        detail.GOODS_UNIT = HisInvoiceDetailCFG.GOODS_GROUP_UNIT;
                        detail.INVOICE_ID = this.hisInvoice.ID;
                        detail.PRICE = totalPrice;
                        hisInvoiceDetails.Add(detail);
                    }
                }
                else
                {
                    var groups = hisSereServs.GroupBy(o => new { o.SERVICE_ID, o.TDL_SERVICE_NAME, o.VIR_PATIENT_PRICE, o.TDL_SERVICE_UNIT_ID, o.DISCOUNT, o.HEIN_RATIO, o.HEIN_PRICE, o.PRICE });

                    foreach (var t in groups)
                    {
                        HIS_INVOICE_DETAIL detail = new HIS_INVOICE_DETAIL();
                        detail.INVOICE_ID = this.hisInvoice.ID;
                        detail.PRICE = t.Key.VIR_PATIENT_PRICE.HasValue ? t.Key.VIR_PATIENT_PRICE.Value : 0;
                        detail.DISCOUNT = t.Key.DISCOUNT;
                        detail.GOODS_NAME = t.Key.TDL_SERVICE_NAME;
                        HIS_SERVICE_UNIT serviceUnit = HisServiceUnitCFG.DATA != null ? HisServiceUnitCFG.DATA.Where(o => o.ID == t.Key.TDL_SERVICE_UNIT_ID).FirstOrDefault() : null;
                        detail.GOODS_UNIT = serviceUnit != null ? serviceUnit.SERVICE_UNIT_NAME : "";
                        detail.AMOUNT = t.Sum(o => o.AMOUNT);
                        if (t.Key.HEIN_RATIO.HasValue || t.Key.HEIN_PRICE.HasValue)
                        {
                            decimal ratio = ratio = t.Key.HEIN_RATIO.HasValue ?
                                t.Key.HEIN_RATIO.Value * 100 : Math.Round((t.Key.HEIN_PRICE.Value / t.Key.PRICE), 4) * 100;
                            if (ratio != 0)
                            {
                                detail.DESCRIPTION = String.Format("{0:#,##0.####}", ratio);
                            }
                        }
                        hisInvoiceDetails.Add(detail);
                    }
                }

                if (!this.hisInvoiceDetailCreate.CreateList(hisInvoiceDetails))
                {
                    throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                }
            }
        }

        private bool Create(HIS_INVOICE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisInvoiceCheck checker = new HisInvoiceCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsValidPermission(data.INVOICE_BOOK_ID);
                valid = valid && checker.IsValidCount(data.INVOICE_BOOK_ID);
                if (valid)
                {
                    if (!DAOWorker.HisInvoiceDAO.Create(data))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisInvoice_TaoThongTinHoaDonThatBai);
                        throw new Exception("Them moi thong tin HisInvoice that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisInvoices.Add(data);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal void RollbackData()
        {
            this.hisSereServUpdate.RollbackData();
            this.hisInvoiceDetailCreate.RollbackData();

            if (IsNotNullOrEmpty(this.recentHisInvoices))
            {
                if (!DAOWorker.HisInvoiceDAO.TruncateList(this.recentHisInvoices))
                {
                    LogSystem.Warn("Rollback du lieu HisInvoice that bai, can kiem tra lai." + LogUtil.TraceData("HisInvoice", this.recentHisInvoices));
                }
            }
        }
    }
}
