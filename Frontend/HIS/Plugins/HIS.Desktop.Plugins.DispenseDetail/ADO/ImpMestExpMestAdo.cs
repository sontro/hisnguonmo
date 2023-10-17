using HIS.Desktop.LocalStorage.BackendData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.DispenseDetail.ADO
{
    public class ImpMestExpMestAdo
    {
        public long ImpExpMestId { get; set; }
        public bool IsImpMest { get; set; }
        public string ImpExpMestCode { get; set; }
        public long CREATE_DATE { get; set; }
        public long? CREATE_TIME { get; set; }
        public string CREATOR { get; set; }
        public string DESCRIPTION { get; set; }
        public decimal? DISCOUNT { get; set; }
        public decimal? DISCOUNT_RATIO { get; set; }
        public string MODIFIER { get; set; }
        public long? MODIFY_TIME { get; set; }
        public long ImpExpMestSttId { get; set; }
        public string ImpExpMestSttName { get; set; }
        public long ImpExpTypeId { get; set; }
        public string ImpExpLoginname { get; set; }
        public string ImpExpUsername { get; set; }
        public long? ImpExpTime { get; set; }
        public long MEDI_STOCK_ID { get; set; }
        public string TDL_DISPENSE_CODE { get; set; }
        public string Type { get; set; }

        public ImpMestExpMestAdo(MOS.EFMODEL.DataModels.HIS_IMP_MEST impMest)
        {
            try
            {
                this.ImpExpMestId = impMest.ID;
                this.IsImpMest = true;
                this.ImpExpMestCode = impMest.IMP_MEST_CODE;
                this.CREATE_DATE = impMest.CREATE_DATE;
                this.CREATE_TIME = impMest.CREATE_TIME;
                this.CREATOR = impMest.CREATOR;
                this.DESCRIPTION = impMest.DESCRIPTION;
                this.DISCOUNT = impMest.DISCOUNT;
                this.DISCOUNT_RATIO = impMest.DISCOUNT_RATIO;
                this.MODIFIER = impMest.MODIFIER;
                this.MODIFY_TIME = impMest.MODIFY_TIME;
                this.ImpExpMestSttId = impMest.IMP_MEST_STT_ID;
                this.ImpExpTypeId = impMest.IMP_MEST_TYPE_ID;
                this.ImpExpLoginname = impMest.IMP_LOGINNAME;
                this.ImpExpUsername = impMest.IMP_USERNAME;
                this.ImpExpTime = impMest.IMP_TIME;
                this.MEDI_STOCK_ID = impMest.MEDI_STOCK_ID;
                this.TDL_DISPENSE_CODE = impMest.TDL_DISPENSE_CODE;
                this.Type = "Phiếu nhập";
                var stt = BackendDataWorker.Get<MOS.EFMODEL.DataModels.HIS_IMP_MEST_STT>().FirstOrDefault(o => o.ID == this.ImpExpMestSttId);
                if (stt != null)
                {
                    this.ImpExpMestSttName = stt.IMP_MEST_STT_NAME;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public ImpMestExpMestAdo(MOS.EFMODEL.DataModels.HIS_EXP_MEST expMest)
        {
            try
            {
                this.ImpExpMestId = expMest.ID;
                this.IsImpMest = false;
                this.ImpExpMestCode = expMest.EXP_MEST_CODE;
                //this.APPROVAL_LOGINNAME = expMest.;
                //this.APPROVAL_TIME = expMest.time;
                //this.APPROVAL_USERNAME = expMest.APPROVAL_USERNAME;
                this.CREATE_DATE = expMest.CREATE_DATE;
                this.CREATE_TIME = expMest.CREATE_TIME;
                this.CREATOR = expMest.CREATOR;
                this.DESCRIPTION = expMest.DESCRIPTION;
                this.DISCOUNT = expMest.DISCOUNT;
                //this.DISCOUNT_RATIO = expMest.DISCOUNT_RATIO;
                this.MODIFIER = expMest.MODIFIER;
                this.MODIFY_TIME = expMest.MODIFY_TIME;
                this.ImpExpMestSttId = expMest.EXP_MEST_STT_ID;
                this.ImpExpTypeId = expMest.EXP_MEST_TYPE_ID;
                this.ImpExpLoginname = expMest.LAST_EXP_LOGINNAME;
                this.ImpExpUsername = expMest.LAST_EXP_USERNAME;
                this.ImpExpTime = expMest.LAST_EXP_TIME;
                this.MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                this.TDL_DISPENSE_CODE = expMest.TDL_DISPENSE_CODE;
                this.Type = "Phiếu xuất";
                var stt = BackendDataWorker.Get<MOS.EFMODEL.DataModels.HIS_EXP_MEST_STT>().FirstOrDefault(o => o.ID == this.ImpExpMestSttId);
                if (stt != null)
                {
                    this.ImpExpMestSttName = stt.EXP_MEST_STT_NAME;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
