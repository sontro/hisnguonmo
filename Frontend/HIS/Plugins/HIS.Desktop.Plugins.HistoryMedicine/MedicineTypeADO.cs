using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HistoryMedicine
{
    class MedicineTypeADO
    {
        public long STT_ID { get; set; }
        public long ID { get; set; }
        public string STT_NAME { get; set; }
        public string MEDICINE_TYPE_CODE { get; set; }
        public long? TIME { get; set; }
        public long MEST_ID { get; set; }
        public string MEST_CODE { get; set; }
        public string MEST_TYPE { get; set; }
        public long IMP_MEST_TYPE_ID { get; set; }
        public long EXP_MEST_TYPE_ID { get; set; }
        public decimal AMOUNT { get; set; }
        public decimal? PRICE { get; set; }
        public string MEDI_STOCK_PERIOD_NAME { get; set; }
        public string MEDI_STOCK_NAME { get; set; }
        public string IMP_MEDI_STOCK_NAME { get; set; }
        public string EXP_MEDI_STOCK_NAME { get; set; }
        public string REQ_DEPARTMENT_NAME { get; set; }
        public bool IsExp { get; set; }
        public string DOCUMENT_NUMBER { get; set; }
        public string PACKAGE_NUMBER { get; set; }
        public string KEY_WORD { get; set; }
        public string MEDI_STOCK_NAME__STR { get; set; }
        public long? TDL_INTRUCTION_TIME { get; set; }
        public long? CREATE_TIME { get; set; }
        public MedicineTypeADO() { }
        public MedicineTypeADO(V_HIS_EXP_MEST_MEDICINE_4 data)
        {
            try
            {
                this.ID = ID;
                this.MEDICINE_TYPE_CODE = data.MEDICINE_TYPE_CODE;
                this.TIME = data.EXP_TIME;

                if (data.EXP_DATE != null && data.EXP_DATE > 0)
                    this.MEST_ID = data.EXP_MEST_ID ?? 0;
                this.MEST_CODE = data.EXP_MEST_CODE;
                this.MEST_TYPE = data.EXP_MEST_TYPE_NAME;
                this.EXP_MEST_TYPE_ID = data.EXP_MEST_TYPE_ID;
                this.AMOUNT = data.AMOUNT;
                this.PRICE = data.PRICE;
                this.IsExp = true;
                this.MEDI_STOCK_PERIOD_NAME = data.MEDI_STOCK_PERIOD_NAME;
                this.MEDI_STOCK_NAME = data.MEDI_STOCK_NAME;

                this.KEY_WORD = convertToUnSign3(this.EXP_MEDI_STOCK_NAME) + this.EXP_MEDI_STOCK_NAME
                                + convertToUnSign3(this.IMP_MEDI_STOCK_NAME) + this.IMP_MEDI_STOCK_NAME
                                + convertToUnSign3(this.MEDI_STOCK_NAME) + this.MEDI_STOCK_NAME
                                + convertToUnSign3(this.MEDI_STOCK_PERIOD_NAME) + this.MEDI_STOCK_PERIOD_NAME
                                + convertToUnSign3(this.REQ_DEPARTMENT_NAME) + this.REQ_DEPARTMENT_NAME
                                + convertToUnSign3(this.STT_NAME) + this.STT_NAME
                                + convertToUnSign3(this.MEDICINE_TYPE_CODE) + this.MEDICINE_TYPE_CODE
                                + convertToUnSign3(this.MEST_CODE) + this.MEST_CODE
                                + convertToUnSign3(this.MEST_TYPE) + this.MEST_TYPE;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public MedicineTypeADO(V_HIS_IMP_MEST_MEDICINE_3 data)
        {
            try
            {
                this.MEDICINE_TYPE_CODE = data.MEDICINE_TYPE_CODE;
                this.TIME = data.IMP_TIME;
                if (data.IMP_TIME != null && data.IMP_TIME > 0)
                    this.MEST_ID = data.IMP_MEST_ID;
                this.MEST_CODE = data.IMP_MEST_CODE;
                this.MEST_TYPE = data.IMP_MEST_TYPE_NAME;
                this.IMP_MEST_TYPE_ID = data.IMP_MEST_TYPE_ID;
                this.AMOUNT = data.AMOUNT;
                this.PRICE = data.PRICE;
                this.MEDI_STOCK_PERIOD_NAME = data.MEDI_STOCK_PERIOD_NAME;
                this.MEDI_STOCK_NAME = data.MEDI_STOCK_NAME;
                this.IsExp = false;
                this.KEY_WORD = convertToUnSign3(this.EXP_MEDI_STOCK_NAME) + this.EXP_MEDI_STOCK_NAME
                                + convertToUnSign3(this.IMP_MEDI_STOCK_NAME) + this.IMP_MEDI_STOCK_NAME
                                + convertToUnSign3(this.MEDI_STOCK_NAME) + this.MEDI_STOCK_NAME
                                + convertToUnSign3(this.MEDI_STOCK_PERIOD_NAME) + this.MEDI_STOCK_PERIOD_NAME
                                + convertToUnSign3(this.REQ_DEPARTMENT_NAME) + this.REQ_DEPARTMENT_NAME
                                + convertToUnSign3(this.STT_NAME) + this.STT_NAME
                                + convertToUnSign3(this.MEDICINE_TYPE_CODE) + this.MEDICINE_TYPE_CODE
                                + convertToUnSign3(this.MEST_CODE) + this.MEST_CODE
                                + convertToUnSign3(this.MEST_TYPE) + this.MEST_TYPE;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public string convertToUnSign3(string s)
        {
            if (String.IsNullOrWhiteSpace(s))
                return "";

            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
    }
}
