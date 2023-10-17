using AutoMapper;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisServiceReqMety;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment
{
    partial class HisTreatmentGet : GetBase
    {
        private const int MaxReqCount = 100;

        internal List<ThongTinBenhNhanQlpkSDO> GetThongTinBenhNhanQlpk(string id, string tukhoa)
        {
            List<ThongTinBenhNhanQlpkSDO> result = new List<ThongTinBenhNhanQlpkSDO>();
            try
            {
                if (!IsNotNull(id) && !IsNotNull(tukhoa))
                {
                    return result;
                }

                string query = "SELECT PATIENT_CODE AS \"mabn\", VIR_PATIENT_NAME AS \"hoten\"," +
                    " SUBSTR(TO_CHAR(DOB),0,4) AS \"namsinh\", (CASE WHEN GENDER_ID = 1 THEN 1 ELSE 0 END) AS \"phai\", " +
                    "NVL(CCCD_NUMBER,CMND_NUMBER) AS \"cmnd\", ETHNIC_NAME AS \"dantoc\", " +
                    "PROVINCE_NAME AS \"tentt\", DISTRICT_NAME AS \"tenquan\", COMMUNE_NAME AS \"tenpxa\", ADDRESS AS \"thon\", " +
                    "NVL(MOBILE,PHONE) AS \"didong\" FROM HIS_PATIENT ";
                if (IsNotNull(id))
                {
                    query += string.Format(" WHERE PATIENT_CODE = '{0}'", id);
                }
                else
                {
                    query += string.Format(" WHERE LOWER(VIR_PATIENT_NAME) LIKE '%{0}%' OR LOWER(MOBILE) LIKE '%{0}%' " +
                        "OR LOWER(PHONE) LIKE '%{0}%' OR LOWER(PROVINCE_NAME) LIKE '%{0}%' OR LOWER(DISTRICT_NAME) LIKE '%{0}%' " +
                        "OR LOWER(COMMUNE_NAME) LIKE '%{0}%' OR LOWER(ADDRESS) LIKE '%{0}%'", tukhoa.ToLower());
                }

                result = DAOWorker.SqlDAO.GetSql<ThongTinBenhNhanQlpkSDO>(query);
                if (result == null)
                {
                    Inventec.Common.Logging.LogSystem.Error(string.Format("khong tim thay benh nhan theo dieu kien loc id:{0}, tukhoa:{1}", id, tukhoa));
                    Inventec.Common.Logging.LogSystem.Error("_____query: " + query);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
                param.HasException = true;
            }
            return result;
        }

        internal List<DanhSachDangKyKhamQlpkSDO> GetDanhSachDangKyKhamQlpk(string fromdate, string todate, string trangthai, string tukhoa)
        {
            List<DanhSachDangKyKhamQlpkSDO> result = new List<DanhSachDangKyKhamQlpkSDO>();
            try
            {
                if (!IsNotNull(fromdate) && !IsNotNull(todate))
                {
                    return result;
                }

                string query = "SELECT P.PATIENT_CODE AS \"mabn\", P.VIR_PATIENT_NAME AS \"hoten\", SUBSTR(TO_CHAR(P.DOB),0,4) AS \"namsinh\", " +
                    "(CASE WHEN P.GENDER_ID = 1 THEN '1' ELSE '0' END) AS \"phai\", NVL(P.CCCD_NUMBER,P.CMND_NUMBER) AS \"cmnd\", " +
                    "P.ETHNIC_NAME AS \"dantoc\", P.PROVINCE_NAME AS \"tentt\", P.DISTRICT_NAME AS \"tenquan\", P.COMMUNE_NAME AS \"tenpxa\", P.ADDRESS AS \"thon\", " +
                    "NVL(P.MOBILE,P.PHONE) AS \"didong\",TO_DATE(T.IN_TIME, 'YYYYMMDDHH24MISS') AS \"ngayud\", T.ICD_CODE AS \"maicd\", NULL AS \"vviet\" " +
                    "FROM HIS_TREATMENT T JOIN HIS_PATIENT P ON T.PATIENT_ID = P.ID WHERE " +
                    "EXISTS (SELECT 1 FROM HIS_SERVICE_REQ WHERE T.ID = TREATMENT_ID AND IS_DELETE = 0 AND SERVICE_REQ_TYPE_ID = 1 AND (IS_NO_EXECUTE IS NULL OR IS_NO_EXECUTE <> 1))";

                if (IsNotNull(fromdate))
                {
                    query += string.Format(" AND T.IN_TIME >= {0}", ConvertTimeStringToTimeNumber(fromdate, "000000"));
                }

                if (IsNotNull(todate))
                {
                    query += string.Format(" AND T.IN_TIME <= {0}", ConvertTimeStringToTimeNumber(todate, "235959"));
                }

                if (IsNotNull(trangthai))
                {
                    switch (trangthai)
                    {
                        case "1"://đã khám
                            query += " AND EXISTS (SELECT 1 FROM HIS_SERVICE_REQ WHERE T.ID = TREATMENT_ID AND IS_DELETE = 0 AND SERVICE_REQ_TYPE_ID = 1 AND (IS_NO_EXECUTE IS NULL OR IS_NO_EXECUTE <> 1) AND START_TIME IS NOT NULL)";
                            break;
                        case "2"://chưa khám
                            query += " AND NOT EXISTS (SELECT 1 FROM HIS_SERVICE_REQ WHERE T.ID = TREATMENT_ID AND IS_DELETE = 0 AND SERVICE_REQ_TYPE_ID = 1 AND (IS_NO_EXECUTE IS NULL OR IS_NO_EXECUTE <> 1) AND START_TIME IS NOT NULL)";
                            break;
                        default://tất cả
                            break;
                    }
                }

                if (IsNotNull(tukhoa))
                {
                    query += string.Format(" AND (LOWER(P.VIR_PATIENT_NAME) LIKE '%{0}%' OR LOWER(P.MOBILE) LIKE '%{0}%' " +
                        "OR LOWER(P.PHONE) LIKE '%{0}%' OR LOWER(P.PROVINCE_NAME) LIKE '%{0}%' OR LOWER(P.DISTRICT_NAME) LIKE '%{0}%' " +
                        "OR LOWER(P.COMMUNE_NAME) LIKE '%{0}%' OR LOWER(P.ADDRESS) LIKE '%{0}%')", tukhoa.ToLower());
                }

                result = DAOWorker.SqlDAO.GetSql<DanhSachDangKyKhamQlpkSDO>(query);
                if (result == null)
                {
                    result = new List<DanhSachDangKyKhamQlpkSDO>();
                    Inventec.Common.Logging.LogSystem.Error(string.Format("khong tim thay danh sach dang ky kham theo dieu kien loc fromdate:{0}, todate:{1}, trangthai:{2},  tukhoa:{3}", fromdate, todate, trangthai, tukhoa));
                    Inventec.Common.Logging.LogSystem.Error("_____query: " + query);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
                param.HasException = true;
            }
            return result;
        }

        internal List<DuLieuDonThuocQlpkSDO> GetDuLieuDonThuocQlpk(string id, string date)
        {
            List<DuLieuDonThuocQlpkSDO> result = new List<DuLieuDonThuocQlpkSDO>();
            try
            {
                if (!IsNotNull(id) && !IsNotNull(date))
                {
                    return result;
                }

                Mapper.CreateMap<DuLieuDonThuocQlpkSDO, DuLieuDonThuocQlpkSDO>();

                string query = "SELECT P.PATIENT_CODE AS \"mabn\", P.VIR_PATIENT_NAME AS \"hoten\", SUBSTR(TO_CHAR(P.DOB),0,4) AS \"namsinh\", " +
                    "(CASE WHEN P.GENDER_ID = 1 THEN 1 ELSE 0 END) AS \"phai\", NVL(P.CCCD_NUMBER,P.CMND_NUMBER) AS \"cmnd\", T.TDL_HEIN_CARD_NUMBER AS \"sothe\", " +
                    "P.ETHNIC_NAME AS \"dantoc\", P.PROVINCE_NAME AS \"tentt\", P.DISTRICT_NAME AS \"tenquan\", P.COMMUNE_NAME AS \"tenpxa\", P.ADDRESS AS \"thon\", " +
                    "TO_DATE(T.IN_TIME, 'YYYYMMDDHH24MISS') AS \"ngaykham\", T.ICD_NAME AS \"chandoan\", T.ADVISE AS \"ghichu\", T.DOCTOR_LOGINNAME AS \"mabacsy\", T.DOCTOR_USERNAME AS \"bacsy\", " +
                    "(SELECT PATIENT_TYPE_NAME FROM HIS_PATIENT_TYPE WHERE ID = T.TDL_PATIENT_TYPE_ID) AS \"doituong\" " +
                    "FROM HIS_TREATMENT T JOIN HIS_PATIENT P ON T.PATIENT_ID = P.ID WHERE TDL_PATIENT_CODE = '{0}' AND IN_DATE = {1}";
                query = string.Format(query, id, ConvertTimeStringToTimeNumber(date, "000000"));

                List<DuLieuDonThuocQlpkSDO> listTreatment = DAOWorker.SqlDAO.GetSql<DuLieuDonThuocQlpkSDO>(query);
                Inventec.Common.Logging.LogSystem.Debug("GetDuLieuDonThuocQlpk__________1");
                if (listTreatment == null)
                {
                    Inventec.Common.Logging.LogSystem.Error(string.Format("khong tim thay benh nhan theo dieu kien loc id:{0}, date:{1}", id, date));
                    Inventec.Common.Logging.LogSystem.Error("_____query: " + query);
                }
                else
                {
                    List<string> listPatientCodes = listTreatment.Select(s => s.mabn).ToList();

                    string querryExpMest = string.Format("SELECT * FROM HIS_EXP_MEST WHERE TDL_PATIENT_CODE IN ('{0}') AND SERVICE_REQ_ID IS NOT NULL", string.Join("','", listPatientCodes));
                    Inventec.Common.Logging.LogSystem.Debug("GetDuLieuDonThuocQlpk__________2");

                    List<HIS_EXP_MEST> listExpMest = DAOWorker.SqlDAO.GetSql<HIS_EXP_MEST>(querryExpMest);

                    if (IsNotNullOrEmpty(listExpMest))
                    {
                        List<HIS_EXP_MEST_MEDICINE> listExpMestMedicine = new List<HIS_EXP_MEST_MEDICINE>();
                        List<HIS_SERVICE_REQ_METY> listReqMety = new List<HIS_SERVICE_REQ_METY>();
                        List<HIS_EXP_MEST_MATERIAL> listExpMestMaterial = new List<HIS_EXP_MEST_MATERIAL>();
                        List<HIS_SERVICE_REQ_MATY> listReqMaty = new List<HIS_SERVICE_REQ_MATY>();

                        string queryExpMedicine = "SELECT * FROM HIS_EXP_MEST_MEDICINE WHERE %IN_CLAUSE%";
                        string queryExpMaterial = "SELECT * FROM HIS_EXP_MEST_MATERIAL WHERE %IN_CLAUSE%";
                        string queryReqMety = "SELECT * FROM HIS_SERVICE_REQ_METY WHERE %IN_CLAUSE%";
                        string queryReqMaty = "SELECT * FROM HIS_SERVICE_REQ_MATY WHERE %IN_CLAUSE%";

                        int skip = 0;
                        while (listExpMest.Count - skip > 0)
                        {
                            List<HIS_EXP_MEST> lstExp = listExpMest.Skip(skip).Take(MaxReqCount).ToList();
                            skip += MaxReqCount;

                            string sqlExpMedicine = DAOWorker.SqlDAO.AddInClause(lstExp.Select(s => s.ID).ToList(), queryExpMedicine, "EXP_MEST_ID");
                            List<HIS_EXP_MEST_MEDICINE> expMedicines = DAOWorker.SqlDAO.GetSql<HIS_EXP_MEST_MEDICINE>(sqlExpMedicine);
                            if (IsNotNullOrEmpty(expMedicines))
                            {
                                listExpMestMedicine.AddRange(expMedicines);
                            }

                            string sqlReqMety = DAOWorker.SqlDAO.AddInClause(lstExp.Select(s => s.ID).ToList(), queryReqMety, "SERVICE_REQ_ID");
                            List<HIS_SERVICE_REQ_METY> reqMetys = DAOWorker.SqlDAO.GetSql<HIS_SERVICE_REQ_METY>(sqlReqMety);
                            if (IsNotNullOrEmpty(reqMetys))
                            {
                                listReqMety.AddRange(reqMetys);
                            }

                            string sqlExpMaterial = DAOWorker.SqlDAO.AddInClause(lstExp.Select(s => s.ID).ToList(), queryExpMaterial, "EXP_MEST_ID");
                            List<HIS_EXP_MEST_MATERIAL> expMaterials = DAOWorker.SqlDAO.GetSql<HIS_EXP_MEST_MATERIAL>(sqlExpMaterial);
                            if (IsNotNullOrEmpty(expMaterials))
                            {
                                listExpMestMaterial.AddRange(expMaterials);
                            }

                            string sqlReqMaty = DAOWorker.SqlDAO.AddInClause(lstExp.Select(s => s.ID).ToList(), queryReqMaty, "SERVICE_REQ_ID");
                            List<HIS_SERVICE_REQ_MATY> reqMatys = DAOWorker.SqlDAO.GetSql<HIS_SERVICE_REQ_MATY>(sqlReqMaty);
                            if (IsNotNullOrEmpty(reqMatys))
                            {
                                listReqMaty.AddRange(reqMatys);
                            }
                        }

                        foreach (var expMest in listExpMest)
                        {
                            List<HIS_EXP_MEST_MEDICINE> listMestMedicine = listExpMestMedicine.Where(o => o.EXP_MEST_ID == expMest.ID).ToList();
                            List<HIS_SERVICE_REQ_METY> listMety = listReqMety.Where(o => o.SERVICE_REQ_ID == expMest.SERVICE_REQ_ID).ToList();
                            List<HIS_EXP_MEST_MATERIAL> listMestMaterial = listExpMestMaterial.Where(o => o.EXP_MEST_ID == expMest.ID).ToList();
                            List<HIS_SERVICE_REQ_MATY> listMaty = listReqMaty.Where(o => o.SERVICE_REQ_ID == expMest.SERVICE_REQ_ID).ToList();
                            DuLieuDonThuocQlpkSDO treatment = listTreatment.FirstOrDefault(o => o.mabn == expMest.TDL_PATIENT_CODE);
                            if (IsNotNull(treatment) && (IsNotNullOrEmpty(listMestMedicine) || IsNotNullOrEmpty(listMety) || IsNotNullOrEmpty(listMestMaterial) || IsNotNullOrEmpty(listMaty)))
                            {
                                DuLieuDonThuocQlpkSDO sdo = Mapper.Map<DuLieuDonThuocQlpkSDO>(treatment);

                                sdo.ThuocDieuTri = new List<ThuocDieuTriQlpkSDO>();

                                long count = 1;
                                #region thuoc trong kho
                                if (IsNotNullOrEmpty(listMestMedicine))
                                {
                                    var groupMedicine = listMestMedicine.GroupBy(o => new { o.TDL_MEDICINE_TYPE_ID, o.PRICE, o.PATIENT_TYPE_ID, o.IS_EXPEND }).ToList();
                                    foreach (var medi in groupMedicine)
                                    {
                                        ThuocDieuTriQlpkSDO thuoc = new ThuocDieuTriQlpkSDO();

                                        var medicineType = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == medi.First().TDL_MEDICINE_TYPE_ID);
                                        if (!IsNotNull(medicineType))
                                            continue;

                                        thuoc.cachdung = medi.First().TUTORIAL;
                                        thuoc.ten = medicineType.MEDICINE_TYPE_NAME;
                                        thuoc.tenhc = medicineType.ACTIVE_INGR_BHYT_NAME;

                                        var serviceUnit = HisServiceUnitCFG.DATA.FirstOrDefault(o => o.ID == medicineType.TDL_SERVICE_UNIT_ID);
                                        if (IsNotNull(serviceUnit))
                                        {
                                            thuoc.dang = serviceUnit.SERVICE_UNIT_NAME;
                                        }

                                        if (medi.First().IS_EXPEND == Constant.IS_TRUE)
                                        {
                                            thuoc.madoituong = doituong.HaoPhi;
                                        }
                                        else if (medi.First().PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                                        {
                                            thuoc.madoituong = doituong.BHYT;
                                        }
                                        else if (medi.First().PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__HOSPITAL_FEE)
                                        {
                                            thuoc.madoituong = doituong.ThuPhi;
                                        }
                                        else if (medi.First().PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__SERVICE)
                                        {
                                            thuoc.madoituong = doituong.DichVu;
                                        }
                                        else
                                        {
                                            thuoc.madoituong = doituong.Khac;
                                        }

                                        thuoc.ngayud = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(expMest.FINISH_TIME ?? (expMest.CREATE_TIME ?? 0)) ?? DateTime.Now;
                                        thuoc.slyeucau = medi.Sum(s => s.AMOUNT);
                                        thuoc.tt = count;
                                        sdo.ThuocDieuTri.Add(thuoc);
                                        count++;
                                    }
                                }
                                #endregion

                                #region vat tu trong kho
                                if (IsNotNullOrEmpty(listMestMaterial))
                                {
                                    var groupMaterial = listMestMaterial.GroupBy(o => new { o.TDL_MATERIAL_TYPE_ID, o.PRICE, o.PATIENT_TYPE_ID, o.IS_EXPEND }).ToList();
                                    foreach (var medi in groupMaterial)
                                    {
                                        ThuocDieuTriQlpkSDO thuoc = new ThuocDieuTriQlpkSDO();

                                        var materialType = HisMaterialTypeCFG.DATA.FirstOrDefault(o => o.ID == medi.First().TDL_MATERIAL_TYPE_ID);
                                        if (!IsNotNull(materialType))
                                            continue;

                                        thuoc.cachdung = medi.First().TUTORIAL;
                                        thuoc.ten = materialType.MATERIAL_TYPE_NAME;
                                        thuoc.tenhc = materialType.MATERIAL_TYPE_NAME;

                                        var serviceUnit = HisServiceUnitCFG.DATA.FirstOrDefault(o => o.ID == materialType.TDL_SERVICE_UNIT_ID);
                                        if (IsNotNull(serviceUnit))
                                        {
                                            thuoc.dang = serviceUnit.SERVICE_UNIT_NAME;
                                        }

                                        if (medi.First().IS_EXPEND == Constant.IS_TRUE)
                                        {
                                            thuoc.madoituong = doituong.HaoPhi;
                                        }
                                        else if (medi.First().PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                                        {
                                            thuoc.madoituong = doituong.BHYT;
                                        }
                                        else if (medi.First().PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__HOSPITAL_FEE)
                                        {
                                            thuoc.madoituong = doituong.ThuPhi;
                                        }
                                        else if (medi.First().PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__SERVICE)
                                        {
                                            thuoc.madoituong = doituong.DichVu;
                                        }
                                        else
                                        {
                                            thuoc.madoituong = doituong.Khac;
                                        }

                                        thuoc.ngayud = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(expMest.FINISH_TIME ?? (expMest.CREATE_TIME ?? 0)) ?? DateTime.Now;
                                        thuoc.slyeucau = medi.Sum(s => s.AMOUNT);
                                        thuoc.tt = count;
                                        sdo.ThuocDieuTri.Add(thuoc);
                                        count++;
                                    }
                                }
                                #endregion

                                #region thuoc ngoai kho
                                if (IsNotNullOrEmpty(listMety))
                                {
                                    var groupMedicine = listMety.GroupBy(o => new { o.MEDICINE_TYPE_ID, o.MEDICINE_TYPE_NAME, o.PRICE }).ToList();
                                    foreach (var medi in groupMedicine)
                                    {
                                        ThuocDieuTriQlpkSDO thuoc = new ThuocDieuTriQlpkSDO();

                                        var medicineType = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == medi.First().MEDICINE_TYPE_ID);
                                        if (IsNotNull(medicineType))
                                        {
                                            thuoc.ten = medicineType.MEDICINE_TYPE_NAME;
                                            thuoc.tenhc = medicineType.ACTIVE_INGR_BHYT_NAME;

                                            var serviceUnit = HisServiceUnitCFG.DATA.FirstOrDefault(o => o.ID == medicineType.TDL_SERVICE_UNIT_ID);
                                            if (IsNotNull(serviceUnit))
                                            {
                                                thuoc.dang = serviceUnit.SERVICE_UNIT_NAME;
                                            }
                                        }

                                        if (IsNotNull(thuoc.ten))
                                        {
                                            thuoc.ten = medi.First().MEDICINE_TYPE_NAME;
                                        }

                                        if (IsNotNull(thuoc.tenhc))
                                        {
                                            thuoc.tenhc = medi.First().MEDICINE_TYPE_NAME;
                                        }

                                        if (IsNotNull(thuoc.dang))
                                        {
                                            thuoc.dang = medi.First().UNIT_NAME;
                                        }

                                        thuoc.cachdung = medi.First().TUTORIAL;
                                        thuoc.madoituong = doituong.ThuPhi;
                                        thuoc.ngayud = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(expMest.FINISH_TIME ?? (expMest.CREATE_TIME ?? 0)) ?? DateTime.Now;
                                        thuoc.slyeucau = medi.Sum(s => s.AMOUNT);
                                        thuoc.tt = count;
                                        sdo.ThuocDieuTri.Add(thuoc);
                                        count++;
                                    }
                                }
                                #endregion

                                #region vat tu ngoai kho
                                if (IsNotNullOrEmpty(listMaty))
                                {
                                    var groupMaterial = listMaty.GroupBy(o => new { o.MATERIAL_TYPE_ID, o.MATERIAL_TYPE_NAME, o.PRICE }).ToList();
                                    foreach (var medi in groupMaterial)
                                    {
                                        ThuocDieuTriQlpkSDO thuoc = new ThuocDieuTriQlpkSDO();

                                        var materialType = HisMaterialTypeCFG.DATA.FirstOrDefault(o => o.ID == medi.First().MATERIAL_TYPE_ID);
                                        if (IsNotNull(materialType))
                                        {
                                            thuoc.ten = materialType.MATERIAL_TYPE_NAME;
                                            thuoc.tenhc = materialType.MATERIAL_TYPE_NAME;

                                            var serviceUnit = HisServiceUnitCFG.DATA.FirstOrDefault(o => o.ID == materialType.TDL_SERVICE_UNIT_ID);
                                            if (IsNotNull(serviceUnit))
                                            {
                                                thuoc.dang = serviceUnit.SERVICE_UNIT_NAME;
                                            }
                                        }

                                        if (IsNotNull(thuoc.ten))
                                        {
                                            thuoc.ten = medi.First().MATERIAL_TYPE_NAME;
                                        }

                                        if (IsNotNull(thuoc.tenhc))
                                        {
                                            thuoc.tenhc = medi.First().MATERIAL_TYPE_NAME;
                                        }

                                        if (IsNotNull(thuoc.dang))
                                        {
                                            thuoc.dang = medi.First().UNIT_NAME;
                                        }

                                        thuoc.cachdung = medi.First().TUTORIAL;
                                        thuoc.madoituong = doituong.ThuPhi;
                                        thuoc.ngayud = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(expMest.FINISH_TIME ?? (expMest.CREATE_TIME ?? 0)) ?? DateTime.Now;
                                        thuoc.slyeucau = medi.Sum(s => s.AMOUNT);
                                        thuoc.tt = count;
                                        sdo.ThuocDieuTri.Add(thuoc);
                                        count++;
                                    }
                                }
                                #endregion

                                result.Add(sdo);
                            }
                        }
                        Inventec.Common.Logging.LogSystem.Debug("GetDuLieuDonThuocQlpk__________4");
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
                param.HasException = true;
            }
            return result;
        }

        internal DuLieuXetNghiemQlpkSDO GetDuLieuXetNghiemQlpk(string id, string date, string loaidulieu)
        {
            DuLieuXetNghiemQlpkSDO result = new DuLieuXetNghiemQlpkSDO();
            try
            {
                if (!IsNotNull(id) && !IsNotNull(date))
                {
                    return result;
                }

                V_HIS_PATIENT hisPatient = DAOWorker.SqlDAO.GetSqlSingle<V_HIS_PATIENT>("SELECT * FROM V_HIS_PATIENT WHERE PATIENT_CODE = :param", id);

                if (!IsNotNull(hisPatient))
                {
                    return result;
                }

                string queryTreatment = string.Format("SELECT * FROM HIS_TREATMENT WHERE TDL_PATIENT_CODE = '{0}' AND IN_DATE = {1}", id, ConvertTimeStringToTimeNumber(date, "000000"));
                List<HIS_TREATMENT> listTreatment = DAOWorker.SqlDAO.GetSql<HIS_TREATMENT>(queryTreatment);
                if (listTreatment == null)
                {
                    Inventec.Common.Logging.LogSystem.Error(string.Format("khong tim thay ho so dieu tri theo dieu kien loc id:{0}, date:{1}", id, date));
                    Inventec.Common.Logging.LogSystem.Error("_____query: " + queryTreatment);
                }
                else
                {
                    List<HIS_SERVICE_REQ> listServiceReq = new List<HIS_SERVICE_REQ>();
                    List<HIS_SERE_SERV> listSereServ = new List<HIS_SERE_SERV>();
                    List<V_HIS_SERE_SERV_TEIN> listSereServTein = new List<V_HIS_SERE_SERV_TEIN>();
                    List<HIS_PATIENT_TYPE_ALTER> listPatientTypeAlter = new List<HIS_PATIENT_TYPE_ALTER>();

                    //có loaidulieu 4 - xét nghiệm huyết học| 10 - xét nghiệm miễn dịch
                    //lấy danh sách dịch vụ xét nghiệm có TEST_TYPE_ID tương ứng với loaidulieu để lọc 
                    List<HIS_SERVICE> listService = new List<HIS_SERVICE>();

                    if (IsNotNull(loaidulieu) && (loaidulieu == "4" || loaidulieu == "10"))
                    {
                        long testTypeId = 0;
                        switch (loaidulieu)
                        {
                            case "4":
                                testTypeId = 1;//IMSys.DbConfig.HIS_RS.HIS_TEST_TYPE.ID__HH
                                break;
                            case "10":
                                testTypeId = 4;//IMSys.DbConfig.HIS_RS.HIS_TEST_TYPE.ID__MD
                                break;
                            default:
                                break;
                        }
                        listService = DAOWorker.SqlDAO.GetSql<HIS_SERVICE>(string.Format("SELECT * FROM HIS_SERVICE WHERE SERVICE_TYPE_ID = {0} AND TEST_TYPE_ID = {1}", IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__XN, testTypeId));
                    }

                    string queryServiceReq = string.Format("SELECT * FROM HIS_SERVICE_REQ WHERE %IN_CLAUSE% AND IS_DELETE = 0 AND SERVICE_REQ_TYPE_ID = {0}", IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN);
                    string querySereServ = string.Format("SELECT * FROM HIS_SERE_SERV WHERE %IN_CLAUSE%  AND IS_DELETE = 0 AND TDL_SERVICE_TYPE_ID = {0} ", IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__XN);
                    string querySereServTein = "SELECT * FROM V_HIS_SERE_SERV_TEIN WHERE %IN_CLAUSE%";
                    string queryPatientTypeAlter = "SELECT * FROM HIS_PATIENT_TYPE_ALTER WHERE %IN_CLAUSE%";

                    if (IsNotNull(loaidulieu))
                    {
                        querySereServ = DAOWorker.SqlDAO.AddInClause(listService.Select(s => s.ID).ToList(), querySereServ, "SERVICE_ID");
                        querySereServ += " AND  %IN_CLAUSE% ";
                    }

                    int skip = 0;
                    while (listTreatment.Count - skip > 0)
                    {
                        List<HIS_TREATMENT> lstTreat = listTreatment.Skip(skip).Take(MaxReqCount).ToList();
                        skip += MaxReqCount;

                        string sqlReq = DAOWorker.SqlDAO.AddInClause(lstTreat.Select(s => s.ID).ToList(), queryServiceReq, "TREATMENT_ID");
                        List<HIS_SERVICE_REQ> reqs = DAOWorker.SqlDAO.GetSql<HIS_SERVICE_REQ>(sqlReq);
                        if (IsNotNullOrEmpty(reqs))
                        {
                            listServiceReq.AddRange(reqs);
                        }

                        string sqlSereServ = DAOWorker.SqlDAO.AddInClause(lstTreat.Select(s => s.ID).ToList(), querySereServ, "TDL_TREATMENT_ID");
                        List<HIS_SERE_SERV> sereServs = DAOWorker.SqlDAO.GetSql<HIS_SERE_SERV>(sqlSereServ);
                        if (IsNotNullOrEmpty(sereServs))
                        {
                            listSereServ.AddRange(sereServs);

                            int skip2 = 0;
                            while (sereServs.Count - skip2 > 0)
                            {
                                List<HIS_SERE_SERV> lstSs = sereServs.Skip(skip2).Take(MaxReqCount).ToList();
                                skip2 += MaxReqCount;

                                string sqlSsTein = DAOWorker.SqlDAO.AddInClause(lstSs.Select(s => s.ID).ToList(), querySereServTein, "SERE_SERV_ID");
                                List<V_HIS_SERE_SERV_TEIN> ssTeins = DAOWorker.SqlDAO.GetSql<V_HIS_SERE_SERV_TEIN>(sqlSsTein);
                                if (IsNotNullOrEmpty(ssTeins))
                                {
                                    listSereServTein.AddRange(ssTeins);
                                }
                            }
                        }

                        string sqlPta = DAOWorker.SqlDAO.AddInClause(lstTreat.Select(s => s.ID).ToList(), queryPatientTypeAlter, "TREATMENT_ID");
                        List<HIS_PATIENT_TYPE_ALTER> patyAlter = DAOWorker.SqlDAO.GetSql<HIS_PATIENT_TYPE_ALTER>(sqlPta);
                        if (IsNotNullOrEmpty(patyAlter))
                        {
                            listPatientTypeAlter.AddRange(patyAlter);
                        }
                    }

                    #region ChiDinhXetNghiem
                    if (IsNotNullOrEmpty(listServiceReq))
                    {
                        result.ChiDinhXetNghiem = new List<ChiDinhXetNghiemQlpkSDO>();
                        foreach (var serviceReq in listServiceReq)
                        {
                            var sereServs = listSereServ.Where(o => o.SERVICE_REQ_ID == serviceReq.ID).ToList();
                            var treat = listTreatment.FirstOrDefault(o => o.ID == serviceReq.TREATMENT_ID);
                            var patyAlters = listPatientTypeAlter.Where(o => o.TREATMENT_ID == serviceReq.TREATMENT_ID && o.LOG_TIME <= serviceReq.INTRUCTION_TIME).ToList();

                            if (!IsNotNullOrEmpty(sereServs) || !IsNotNull(treat))
                            {
                                continue;
                            }

                            foreach (var item in sereServs)
                            {
                                ChiDinhXetNghiemQlpkSDO sdo = new ChiDinhXetNghiemQlpkSDO();
                                sdo.mabn = hisPatient.PATIENT_CODE;
                                sdo.dantoc = hisPatient.ETHNIC_NAME;
                                sdo.diachi = hisPatient.VIR_ADDRESS;
                                sdo.didong = hisPatient.PHONE;
                                sdo.dienthoai = hisPatient.MOBILE;
                                sdo.hoten = hisPatient.VIR_PATIENT_NAME;
                                sdo.namsinh = hisPatient.DOB.ToString().Substring(0, 4);
                                sdo.ngaysinh = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(hisPatient.DOB);
                                sdo.noilamviec = hisPatient.WORK_PLACE ?? hisPatient.WORK_PLACE_NAME;
                                sdo.phai = hisPatient.GENDER_ID == IMSys.DbConfig.HIS_RS.HIS_GENDER.ID__FEMALE ? 1 : 0;
                                sdo.tuoi = Inventec.Common.DateTime.Calculation.Age(hisPatient.DOB);
                                sdo.tuoivao = "" + Inventec.Common.DateTime.Calculation.Age(hisPatient.DOB, treat.IN_TIME);
                                sdo.noigioithieu = treat.MEDI_ORG_NAME;
                                sdo.soluutru = treat.STORE_CODE;

                                sdo.denngay = Inventec.Common.DateTime.Convert.TimeNumberToDateString(treat.TDL_HEIN_CARD_TO_TIME ?? 0);
                                sdo.sothe = treat.TDL_HEIN_CARD_NUMBER;
                                sdo.tungay = Inventec.Common.DateTime.Convert.TimeNumberToDateString(treat.TDL_HEIN_CARD_FROM_TIME ?? 0);

                                //tuyến "0 là trái tuyến 1 là đúng tuyến"
                                sdo.traituyen = 0;
                                if (patyAlters != null && patyAlters.Count > 0 && patyAlters.OrderByDescending(o => o.LOG_TIME).First().RIGHT_ROUTE_CODE == MOS.LibraryHein.Bhyt.HeinRightRoute.HeinRightRouteCode.TRUE)
                                {
                                    sdo.traituyen = 1;
                                }

                                sdo.maicd = serviceReq.ICD_CODE;
                                sdo.chandoan = serviceReq.ICD_NAME;
                                sdo.mabs = serviceReq.REQUEST_LOGINNAME;
                                sdo.tenbs = serviceReq.REQUEST_USERNAME;
                                sdo.ngay = Inventec.Common.DateTime.Convert.TimeNumberToTimeStringWithoutSecond(serviceReq.INTRUCTION_DATE);
                                sdo.thoigiantrakq = serviceReq.FINISH_TIME.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToTimeStringWithoutSecond(serviceReq.FINISH_TIME ?? 0) : null;
                                sdo.tinhtrang = serviceReq.IS_EMERGENCY == 1 ? "Cấp cứu" : "Thường";//thường, cấp cứu
                                sdo.ghichu = serviceReq.NOTE;
                                sdo.stt = serviceReq.NUM_ORDER ?? 0;
                                var room = HisRoomCFG.DATA.FirstOrDefault(o => o.ID == serviceReq.REQUEST_ROOM_ID);
                                sdo.tenkp = IsNotNull(room) ? room.ROOM_NAME : " ";

                                sdo.ten = item.TDL_SERVICE_NAME;
                                var serviceUnit = HisServiceUnitCFG.DATA.FirstOrDefault(o => o.ID == item.TDL_SERVICE_UNIT_ID);
                                sdo.dvt = IsNotNull(serviceUnit) ? serviceUnit.SERVICE_UNIT_NAME : "";
                                var patientType = HisPatientTypeCFG.DATA.FirstOrDefault(o => o.ID == item.PATIENT_TYPE_ID);
                                sdo.doituong = IsNotNull(patientType) ? patientType.PATIENT_TYPE_NAME : "Khác";

                                sdo.dongia = item.VIR_PRICE ?? 0;
                                if (item.IS_EXPEND == Constant.IS_TRUE)
                                {
                                    sdo.madoituong = doituong.HaoPhi;
                                }
                                else if (item.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__HOSPITAL_FEE)
                                {
                                    sdo.madoituong = doituong.ThuPhi;
                                }
                                else if (item.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                                {
                                    sdo.madoituong = doituong.BHYT;
                                    sdo.gia_bh = item.VIR_PRICE ?? 0;
                                }
                                else if (item.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__SERVICE)
                                {
                                    sdo.madoituong = doituong.DichVu;
                                    sdo.gia_dv = item.VIR_PRICE ?? 0;
                                }
                                else
                                {
                                    sdo.madoituong = doituong.Khac;
                                }

                                sdo.chenhlech = 0;
                                sdo.gia_cs = 0;
                                sdo.gia_ksk = 0;
                                sdo.gia_nn = 0;
                                sdo.gia_th = 0;
                                sdo.soluong = (long)item.AMOUNT;

                                sdo.loaiba = 0;
                                sdo.loaivp = 0;
                                sdo.mavp = 0;
                                sdo.idnhomvp = 0;
                                sdo.nhomvp = "";
                                sdo.tenloaivp = "";
                                sdo.tennhomvp = "";
                                sdo.tennn = "";
                                sdo.sttloai = 0;
                                sdo.sttnhom = 0;

                                sdo.sttcho = 0;
                                sdo.trangthai = 0;
                                sdo.trieuchung = "";
                                sdo.cholam = "";
                                sdo.giongay = null;
                                sdo.giaycamdoan = 0;
                                sdo.nguoiin = "";
                                sdo.giuong = "";
                                sdo.thuchien = "";

                                result.ChiDinhXetNghiem.Add(sdo);
                            }
                        }
                    }
                    #endregion

                    #region ketqua
                    if (IsNotNullOrEmpty(listSereServTein))
                    {
                        result.KetQua = new List<KetQuaQlpkSDO>();
                        foreach (var sereServTein in listSereServTein)
                        {
                            var sereServ = listSereServ.FirstOrDefault(o => o.ID == sereServTein.SERE_SERV_ID);
                            if (!IsNotNull(sereServ))
                                continue;

                            var serviceReq = listServiceReq.FirstOrDefault(o => o.ID == sereServ.SERVICE_REQ_ID);
                            if (!IsNotNull(serviceReq))
                                continue;

                            var treat = listTreatment.FirstOrDefault(o => o.ID == serviceReq.TREATMENT_ID);
                            if (!IsNotNull(treat))
                                continue;

                            KetQuaQlpkSDO sdo = new KetQuaQlpkSDO();
                            sdo.mabn = hisPatient.PATIENT_CODE;
                            sdo.hoten = hisPatient.VIR_PATIENT_NAME;
                            sdo.ngaysinh = hisPatient.DOB.ToString().Substring(0, 4);
                            sdo.phai = hisPatient.GENDER_NAME;
                            sdo.matt = hisPatient.PROVINCE_CODE;
                            sdo.tentt = hisPatient.PROVINCE_NAME;
                            sdo.maqu = hisPatient.DISTRICT_CODE;
                            sdo.tenquan = hisPatient.DISTRICT_NAME;
                            sdo.maphuongxa = hisPatient.COMMUNE_CODE;
                            sdo.tenpxa = hisPatient.COMMUNE_NAME;
                            sdo.thon = hisPatient.ADDRESS;
                            sdo.sonha = "";

                            sdo.denngay = Inventec.Common.DateTime.Convert.TimeNumberToDateString(treat.TDL_HEIN_CARD_TO_TIME ?? 0);
                            sdo.sothe = treat.TDL_HEIN_CARD_NUMBER;
                            sdo.tungay = Inventec.Common.DateTime.Convert.TimeNumberToDateString(treat.TDL_HEIN_CARD_FROM_TIME ?? 0);
                            sdo.mabv = treat.TDL_HEIN_MEDI_ORG_CODE;

                            sdo.maicd = serviceReq.ICD_CODE;
                            sdo.chandoan = serviceReq.ICD_NAME;
                            sdo.ghichu = serviceReq.NOTE;
                            sdo.ktv = "";
                            sdo.tenktv = "";
                            var room = HisRoomCFG.DATA.FirstOrDefault(o => o.ID == serviceReq.REQUEST_ROOM_ID);
                            sdo.makp = IsNotNull(room) ? room.ROOM_CODE : " ";
                            sdo.tenkp = IsNotNull(room) ? room.ROOM_NAME : " ";
                            sdo.mabs = serviceReq.REQUEST_LOGINNAME;
                            sdo.tenbs = serviceReq.REQUEST_USERNAME;
                            sdo.ngay = Inventec.Common.DateTime.Convert.TimeNumberToTimeStringWithoutSecond(serviceReq.INTRUCTION_DATE);
                            sdo.mabsdoc = serviceReq.EXECUTE_LOGINNAME;
                            sdo.tenbsdoc = serviceReq.EXECUTE_USERNAME;

                            sdo.cholam = "";

                            var patientType = HisPatientTypeCFG.DATA.FirstOrDefault(o => o.ID == sereServ.PATIENT_TYPE_ID);
                            sdo.doituong = IsNotNull(patientType) ? patientType.PATIENT_TYPE_NAME : "Khác";
                            sdo.tendv = sereServ.TDL_SERVICE_NAME;
                            sdo.ten_bv_ten = sereServ.TDL_SERVICE_NAME;

                            sdo.nhanxet = "";
                            sdo.ketluan = "";

                            var testIndexRange = HisTestIndexRangeCFG.DATA.Where(o => o.TEST_INDEX_ID == sereServTein.TEST_INDEX_ID);
                            foreach (var item in testIndexRange)
                            {
                                if (item.IS_FEMALE == Constant.IS_TRUE)
                                {
                                    sdo.csbt_nu = item.NORMAL_VALUE;
                                }

                                if (item.IS_MALE == Constant.IS_TRUE)
                                {
                                    sdo.csbt_nam = item.NORMAL_VALUE;
                                }
                            }

                            sdo.ketqua_ct = sereServTein.DESCRIPTION;
                            sdo.ghichu_ct = sereServTein.VALUE;

                            sdo.ten_ten = sereServTein.TEST_INDEX_NAME;
                            sdo.ten_nhom = sereServTein.TEST_INDEX_GROUP_NAME;
                            sdo.dvt = sereServTein.TEST_INDEX_UNIT_NAME;

                            sdo.ngaylaymau = "";
                            sdo.nguoinhap = "";

                            sdo.ten_bv_so = "";
                            sdo.ten_loai = "";

                            sdo.id = 0;
                            sdo.id_bv_chitiet = 0;
                            sdo.id_bv_so = 0;
                            sdo.id_bv_ten = 0;
                            sdo.id_loai = 0;
                            sdo.id_nhom = 0;
                            sdo.id_ten = 0;

                            sdo.maql = 0;
                            sdo.sophieu = 0;
                            sdo.stt_bv_ten = 0;
                            sdo.stt_chitiet = 0;
                            sdo.stt_so = 0;
                            sdo.stt_ten = 0;
                            sdo.sttlaymau = "";
                            sdo.tendoan = "";

                            sdo.denghi = "";
                            sdo.lanin = 0;
                            sdo.userid = 0;
                            sdo.vviet = "";

                            result.KetQua.Add(sdo);
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
                param.HasException = true;
            }
            return result;
        }

        private long ConvertTimeStringToTimeNumber(string time, string hour)
        {
            long result = 0;
            try
            {
                if (!String.IsNullOrWhiteSpace(time))
                {
                    string[] timeAr = time.Split('-', ' ', ':');
                    if (timeAr != null && timeAr.Count() > 0)
                    {
                        string t = "";
                        foreach (var item in timeAr)
                        {
                            t += item;
                        }

                        t += hour;

                        try
                        {
                            result = Convert.ToInt64(t.Trim());
                        }
                        catch (Exception ex)
                        {
                            Inventec.Common.Logging.LogSystem.Error(ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = 0;
            }
            return result;
        }

        private class doituong
        {
            public const long BHYT = 1;
            public const long ThuPhi = 2;
            public const long Khac = 4;
            public const long HaoPhi = 5;
            public const long DichVu = 8;
        }
    }
}
