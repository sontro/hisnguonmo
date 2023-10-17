using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Token.ResourceSystem;
using MOS.EFMODEL.DataModels;
using MOS.LibraryMessage;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisMaterialBean.Handle;
using MOS.MANAGER.HisMaterialPaty;
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

namespace MOS.MANAGER.HisMaterialBean.Handle
{
    /// <summary>
    /// Nghiep vu lay bean theo serial_number va session_key
    /// </summary>
    class HisMaterialBeanTakeBySerialAndPatientType : BusinessBase
    {
        internal HisMaterialBeanTakeBySerialAndPatientType()
            : base()
        {
        }

        internal HisMaterialBeanTakeBySerialAndPatientType(CommonParam param)
            : base(param)
        {
        }

        internal bool Run(TakeBeanBySerialSDO sdo, ref HIS_MATERIAL_BEAN resultData)
        {
            bool result = false;
            try
            {
                if (this.IsValid(sdo))
                {
                    HIS_MATERIAL_BEAN bean = this.GetBean(sdo);;
                    
                    if (bean == null)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMaterialBean_VatTuKhongCoSan, sdo.SerialNumber);
                        return false;
                    }
                    
                    if (bean.TDL_IS_SALE_EQUAL_IMP_PRICE != Constant.IS_TRUE)
                    {
                        HIS_MATERIAL_PATY paty = this.GetPaty(bean.MATERIAL_ID, sdo.PatientTypeId);
                        if (paty == null)
                        {
                            HIS_PATIENT_TYPE patientType = HisPatientTypeCFG.DATA.Where(o => o.ID == sdo.PatientTypeId).FirstOrDefault();
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMaterialBean_KhongCoChinhSachGia, sdo.SerialNumber, patientType.PATIENT_TYPE_NAME);
                            return false;
                        }
                        //gan lai theo gia ban de tra ve cho client
                        bean.TDL_MATERIAL_IMP_PRICE = paty.EXP_PRICE;
                        bean.TDL_MATERIAL_IMP_VAT_RATIO = paty.EXP_VAT_RATIO;
                    }

                    string sessionKey = SessionUtil.SessionKey(sdo.ClientSessionKey);
                    if (bean != null && this.LockBean(bean.ID, sessionKey))
                    {
                        resultData = bean;
                        return true;
                    }
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

        private bool LockBean(long id, string sessionKey)
        {
            //cap nhat session_key, is_active
            //Luu y: can set ca is_use (de trigger trong DB check duoc trong truong hop 2 tien trinh cung thuc hien lockbean cung luc)
            string sql = "UPDATE HIS_MATERIAL_BEAN SET SESSION_KEY = :param1, IS_USE = 1, IS_ACTIVE = 0 WHERE ID = :param2";
            return DAOWorker.SqlDAO.Execute(sql, sessionKey, id);
        }

        private HIS_MATERIAL_BEAN GetBean(TakeBeanBySerialSDO sdo)
        {
            HisMaterialBeanFilterQuery filter = new HisMaterialBeanFilterQuery();
            filter.SERIAL_NUMBER = sdo.SerialNumber;
            filter.MATERIAL_IS_ACTIVE = Constant.IS_TRUE;
            filter.IS_ACTIVE = Constant.IS_TRUE;
            filter.MEDI_STOCK_ID = sdo.MediStockId;
            var rs = new HisMaterialBeanGet().Get(filter);
            return IsNotNullOrEmpty(rs) ? rs.OrderBy(o => o.CREATE_TIME).FirstOrDefault() : null;
        }

        private HIS_MATERIAL_PATY GetPaty(long materialId, long patientTypeId)
        {
            HisMaterialPatyFilterQuery patyFilter = new HisMaterialPatyFilterQuery();
            patyFilter.MATERIAL_ID = materialId;
            patyFilter.PATIENT_TYPE_ID = patientTypeId;
            patyFilter.IS_ACTIVE = Constant.IS_TRUE;
            var rs = new HisMaterialPatyGet().Get(patyFilter);
            return IsNotNullOrEmpty(rs) ? rs.OrderBy(o => o.CREATE_TIME).FirstOrDefault() : null;
        }

        private bool IsValid(TakeBeanBySerialSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsNotNullOrEmpty(data.ClientSessionKey)) throw new ArgumentNullException("data.ClientSessionKey null");
                if (data.MediStockId <= 0) throw new ArgumentNullException("thieu data.MediStockId");
                if (string.IsNullOrWhiteSpace(data.SerialNumber)) throw new ArgumentNullException("thieu data.SerialNumber");

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