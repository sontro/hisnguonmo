using AutoMapper;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LibraryBillTwoBook
{
    public class BillTwoBookPriceProcessor
    {
        private long _PatientTypeId__Bhyt;
        private long _PatientTypeId__Fee;
        private long _PatientTypeId__Service;
        private List<long> _PatientTypes;

        public BillTwoBookPriceProcessor(long patyIdBhyt, long patyIdVienPhi, long patyIdDicVu)
        {
            this._PatientTypeId__Bhyt = patyIdBhyt;
            this._PatientTypeId__Fee = patyIdVienPhi;
            this._PatientTypeId__Service = patyIdDicVu;
        }

        public BillTwoBookPriceProcessor(long patyIdBhyt, long patyIdVienPhi, long patyIdDicVu, List<HIS_PATIENT_TYPE> lstPaty)
        {
            this._PatientTypeId__Bhyt = patyIdBhyt;
            this._PatientTypeId__Fee = patyIdVienPhi;
            this._PatientTypeId__Service = patyIdDicVu;
            this._PatientTypes = lstPaty != null ? lstPaty.Where(o => o.IS_NOT_SERVICE_BILL == (short)1).Select(s => s.ID).ToList() : null;
        }

        public void Hcm115Calculator(HIS_SERE_SERV item, ref decimal recieptAmount, ref decimal invoiceAmount)
        {
            if (this._PatientTypeId__Bhyt <= 0 || this._PatientTypeId__Fee <= 0)
                throw new Exception("_PatientTypeId__Bhyt || _PatientTypeId__Fee <= 0");
            if (this._PatientTypes == null) this._PatientTypes = new List<long>();

            if ((item.PATIENT_TYPE_ID == _PatientTypeId__Bhyt || item.PATIENT_TYPE_ID == _PatientTypeId__Fee)
                && !item.PRIMARY_PATIENT_TYPE_ID.HasValue)
            {
                recieptAmount = (item.VIR_TOTAL_PATIENT_PRICE ?? 0);
            }
            else if (this._PatientTypes.Contains(item.PATIENT_TYPE_ID) && !item.PRIMARY_PATIENT_TYPE_ID.HasValue)
            {
                recieptAmount = (item.VIR_TOTAL_PATIENT_PRICE ?? 0);
            }
            else if (item.PATIENT_TYPE_ID != _PatientTypeId__Bhyt && item.PATIENT_TYPE_ID != _PatientTypeId__Fee && !this._PatientTypes.Contains(item.PATIENT_TYPE_ID))
            {
                invoiceAmount = (item.VIR_TOTAL_PATIENT_PRICE ?? 0);
            }
            else
            {
                if (item.PATIENT_TYPE_ID == _PatientTypeId__Fee)
                {
                    decimal vpPrice = item.AMOUNT * (item.LIMIT_PRICE ?? 0) * (1 + item.VAT_RATIO);
                    if (item.VIR_TOTAL_PATIENT_PRICE > vpPrice)
                    {
                        recieptAmount = vpPrice;
                        invoiceAmount = ((item.VIR_TOTAL_PATIENT_PRICE ?? 0) - vpPrice);
                    }
                    else
                    {
                        recieptAmount = (item.VIR_TOTAL_PATIENT_PRICE ?? 0);
                    }
                }
                else if (this._PatientTypes.Contains(item.PATIENT_TYPE_ID))
                {
                    decimal vpPrice = item.AMOUNT * (item.LIMIT_PRICE ?? 0) * (1 + item.VAT_RATIO);
                    if (item.VIR_TOTAL_PATIENT_PRICE > vpPrice)
                    {
                        recieptAmount = vpPrice;
                        invoiceAmount = ((item.VIR_TOTAL_PATIENT_PRICE ?? 0) - vpPrice);
                    }
                    else
                    {
                        recieptAmount = (item.VIR_TOTAL_PATIENT_PRICE ?? 0);
                    }
                }
                else
                {
                    if ((item.VIR_TOTAL_PATIENT_PRICE ?? 0) > (item.VIR_TOTAL_PATIENT_PRICE_BHYT ?? 0))
                    {
                        recieptAmount = (item.VIR_TOTAL_PATIENT_PRICE_BHYT ?? 0);
                        invoiceAmount = ((item.VIR_TOTAL_PATIENT_PRICE ?? 0) - (item.VIR_TOTAL_PATIENT_PRICE_BHYT ?? 0));
                    }
                    else
                    {
                        recieptAmount = (item.VIR_TOTAL_PATIENT_PRICE ?? 0);
                    }
                }
            }
        }

        public void CtoTWCalcualator(HIS_SERE_SERV item, ref decimal recieptAmount, ref decimal invoiceAmount, HIS_SERE_SERV_BILL recieptSSBill = null)
        {
            if (this._PatientTypeId__Bhyt <= 0 || this._PatientTypeId__Fee <= 0 || this._PatientTypeId__Service <= 0)
                throw new Exception("_PatientTypeId__Bhyt || _PatientTypeId__Fee || _PatientTypeId__Service <= 0");

            if (item.PRIMARY_PATIENT_TYPE_ID.HasValue && item.PRIMARY_PATIENT_TYPE_ID.Value == this._PatientTypeId__Service)
            {
                invoiceAmount = (item.VIR_TOTAL_PATIENT_PRICE ?? 0);
            }
            else if (item.PATIENT_TYPE_ID == this._PatientTypeId__Service)
            {
                invoiceAmount = (item.VIR_TOTAL_PATIENT_PRICE ?? 0);
            }
            else
            {
                if (item.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH
                    && item.PATIENT_TYPE_ID == this._PatientTypeId__Bhyt
                    && (item.VIR_TOTAL_PATIENT_PRICE ?? 0) > (item.VIR_TOTAL_PATIENT_PRICE_BHYT ?? 0))
                {
                    invoiceAmount = ((item.VIR_TOTAL_PATIENT_PRICE ?? 0) - (item.VIR_TOTAL_PATIENT_PRICE_BHYT ?? 0));
                    recieptAmount = (item.VIR_TOTAL_PATIENT_PRICE_BHYT ?? 0);
                    if (recieptSSBill != null && recieptAmount > 0) recieptSSBill.PATIENT_BHYT_PRICE = recieptAmount;
                }
                else if (item.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__G
                    && item.PATIENT_TYPE_ID == this._PatientTypeId__Bhyt
                    && (item.VIR_TOTAL_PATIENT_PRICE ?? 0) > (item.VIR_TOTAL_PATIENT_PRICE_BHYT ?? 0))
                {
                    invoiceAmount = (item.VIR_TOTAL_PATIENT_PRICE ?? 0);
                }
                else
                {
                    recieptAmount = (item.VIR_TOTAL_PATIENT_PRICE ?? 0);
                    if (recieptSSBill != null && recieptAmount > 0)
                    {
                        if ((item.VIR_TOTAL_PATIENT_PRICE ?? 0) > (item.VIR_TOTAL_PATIENT_PRICE_BHYT ?? 0))
                        {
                            recieptSSBill.PATIENT_BHYT_PRICE = (item.VIR_TOTAL_PATIENT_PRICE_BHYT ?? 0);
                            recieptSSBill.PATIENT_PAY_PRICE = (item.VIR_TOTAL_PATIENT_PRICE ?? 0) - (item.VIR_TOTAL_PATIENT_PRICE_BHYT ?? 0);
                        }
                        else
                        {
                            recieptSSBill.PATIENT_BHYT_PRICE = recieptAmount;
                        }
                    }
                }
            }
        }

        public void QbhCubaCalcualator(HIS_SERE_SERV item, ref decimal recieptAmount, ref decimal invoiceAmount)
        {
            if (this._PatientTypeId__Bhyt <= 0 || this._PatientTypeId__Fee <= 0)
                throw new Exception("_PatientTypeId__Bhyt || _PatientTypeId__Fee <= 0");

            if (item.PATIENT_TYPE_ID != _PatientTypeId__Bhyt && item.PATIENT_TYPE_ID != _PatientTypeId__Fee)
            {
                invoiceAmount = (item.VIR_TOTAL_PATIENT_PRICE ?? 0);
            }
            else if (item.PATIENT_TYPE_ID == _PatientTypeId__Fee && !item.PRIMARY_PATIENT_TYPE_ID.HasValue)
            {
                recieptAmount = (item.VIR_TOTAL_PATIENT_PRICE ?? 0);
            }
            else if (item.PATIENT_TYPE_ID == _PatientTypeId__Fee && item.PRIMARY_PATIENT_TYPE_ID.HasValue)
            {
                decimal vpPrice = item.AMOUNT * (item.LIMIT_PRICE ?? 0) * (1 + item.VAT_RATIO);
                if (item.VIR_TOTAL_PATIENT_PRICE > vpPrice)
                {
                    recieptAmount = vpPrice;
                    invoiceAmount = ((item.VIR_TOTAL_PATIENT_PRICE ?? 0) - vpPrice);
                }
                else
                {
                    recieptAmount = (item.VIR_TOTAL_PATIENT_PRICE ?? 0);
                }
            }
            else
            {
                if (item.PRIMARY_PATIENT_TYPE_ID.HasValue)
                {
                    if ((item.VIR_TOTAL_PATIENT_PRICE ?? 0) > (item.VIR_TOTAL_PATIENT_PRICE_BHYT ?? 0))
                    {
                        recieptAmount = (item.VIR_TOTAL_PATIENT_PRICE_BHYT ?? 0);
                        invoiceAmount = (item.VIR_TOTAL_PATIENT_PRICE ?? 0) - (item.VIR_TOTAL_PATIENT_PRICE_BHYT ?? 0);
                    }
                    else
                    {
                        recieptAmount = (item.VIR_TOTAL_PATIENT_PRICE ?? 0);
                    }
                }
                else if (item.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__G
                    || item.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH)
                {
                    if ((item.VIR_TOTAL_PATIENT_PRICE ?? 0) > (item.VIR_TOTAL_PATIENT_PRICE_BHYT ?? 0))
                    {
                        recieptAmount = (item.VIR_TOTAL_PATIENT_PRICE_BHYT ?? 0);
                        invoiceAmount = (item.VIR_TOTAL_PATIENT_PRICE ?? 0) - (item.VIR_TOTAL_PATIENT_PRICE_BHYT ?? 0);
                    }
                    else
                    {
                        recieptAmount = (item.VIR_TOTAL_PATIENT_PRICE ?? 0);
                    }
                }
                else
                {
                    recieptAmount = (item.VIR_TOTAL_PATIENT_PRICE ?? 0);
                }
            }
        }

        public void Hcm115Calculator(V_HIS_SERE_SERV_5 item, ref decimal recieptAmount, ref decimal invoiceAmount)
        {
            Mapper.CreateMap<V_HIS_SERE_SERV_5, HIS_SERE_SERV>();
            HIS_SERE_SERV ss = Mapper.Map<HIS_SERE_SERV>(item);
            this.Hcm115Calculator(ss, ref recieptAmount, ref invoiceAmount);
        }

        public void CtoTWCalcualator(V_HIS_SERE_SERV_5 item, ref decimal recieptAmount, ref decimal invoiceAmount)
        {
            Mapper.CreateMap<V_HIS_SERE_SERV_5, HIS_SERE_SERV>();
            HIS_SERE_SERV ss = Mapper.Map<HIS_SERE_SERV>(item);
            this.CtoTWCalcualator(ss, ref recieptAmount, ref invoiceAmount);
        }

        public void QbhCubaCalcualator(V_HIS_SERE_SERV_5 item, ref decimal recieptAmount, ref decimal invoiceAmount)
        {
            Mapper.CreateMap<V_HIS_SERE_SERV_5, HIS_SERE_SERV>();
            HIS_SERE_SERV ss = Mapper.Map<HIS_SERE_SERV>(item);
            this.QbhCubaCalcualator(ss, ref recieptAmount, ref invoiceAmount);
        }

    }
}
