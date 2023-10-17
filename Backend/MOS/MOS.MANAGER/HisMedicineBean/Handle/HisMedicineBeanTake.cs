using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Token.ResourceSystem;
using MOS.EFMODEL.DataModels;
using MOS.LibraryMessage;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMedicineBean.Handle
{
    class MedicineBeanResultHolder
    {
        public string Message { get; set; }
        public List<HIS_MEDICINE_BEAN> MedicineBeans { get; set; }
    }

    /// <summary>
    /// Nghiep vu tach bean theo tung session (tinh theo tung lan tao phieu xuat cua nguoi dung) do client yeu cau
    /// </summary>
    class HisMedicineBeanTake : BusinessBase
    {
        private const char OBJECT_SEPARATOR = ';';
        private const char FIELD_SEPARATOR = ':';

        internal HisMedicineBeanTake()
            : base()
        {
        }

        internal HisMedicineBeanTake(CommonParam paramCreate)
            : base(paramCreate)
        {
        }

        /// <summary>
        /// Lay bean theo yeu cau
        /// Luu y: Take bean trong truong hop cap nhat phieu xuat thi ham take bean sẽ lấy các bean cũ 
        /// (các bean đã được gắn vào phiếu xuất) để xử lý. Để xử lý được thì client sẽ truyền
        /// lên exp_mest_medicine_id tuong ung
        /// </summary>
        /// <param name="mediStockId"></param>
        /// <param name="medicineTypeId"></param>
        /// <param name="amount"></param>
        /// <param name="patientTypeId"></param>
        /// <param name="sessionKey"></param>
        /// <returns></returns>
        internal List<HIS_MEDICINE_BEAN> Take(TakeBeanSDO sdo)
        {
            List<HIS_MEDICINE_BEAN> result = null;
            try
            {
                if (this.Validate(sdo))
                {
                    string sessionKey = SessionUtil.SessionKey(sdo.ClientSessionKey);
                    string languageCode = string.IsNullOrWhiteSpace(param.LanguageCode) ? Message.LanguageCode.VI : param.LanguageCode;
                    string beanIds = IsNotNullOrEmpty(sdo.BeanIds) ? string.Join(",", sdo.BeanIds) : "0";
                    string expMestMedicineIds = IsNotNullOrEmpty(sdo.ExpMestDetailIds) ? string.Join(",", sdo.ExpMestDetailIds) : null;

                    if (sdo.ExpiredDate.HasValue)
                    {
                        try
                        {
                            //Lay dau ngay de check HSD (vi vat tu khi nhap kho, HSD ko luu gio, phut, giay
                            sdo.ExpiredDate = Inventec.Common.DateTime.Get.StartDay(sdo.ExpiredDate.Value);
                        }
                        catch (Exception ex)
                        {
                            LogSystem.Error(ex);
                        }
                    }

                    string storedSql = "PKG_TAKE_MEDICINE_BEAN.PRO_EXECUTE";

                    OracleParameter languageCodePar = new OracleParameter("P_LANGUAGE_CODE", OracleDbType.Varchar2, 10, languageCode, ParameterDirection.Input);
                    OracleParameter messagePar = new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, ParameterDirection.Output);
                    messagePar.Size = 3000;
                    OracleParameter medicineBeanPar = new OracleParameter("P_MEDICINE_BEAN", OracleDbType.Varchar2, ParameterDirection.Output);
                    medicineBeanPar.Size = 3000;
                    OracleParameter mediStockIdPar = new OracleParameter("P_MEDI_STOCK_ID", OracleDbType.Int32, sdo.MediStockId, ParameterDirection.Input);
                    OracleParameter medicineTypeIdPar = new OracleParameter("P_MEDICINE_TYPE_ID", OracleDbType.Int32, sdo.TypeId, ParameterDirection.Input);
                    OracleParameter amountPar = new OracleParameter("P_AMOUNT", OracleDbType.Decimal, sdo.Amount, ParameterDirection.Input);
                    OracleParameter sessionKeyPar = new OracleParameter("P_SESSION_KEY", OracleDbType.Varchar2, 300, sessionKey, ParameterDirection.Input);
                    OracleParameter orderOptPar = new OracleParameter("P_ORDER_OPT", OracleDbType.Int32, HisMediStockCFG.EXPORT_OPTION, ParameterDirection.Input);
                    OracleParameter patientTypeIdPar = new OracleParameter("P_PATIENT_TYPE_ID", OracleDbType.Long, sdo.PatientTypeId, ParameterDirection.Input);
                    OracleParameter beanIdsPar = new OracleParameter("P_BEAN_IDS", OracleDbType.Varchar2, 3000, beanIds, ParameterDirection.InputOutput);
                    OracleParameter expMestMedicineIdsPar = new OracleParameter("P_EXP_MEST_MEDICINE_IDS", OracleDbType.Varchar2, 3000, expMestMedicineIds, ParameterDirection.InputOutput);
                    OracleParameter expiredDatePar = new OracleParameter("P_EXPIRED_DATE", OracleDbType.Int32, sdo.ExpiredDate, ParameterDirection.InputOutput);

                    object resultHolder = null;

                    if (DAOWorker.SqlDAO.ExecuteStored(OutputHandler, ref resultHolder, storedSql, languageCodePar, messagePar, medicineBeanPar, mediStockIdPar, medicineTypeIdPar, amountPar, sessionKeyPar, orderOptPar, patientTypeIdPar, beanIdsPar, expMestMedicineIdsPar, expiredDatePar))
                    {
                        if (resultHolder != null)
                        {
                            MedicineBeanResultHolder t = (MedicineBeanResultHolder)resultHolder;
                            if (t.Message != null)
                            {
                                param.Messages.Add(t.Message);
                            }
                            result = t.MedicineBeans;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        /// <summary>
        /// Lay nhieu bean
        /// </summary>
        /// <param name="sdos"></param>
        /// <returns></returns>
        internal List<TakeMedicineBeanListResultSDO> Take(List<TakeBeanSDO> sdos)
        {
            List<TakeMedicineBeanListResultSDO> result = null;
            try
            {
                if (IsNotNullOrEmpty(sdos))
                {
                    result = new List<TakeMedicineBeanListResultSDO>();
                    foreach (TakeBeanSDO sdo in sdos)
                    {
                        List<HIS_MEDICINE_BEAN> beans = this.Take(sdo);
                        if (beans != null)
                        {
                            TakeMedicineBeanListResultSDO tmp = new TakeMedicineBeanListResultSDO();
                            tmp.Request = sdo;
                            tmp.Result = beans;
                            result.Add(tmp);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        //Xu ly ket qua tra ve khi goi procedure
        private void OutputHandler(ref object resultHolder, OracleParameter[] parameters)
        {
            try
            {
                MedicineBeanResultHolder rs = new MedicineBeanResultHolder();

                if (parameters[1] != null && parameters[1].Value != null && parameters[1].Value != DBNull.Value)
                {
                    rs.Message = !Constant.DB_NULL_STR.Equals(parameters[1].Value.ToString()) ? parameters[1].Value.ToString() : null;
                }

                if (parameters[2] != null && parameters[2].Value != null && parameters[2].Value != DBNull.Value)
                {
                    List<HIS_MEDICINE_BEAN> beans = new List<HIS_MEDICINE_BEAN>();
                    string tmp = parameters[2].Value.ToString();
                    string[] beanStrArr = !string.IsNullOrWhiteSpace(tmp) && !Constant.DB_NULL_STR.Equals(tmp) ? tmp.Split(OBJECT_SEPARATOR) : null;
                    if (beanStrArr != null && beanStrArr.Length > 0)
                    {
                        foreach (string s in beanStrArr)
                        {
                            string[] fieldStrArr = !string.IsNullOrWhiteSpace(s) ? s.Split(FIELD_SEPARATOR) : null;
                            if (fieldStrArr != null && fieldStrArr.Length > 0)
                            {
                                HIS_MEDICINE_BEAN bean = new HIS_MEDICINE_BEAN();
                                bean.ID = long.Parse(fieldStrArr[0]);
                                bean.MEDICINE_ID = long.Parse(fieldStrArr[1]);
                                bean.TDL_MEDICINE_IMP_PRICE = decimal.Parse(fieldStrArr[2]);
                                bean.TDL_MEDICINE_IMP_VAT_RATIO = decimal.Parse(fieldStrArr[3]);
                                bean.AMOUNT = decimal.Parse(fieldStrArr[4]);
                                beans.Add(bean);
                            }
                        }
                        rs.MedicineBeans = beans;
                    }
                }

                resultHolder = rs;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private bool Validate(TakeBeanSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsNotNullOrEmpty(data.ClientSessionKey)) throw new ArgumentNullException("data.ClientSessionKey null");
                if (data.Amount <= 0) throw new ArgumentNullException("thieu data.Amount");
                if (data.MediStockId <= 0) throw new ArgumentNullException("thieu data.MediStockId");
                if (data.TypeId <= 0) throw new ArgumentNullException("thieu data.TypeId");
                //Trong truong hop ko truyen len beanIds (take bean de thêm 1 dòng mới vào phiếu xuất) 
                //thi luon coi nhu la take bean moi (set exp_mest_id = null ==> de ko lay vao cac bean cu cua phieu xuat)
                if (IsNotNullOrEmpty(data.ExpMestDetailIds) && !IsNotNullOrEmpty(data.BeanIds)) throw new ArgumentNullException("co ExpMestDetailIds nhung ko co BeanIds");
                V_HIS_MEDI_STOCK mediStock = HisMediStockCFG.DATA.Where(o => o.ID == data.MediStockId).FirstOrDefault();
                if (mediStock == null || mediStock.IS_ACTIVE != MOS.UTILITY.Constant.IS_TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMediStock_KhoDangTamKhoa);
                    return false;
                }
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Warn(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
