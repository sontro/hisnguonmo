using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisBidType;
using MOS.MANAGER.HisBloodType;
using MOS.MANAGER.HisManufacturer;
using MOS.MANAGER.HisMaterialType;
using MOS.MANAGER.HisMaterialTypeMap;
using MOS.MANAGER.HisMedicineType;
using MOS.MANAGER.HisSupplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisBid
{
    class HisBidLogUtil
    {
        internal static void ProcessLogMaterial(List<HIS_BID_MATERIAL_TYPE> createList, List<HIS_BID_MATERIAL_TYPE> updateList, List<HIS_BID_MATERIAL_TYPE> beforeList, List<HIS_BID_MATERIAL_TYPE> deleteList, ref List<HIS_SUPPLIER> suppliers, ref List<HIS_MANUFACTURER> manufacturers, ref List<string> logs)
        {
            try
            {
                string vt = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.VatTu);
                if (!String.IsNullOrWhiteSpace(vt)) vt = vt.ToUpper();
                List<string> cruds = new List<string>();
                if (createList != null && createList.Count > 0)
                {
                    List<string> list = new List<string>();
                    string them = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Them);
                    foreach (var item in createList)
                    {
                        string l = GenerateLogMaterial(suppliers, manufacturers, item);
                        if (!String.IsNullOrWhiteSpace(l)) list.Add(l);
                    }
                    if (list.Count > 0) cruds.Add(String.Format("{0} ({1})", them, String.Join(", ", list)));
                }

                if (updateList != null && updateList.Count > 0)
                {
                    List<string> list = new List<string>();
                    string sua = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Sua);
                    foreach (var item in updateList)
                    {
                        var before = beforeList.FirstOrDefault(o => o.ID == item.ID);
                        string l = GenerateLogUpdateMaterial(suppliers, manufacturers, item, before);
                        if (!String.IsNullOrWhiteSpace(l)) list.Add(l);
                    }
                    if (list.Count > 0) cruds.Add(String.Format("{0} ({1})", sua, String.Join(", ", list)));
                }

                if (deleteList != null && deleteList.Count > 0)
                {
                    List<string> list = new List<string>();
                    string xoa = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Xoa);
                    foreach (var item in deleteList)
                    {
                        string l = GenerateLogDeleteMaterial(item, suppliers);
                        if (!String.IsNullOrWhiteSpace(l)) list.Add(l);
                    }
                    if (list.Count > 0) cruds.Add(String.Format("{0} ({1})", xoa, String.Join(", ", list)));
                }
                if (cruds.Count > 0)
                {
                    string log = String.Format("{0} ({1})", vt, String.Join(", ", cruds));
                    logs.Add(log);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private static string GenerateLogMaterial(List<HIS_SUPPLIER> suppliers, List<HIS_MANUFACTURER> manufacturers, HIS_BID_MATERIAL_TYPE item)
        {
            string l = "";
            try
            {
                List<string> list = new List<string>();

                list.Add(String.Format("SL: {0}", item.AMOUNT));

                var supp = suppliers.FirstOrDefault(o => o.ID == item.SUPPLIER_ID);
                if (supp == null)
                {
                    supp = new HisSupplierGet().GetById(item.SUPPLIER_ID);
                    suppliers.Add(supp);
                }

                string sup = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NhaThau);
                list.Add(String.Format("{0}: {1}", sup, supp.SUPPLIER_NAME));

                if (item.IMP_PRICE.HasValue)
                {
                    string price = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GiaNhap);
                    list.Add(String.Format("{0} {1}", price, item.IMP_PRICE.Value));
                }
                if (item.IMP_VAT_RATIO.HasValue)
                {
                    string vat = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.VatNhap);
                    list.Add(String.Format("{0} {1}", vat, item.IMP_VAT_RATIO.Value));
                }
                if (item.EXPIRED_DATE.HasValue)
                {
                    string han = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.HanDung);
                    list.Add(String.Format("{0}: {1}", han, Inventec.Common.DateTime.Convert.TimeNumberToDateString(item.EXPIRED_DATE.Value)));
                }
                if (!String.IsNullOrWhiteSpace(item.BID_NUM_ORDER))
                {
                    string stt = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.STTThau);
                    list.Add(String.Format("{0}: {1}", stt, item.BID_NUM_ORDER));
                }
                if (!String.IsNullOrWhiteSpace(item.BID_GROUP_CODE))
                {
                    string group = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NhomThau);
                    list.Add(String.Format("{0}: {1}", group, item.BID_GROUP_CODE));
                }
                if (!String.IsNullOrWhiteSpace(item.BID_PACKAGE_CODE))
                {
                    string pack = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GoiThau);
                    list.Add(String.Format("{0}: {1}", pack, item.BID_PACKAGE_CODE));
                }
                if (!String.IsNullOrWhiteSpace(item.CONCENTRA))
                {
                    string concentra = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.HamLuong);
                    list.Add(String.Format("{0}: {1}", concentra, item.CONCENTRA));
                }
                if (item.MANUFACTURER_ID.HasValue)
                {
                    var manu = manufacturers.FirstOrDefault(o => o.ID == item.MANUFACTURER_ID.Value);
                    if (manu == null)
                    {
                        manu = new HisManufacturerGet().GetById(item.MANUFACTURER_ID.Value);
                        manufacturers.Add(manu);
                    }
                    string ma = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.HangSanXuat);
                    list.Add(String.Format("{0}: {1}", ma, manu.MANUFACTURER_NAME));
                }
                if (!String.IsNullOrWhiteSpace(item.NATIONAL_NAME))
                {
                    string national = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.QuocGia);
                    list.Add(String.Format("{0}: {1}", national, item.NATIONAL_NAME));
                }
                if (!String.IsNullOrWhiteSpace(item.NOTE))
                {
                    string note = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GhiChu);
                    list.Add(String.Format("{0}: {1}", note, item.NOTE));
                }
                if (list.Count > 0)
                {
                    l = String.Format("{0} ({1})", GetTypeName(item), String.Join(", ", list));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                l = "";
            }
            return l;
        }

        private static string GenerateLogUpdateMaterial(List<HIS_SUPPLIER> suppliers, List<HIS_MANUFACTURER> manufacturers, HIS_BID_MATERIAL_TYPE item, HIS_BID_MATERIAL_TYPE before)
        {
            string l = "";
            try
            {
                List<string> list = new List<string>();

                if (item.AMOUNT != before.AMOUNT)
                {
                    list.Add(String.Format("SL: {0}=>{1}", before.AMOUNT, item.AMOUNT));
                }

                if (item.SUPPLIER_ID != before.SUPPLIER_ID)
                {
                    string newName = "";
                    string oldName = "";
                    var newS = suppliers.FirstOrDefault(o => o.ID == item.SUPPLIER_ID);
                    if (newS == null)
                    {
                        newS = new HisSupplierGet().GetById(item.SUPPLIER_ID);
                        suppliers.Add(newS);
                    }
                    newName = newS.SUPPLIER_NAME;
                    var old = suppliers.FirstOrDefault(o => o.ID == before.SUPPLIER_ID);
                    if (newS == null)
                    {
                        old = new HisSupplierGet().GetById(before.SUPPLIER_ID);
                        suppliers.Add(old);
                    }
                    oldName = old.SUPPLIER_NAME;
                    string sup = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NhaThau);
                    list.Add(String.Format("{0}: {1}=>{2}", sup, oldName, newName));
                }

                if ((item.IMP_PRICE ?? -1) != (before.IMP_PRICE ?? -1))
                {
                    string newName = "";
                    string oldName = "";
                    if (item.IMP_PRICE.HasValue)
                    {
                        newName = item.IMP_PRICE.Value.ToString();
                    }
                    if (before.IMP_PRICE.HasValue)
                    {
                        oldName = before.IMP_PRICE.Value.ToString();
                    }
                    string price = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GiaNhap);
                    list.Add(String.Format("{0} {1}=>{2}", price, oldName, newName));
                }
                if ((item.IMP_VAT_RATIO ?? -1) != (before.IMP_VAT_RATIO ?? -1))
                {
                    string newName = "";
                    string oldName = "";
                    if (item.IMP_VAT_RATIO.HasValue)
                    {
                        newName = item.IMP_VAT_RATIO.Value.ToString();
                    }
                    if (before.IMP_VAT_RATIO.HasValue)
                    {
                        oldName = before.IMP_VAT_RATIO.Value.ToString();
                    }
                    string vat = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.VatNhap);
                    list.Add(String.Format("{0} {1}=>{2}", vat, oldName, newName));
                }
                if ((item.EXPIRED_DATE ?? 0) != (before.EXPIRED_DATE ?? 0))
                {
                    string newName = "";
                    string oldName = "";
                    if (item.EXPIRED_DATE.HasValue)
                    {
                        newName = Inventec.Common.DateTime.Convert.TimeNumberToDateString(item.EXPIRED_DATE.Value);
                    }
                    if (before.EXPIRED_DATE.HasValue)
                    {
                        oldName = Inventec.Common.DateTime.Convert.TimeNumberToDateString(before.EXPIRED_DATE.Value);
                    }
                    string han = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.HanDung);
                    list.Add(String.Format("{0}: {1}=>{2}", han, oldName, newName));
                }
                if ((item.BID_NUM_ORDER ?? "") != (before.BID_NUM_ORDER ?? ""))
                {
                    string newName = item.BID_NUM_ORDER ?? "";
                    string oldName = before.BID_NUM_ORDER ?? "";
                    string stt = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.STTThau);
                    list.Add(String.Format("{0}: {1}=>{2}", stt, oldName, newName));
                }
                if ((item.BID_GROUP_CODE ?? "") != (before.BID_GROUP_CODE ?? ""))
                {
                    string newName = item.BID_GROUP_CODE ?? "";
                    string oldName = before.BID_GROUP_CODE ?? "";
                    string group = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NhomThau);
                    list.Add(String.Format("{0}: {1}=>{2}", group, oldName, newName));
                }
                if ((item.BID_PACKAGE_CODE ?? "") != (before.BID_PACKAGE_CODE ?? ""))
                {
                    string newName = item.BID_PACKAGE_CODE ?? "";
                    string oldName = before.BID_PACKAGE_CODE ?? "";
                    string pack = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GoiThau);
                    list.Add(String.Format("{0}: {1}=>{2}", pack, oldName, newName));
                }
                if ((item.CONCENTRA ?? "") != (before.CONCENTRA ?? ""))
                {
                    string newName = item.CONCENTRA ?? "";
                    string oldName = before.CONCENTRA ?? "";
                    string concentra = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.HamLuong);
                    list.Add(String.Format("{0}: {1}=>{2}", concentra, oldName, newName));
                }
                if ((item.MANUFACTURER_ID ?? 0) != (before.MANUFACTURER_ID ?? 0))
                {
                    string newName = "";
                    string oldName = "";
                    if (item.MANUFACTURER_ID.HasValue)
                    {
                        var manu = manufacturers.FirstOrDefault(o => o.ID == item.MANUFACTURER_ID.Value);
                        if (manu == null)
                        {
                            manu = new HisManufacturerGet().GetById(item.MANUFACTURER_ID.Value);
                            manufacturers.Add(manu);
                            newName = manu.MANUFACTURER_NAME;
                        }
                    }
                    if (before.MANUFACTURER_ID.HasValue)
                    {
                        var manu = manufacturers.FirstOrDefault(o => o.ID == before.MANUFACTURER_ID.Value);
                        if (manu == null)
                        {
                            manu = new HisManufacturerGet().GetById(before.MANUFACTURER_ID.Value);
                            manufacturers.Add(manu);
                            oldName = manu.MANUFACTURER_NAME;
                        }
                    }
                    string ma = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.HangSanXuat);
                    list.Add(String.Format("{0}: {1}=>{2}", ma, oldName, newName));
                }
                if ((item.NATIONAL_NAME ?? "") != (before.NATIONAL_NAME ?? ""))
                {
                    string newName = item.NATIONAL_NAME ?? "";
                    string oldName = before.NATIONAL_NAME ?? "";
                    string national = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.QuocGia);
                    list.Add(String.Format("{0}: {1}=>{2}", national, oldName, newName));
                }
                if ((String.IsNullOrWhiteSpace(item.NOTE) && !String.IsNullOrWhiteSpace(before.NOTE)) ||
                    (!String.IsNullOrWhiteSpace(item.NOTE) && String.IsNullOrWhiteSpace(before.NOTE)) ||
                    (!String.IsNullOrWhiteSpace(item.NOTE) && !String.IsNullOrWhiteSpace(before.NOTE) && item.NOTE.Trim().ToLower() != before.NOTE.Trim().ToLower()))
                {
                    string note = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GhiChu);
                    list.Add(String.Format("{0}: {1}=>{2}", note, before.NOTE, item.NOTE));
                }
                if (list.Count > 0)
                {
                    l = String.Format("{0} ({1})", GetTypeName(item), String.Join(", ", list));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                l = "";
            }
            return l;
        }

        private static string GenerateLogDeleteMaterial(HIS_BID_MATERIAL_TYPE item, List<HIS_SUPPLIER> suppliers)
        {
            string l = "";
            try
            {
                var supp = suppliers.FirstOrDefault(o => o.ID == item.SUPPLIER_ID);
                if (supp == null)
                {
                    supp = new HisSupplierGet().GetById(item.SUPPLIER_ID);
                    suppliers.Add(supp);
                }
                string thau = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NhaThau);
                l = String.Format("{0} ({1}: {2})", GetTypeName(item), thau, supp.SUPPLIER_NAME);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                l = "";
            }
            return l;
        }

        internal static void ProcessLogMedicine(List<HIS_BID_MEDICINE_TYPE> createList, List<HIS_BID_MEDICINE_TYPE> updateList, List<HIS_BID_MEDICINE_TYPE> beforeList, List<HIS_BID_MEDICINE_TYPE> deleteList, ref List<HIS_SUPPLIER> suppliers, ref List<HIS_MANUFACTURER> manufacturers, ref List<string> logs)
        {
            try
            {
                string thuoc = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Thuoc);
                if (!String.IsNullOrWhiteSpace(thuoc)) thuoc = thuoc.ToUpper();
                List<string> cruds = new List<string>();
                if (createList != null && createList.Count > 0)
                {
                    List<string> list = new List<string>();
                    string them = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Them);
                    foreach (var item in createList)
                    {
                        string l = GenerateLogMedicine(suppliers, manufacturers, item);
                        if (!String.IsNullOrWhiteSpace(l)) list.Add(l);
                    }
                    if (list.Count > 0) cruds.Add(String.Format("{0} ({1})", them, String.Join(", ", list)));
                }

                if (updateList != null && updateList.Count > 0)
                {
                    List<string> list = new List<string>();
                    string sua = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Sua);
                    foreach (var item in updateList)
                    {
                        var before = beforeList.FirstOrDefault(o => o.ID == item.ID);
                        string l = GenerateLogUpdateMedicine(suppliers, manufacturers, item, before);
                        if (!String.IsNullOrWhiteSpace(l)) list.Add(l);
                    }
                    if (list.Count > 0) cruds.Add(String.Format("{0} ({1})", sua, String.Join(", ", list)));
                }

                if (deleteList != null && deleteList.Count > 0)
                {
                    List<string> list = new List<string>();
                    string xoa = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Xoa);
                    foreach (var item in deleteList)
                    {
                        string l = GenerateLogDeleteMedicine(item, suppliers);
                        if (!String.IsNullOrWhiteSpace(l)) list.Add(l);
                    }
                    if (list.Count > 0) cruds.Add(String.Format("{0} ({1})", xoa, String.Join(", ", list)));
                }
                if (cruds.Count > 0)
                {
                    string log = String.Format("{0} ({1})", thuoc, String.Join(", ", cruds));
                    logs.Add(log);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private static string GenerateLogMedicine(List<HIS_SUPPLIER> suppliers, List<HIS_MANUFACTURER> manufacturers, HIS_BID_MEDICINE_TYPE item)
        {
            string l = "";
            try
            {
                List<string> list = new List<string>();
                var type = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == item.MEDICINE_TYPE_ID);
                if (type == null)
                {
                    type = new HisMedicineTypeGet().GetById(item.MEDICINE_TYPE_ID);
                }

                list.Add(String.Format("SL: {0}", item.AMOUNT));

                var supp = suppliers.FirstOrDefault(o => o.ID == item.SUPPLIER_ID);
                if (supp == null)
                {
                    supp = new HisSupplierGet().GetById(item.SUPPLIER_ID);
                    suppliers.Add(supp);
                }

                string sup = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NhaThau);
                list.Add(String.Format("{0}: {1}", sup, supp.SUPPLIER_NAME));

                if (item.IMP_PRICE.HasValue)
                {
                    string price = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GiaNhap);
                    list.Add(String.Format("{0} {1}", price, item.IMP_PRICE.Value));
                }
                if (item.IMP_VAT_RATIO.HasValue)
                {
                    string vat = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.VatNhap);
                    list.Add(String.Format("{0} {1}", vat, item.IMP_VAT_RATIO.Value));
                }
                if (item.EXPIRED_DATE.HasValue)
                {
                    string han = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.HanDung);
                    list.Add(String.Format("{0}: {1}", han, Inventec.Common.DateTime.Convert.TimeNumberToDateString(item.EXPIRED_DATE.Value)));
                }
                if (!String.IsNullOrWhiteSpace(item.BID_NUM_ORDER))
                {
                    string stt = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.STTThau);
                    list.Add(String.Format("{0}: {1}", stt, item.BID_NUM_ORDER));
                }
                if (!String.IsNullOrWhiteSpace(item.BID_GROUP_CODE))
                {
                    string group = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NhomThau);
                    list.Add(String.Format("{0}: {1}", group, item.BID_GROUP_CODE));
                }
                if (!String.IsNullOrWhiteSpace(item.BID_PACKAGE_CODE))
                {
                    string pack = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GoiThau);
                    list.Add(String.Format("{0}: {1}", pack, item.BID_PACKAGE_CODE));
                }
                if (!String.IsNullOrWhiteSpace(item.CONCENTRA))
                {
                    string concentra = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.HamLuong);
                    list.Add(String.Format("{0}: {1}", concentra, item.CONCENTRA));
                }
                if (!String.IsNullOrWhiteSpace(item.MEDICINE_REGISTER_NUMBER))
                {
                    string sdk = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoDangKy);
                    list.Add(String.Format("{0}: {1}", sdk, item.MEDICINE_REGISTER_NUMBER));
                }
                if (item.MANUFACTURER_ID.HasValue)
                {
                    var manu = manufacturers.FirstOrDefault(o => o.ID == item.MANUFACTURER_ID.Value);
                    if (manu == null)
                    {
                        manu = new HisManufacturerGet().GetById(item.MANUFACTURER_ID.Value);
                        manufacturers.Add(manu);
                    }
                    string ma = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.HangSanXuat);
                    list.Add(String.Format("{0}: {1}", ma, manu.MANUFACTURER_NAME));
                }
                if (!String.IsNullOrWhiteSpace(item.NATIONAL_NAME))
                {
                    string national = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.QuocGia);
                    list.Add(String.Format("{0}: {1}", national, item.NATIONAL_NAME));
                }
                if (!String.IsNullOrWhiteSpace(item.NOTE))
                {
                    string note = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GhiChu);
                    list.Add(String.Format("{0}: {1}", note, item.NOTE));
                }
                if (list.Count > 0)
                {
                    l = String.Format("{0} ({1})", type.MEDICINE_TYPE_NAME, String.Join(", ", list));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                l = "";
            }
            return l;
        }

        private static string GenerateLogUpdateMedicine(List<HIS_SUPPLIER> suppliers, List<HIS_MANUFACTURER> manufacturers, HIS_BID_MEDICINE_TYPE item, HIS_BID_MEDICINE_TYPE before)
        {
            string l = "";
            try
            {
                List<string> list = new List<string>();
                var type = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == item.MEDICINE_TYPE_ID);
                if (type == null)
                {
                    type = new HisMedicineTypeGet().GetById(item.MEDICINE_TYPE_ID);
                }

                if (item.AMOUNT != before.AMOUNT)
                {
                    list.Add(String.Format("SL: {0}=>{1}", before.AMOUNT, item.AMOUNT));
                }

                if (item.SUPPLIER_ID != before.SUPPLIER_ID)
                {
                    string newName = "";
                    string oldName = "";
                    var newS = suppliers.FirstOrDefault(o => o.ID == item.SUPPLIER_ID);
                    if (newS == null)
                    {
                        newS = new HisSupplierGet().GetById(item.SUPPLIER_ID);
                        suppliers.Add(newS);
                    }
                    newName = newS.SUPPLIER_NAME;
                    var old = suppliers.FirstOrDefault(o => o.ID == before.SUPPLIER_ID);
                    if (newS == null)
                    {
                        old = new HisSupplierGet().GetById(before.SUPPLIER_ID);
                        suppliers.Add(old);
                    }
                    oldName = old.SUPPLIER_NAME;
                    string sup = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NhaThau);
                    list.Add(String.Format("{0}: {1}=>{2}", sup, oldName, newName));
                }

                if ((item.IMP_PRICE ?? -1) != (before.IMP_PRICE ?? -1))
                {
                    string newName = "";
                    string oldName = "";
                    if (item.IMP_PRICE.HasValue)
                    {
                        newName = item.IMP_PRICE.Value.ToString();
                    }
                    if (before.IMP_PRICE.HasValue)
                    {
                        oldName = before.IMP_PRICE.Value.ToString();
                    }
                    string price = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GiaNhap);
                    list.Add(String.Format("{0} {1}=>{2}", price, oldName, newName));
                }
                if ((item.IMP_VAT_RATIO ?? -1) != (before.IMP_VAT_RATIO ?? -1))
                {
                    string newName = "";
                    string oldName = "";
                    if (item.IMP_VAT_RATIO.HasValue)
                    {
                        newName = item.IMP_VAT_RATIO.Value.ToString();
                    }
                    if (before.IMP_VAT_RATIO.HasValue)
                    {
                        oldName = before.IMP_VAT_RATIO.Value.ToString();
                    }
                    string vat = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.VatNhap);
                    list.Add(String.Format("{0} {1}=>{2}", vat, oldName, newName));
                }
                if ((item.EXPIRED_DATE ?? 0) != (before.EXPIRED_DATE ?? 0))
                {
                    string newName = "";
                    string oldName = "";
                    if (item.EXPIRED_DATE.HasValue)
                    {
                        newName = Inventec.Common.DateTime.Convert.TimeNumberToDateString(item.EXPIRED_DATE.Value);
                    }
                    if (before.EXPIRED_DATE.HasValue)
                    {
                        oldName = Inventec.Common.DateTime.Convert.TimeNumberToDateString(before.EXPIRED_DATE.Value);
                    }
                    string han = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.HanDung);
                    list.Add(String.Format("{0}: {1}=>{2}", han, oldName, newName));
                }
                if ((item.BID_NUM_ORDER ?? "") != (before.BID_NUM_ORDER ?? ""))
                {
                    string newName = item.BID_NUM_ORDER ?? "";
                    string oldName = before.BID_NUM_ORDER ?? "";
                    string stt = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.STTThau);
                    list.Add(String.Format("{0}: {1}=>{2}", stt, oldName, newName));
                }
                if ((item.BID_GROUP_CODE ?? "") != (before.BID_GROUP_CODE ?? ""))
                {
                    string newName = item.BID_GROUP_CODE ?? "";
                    string oldName = before.BID_GROUP_CODE ?? "";
                    string group = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NhomThau);
                    list.Add(String.Format("{0}: {1}=>{2}", group, oldName, newName));
                }
                if ((item.BID_PACKAGE_CODE ?? "") != (before.BID_PACKAGE_CODE ?? ""))
                {
                    string newName = item.BID_PACKAGE_CODE ?? "";
                    string oldName = before.BID_PACKAGE_CODE ?? "";
                    string pack = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GoiThau);
                    list.Add(String.Format("{0}: {1}=>{2}", pack, oldName, newName));
                }
                if ((item.CONCENTRA ?? "") != (before.CONCENTRA ?? ""))
                {
                    string newName = item.CONCENTRA ?? "";
                    string oldName = before.CONCENTRA ?? "";
                    string concentra = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.HamLuong);
                    list.Add(String.Format("{0}: {1}=>{2}", concentra, oldName, newName));
                }
                if ((item.MEDICINE_REGISTER_NUMBER ?? "") != (before.MEDICINE_REGISTER_NUMBER ?? ""))
                {
                    string newName = item.MEDICINE_REGISTER_NUMBER ?? "";
                    string oldName = before.MEDICINE_REGISTER_NUMBER ?? "";
                    string sdk = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoDangKy);
                    list.Add(String.Format("{0}: {1}=>{2}", sdk, oldName, newName));
                }
                if ((item.MANUFACTURER_ID ?? 0) != (before.MANUFACTURER_ID ?? 0))
                {
                    string newName = "";
                    string oldName = "";
                    if (item.MANUFACTURER_ID.HasValue)
                    {
                        var manu = manufacturers.FirstOrDefault(o => o.ID == item.MANUFACTURER_ID.Value);
                        if (manu == null)
                        {
                            manu = new HisManufacturerGet().GetById(item.MANUFACTURER_ID.Value);
                            manufacturers.Add(manu);
                            newName = manu.MANUFACTURER_NAME;
                        }
                    }
                    if (before.MANUFACTURER_ID.HasValue)
                    {
                        var manu = manufacturers.FirstOrDefault(o => o.ID == before.MANUFACTURER_ID.Value);
                        if (manu == null)
                        {
                            manu = new HisManufacturerGet().GetById(before.MANUFACTURER_ID.Value);
                            manufacturers.Add(manu);
                            oldName = manu.MANUFACTURER_NAME;
                        }
                    }
                    string ma = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.HangSanXuat);
                    list.Add(String.Format("{0}: {1}=>{2}", ma, oldName, newName));
                }
                if ((item.NATIONAL_NAME ?? "") != (before.NATIONAL_NAME ?? ""))
                {
                    string newName = item.NATIONAL_NAME ?? "";
                    string oldName = before.NATIONAL_NAME ?? "";
                    string national = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.QuocGia);
                    list.Add(String.Format("{0}: {1}=>{2}", national, oldName, newName));
                }
                if ((String.IsNullOrWhiteSpace(item.NOTE) && !String.IsNullOrWhiteSpace(before.NOTE)) ||
                    (!String.IsNullOrWhiteSpace(item.NOTE) && String.IsNullOrWhiteSpace(before.NOTE)) ||
                    (!String.IsNullOrWhiteSpace(item.NOTE) && !String.IsNullOrWhiteSpace(before.NOTE) && item.NOTE.Trim().ToLower() != before.NOTE.Trim().ToLower()))
                {
                    string note = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GhiChu);
                    list.Add(String.Format("{0}: {1}=>{2}", note, before.NOTE, item.NOTE));
                }
                if (list.Count > 0)
                {
                    l = String.Format("{0} ({1})", type.MEDICINE_TYPE_NAME, String.Join(", ", list));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                l = "";
            }
            return l;
        }

        private static string GenerateLogDeleteMedicine(HIS_BID_MEDICINE_TYPE item, List<HIS_SUPPLIER> suppliers)
        {
            string l = "";
            try
            {
                var type = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == item.MEDICINE_TYPE_ID);
                if (type == null)
                {
                    type = new HisMedicineTypeGet().GetById(item.MEDICINE_TYPE_ID);
                }
                var supp = suppliers.FirstOrDefault(o => o.ID == item.SUPPLIER_ID);
                if (supp == null)
                {
                    supp = new HisSupplierGet().GetById(item.SUPPLIER_ID);
                    suppliers.Add(supp);
                }
                string thau = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NhaThau);
                l = String.Format("{0} ({1}: {2})", type.MEDICINE_TYPE_NAME, thau, supp.SUPPLIER_NAME);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                l = "";
            }
            return l;
        }

        internal static void ProcessLogBlood(List<HIS_BID_BLOOD_TYPE> createList, List<HIS_BID_BLOOD_TYPE> updateList, List<HIS_BID_BLOOD_TYPE> beforeList, List<HIS_BID_BLOOD_TYPE> deleteList, ref List<HIS_SUPPLIER> suppliers, ref List<string> logs)
        {
            try
            {
                string thuoc = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Thuoc);
                if (!String.IsNullOrWhiteSpace(thuoc)) thuoc = thuoc.ToUpper();
                List<string> cruds = new List<string>();
                if (createList != null && createList.Count > 0)
                {
                    List<string> list = new List<string>();
                    string them = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Them);
                    foreach (var item in createList)
                    {
                        string l = GenerateLogBlood(suppliers, item);
                        if (!String.IsNullOrWhiteSpace(l)) list.Add(l);
                    }
                    if (list.Count > 0) cruds.Add(String.Format("{0} ({1})", them, String.Join(", ", list)));
                }

                if (updateList != null && updateList.Count > 0)
                {
                    List<string> list = new List<string>();
                    string sua = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Sua);
                    foreach (var item in updateList)
                    {
                        var before = beforeList.FirstOrDefault(o => o.ID == item.ID);
                        string l = GenerateLogUpdateBlood(suppliers, item, before);
                        if (!String.IsNullOrWhiteSpace(l)) list.Add(l);
                    }
                    if (list.Count > 0) cruds.Add(String.Format("{0} ({1})", sua, String.Join(", ", list)));
                }

                if (deleteList != null && deleteList.Count > 0)
                {
                    List<string> list = new List<string>();
                    string xoa = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Xoa);
                    foreach (var item in deleteList)
                    {
                        string l = GenerateLogDeleteBlood(item, suppliers);
                        if (!String.IsNullOrWhiteSpace(l)) list.Add(l);
                    }
                    if (list.Count > 0) cruds.Add(String.Format("{0} ({1})", xoa, String.Join(", ", list)));
                }
                if (cruds.Count > 0)
                {
                    string log = String.Format("{0} ({1})", thuoc, String.Join(", ", cruds));
                    logs.Add(log);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private static string GenerateLogBlood(List<HIS_SUPPLIER> suppliers, HIS_BID_BLOOD_TYPE item)
        {
            string l = "";
            try
            {
                List<string> list = new List<string>();
                var type = HisBloodTypeCFG.DATA.FirstOrDefault(o => o.ID == item.BLOOD_TYPE_ID);
                if (type == null)
                {
                    type = new HisBloodTypeGet().GetById(item.BLOOD_TYPE_ID);
                }

                list.Add(String.Format("SL: {0}", item.AMOUNT));

                var supp = suppliers.FirstOrDefault(o => o.ID == item.SUPPLIER_ID);
                if (supp == null)
                {
                    supp = new HisSupplierGet().GetById(item.SUPPLIER_ID);
                    suppliers.Add(supp);
                }

                string sup = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NhaThau);
                list.Add(String.Format("{0}: {1}", sup, supp.SUPPLIER_NAME));

                if (item.IMP_PRICE.HasValue)
                {
                    string price = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GiaNhap);
                    list.Add(String.Format("{0} {1}", price, item.IMP_PRICE.Value));
                }
                if (item.IMP_VAT_RATIO.HasValue)
                {
                    string vat = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.VatNhap);
                    list.Add(String.Format("{0} {1}", vat, item.IMP_VAT_RATIO.Value));
                }
                if (!String.IsNullOrWhiteSpace(item.BID_NUM_ORDER))
                {
                    string stt = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.STTThau);
                    list.Add(String.Format("{0}: {1}", stt, item.BID_NUM_ORDER));
                }
                if (list.Count > 0)
                {
                    l = String.Format("{0} ({1})", type.BLOOD_TYPE_NAME, String.Join(", ", list));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                l = "";
            }
            return l;
        }

        private static string GenerateLogUpdateBlood(List<HIS_SUPPLIER> suppliers, HIS_BID_BLOOD_TYPE item, HIS_BID_BLOOD_TYPE before)
        {
            string l = "";
            try
            {
                List<string> list = new List<string>();
                var type = HisBloodTypeCFG.DATA.FirstOrDefault(o => o.ID == item.BLOOD_TYPE_ID);
                if (type == null)
                {
                    type = new HisBloodTypeGet().GetById(item.BLOOD_TYPE_ID);
                }

                if (item.AMOUNT != before.AMOUNT)
                {
                    list.Add(String.Format("SL: {0}=>{1}", before.AMOUNT, item.AMOUNT));
                }

                if (item.SUPPLIER_ID != before.SUPPLIER_ID)
                {
                    string newName = "";
                    string oldName = "";
                    var newS = suppliers.FirstOrDefault(o => o.ID == item.SUPPLIER_ID);
                    if (newS == null)
                    {
                        newS = new HisSupplierGet().GetById(item.SUPPLIER_ID);
                        suppliers.Add(newS);
                    }
                    newName = newS.SUPPLIER_NAME;
                    var old = suppliers.FirstOrDefault(o => o.ID == before.SUPPLIER_ID);
                    if (newS == null)
                    {
                        old = new HisSupplierGet().GetById(before.SUPPLIER_ID);
                        suppliers.Add(old);
                    }
                    oldName = old.SUPPLIER_NAME;
                    string sup = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NhaThau);
                    list.Add(String.Format("{0}: {1}=>{2}", sup, oldName, newName));
                }

                if ((item.IMP_PRICE ?? -1) != (before.IMP_PRICE ?? -1))
                {
                    string newName = "";
                    string oldName = "";
                    if (item.IMP_PRICE.HasValue)
                    {
                        newName = item.IMP_PRICE.Value.ToString();
                    }
                    if (before.IMP_PRICE.HasValue)
                    {
                        oldName = before.IMP_PRICE.Value.ToString();
                    }
                    string price = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GiaNhap);
                    list.Add(String.Format("{0} {1}=>{2}", price, oldName, newName));
                }
                if ((item.IMP_VAT_RATIO ?? -1) != (before.IMP_VAT_RATIO ?? -1))
                {
                    string newName = "";
                    string oldName = "";
                    if (item.IMP_VAT_RATIO.HasValue)
                    {
                        newName = item.IMP_VAT_RATIO.Value.ToString();
                    }
                    if (before.IMP_VAT_RATIO.HasValue)
                    {
                        oldName = before.IMP_VAT_RATIO.Value.ToString();
                    }
                    string vat = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.VatNhap);
                    list.Add(String.Format("{0} {1}=>{2}", vat, oldName, newName));
                }

                if ((item.BID_NUM_ORDER ?? "") != (before.BID_NUM_ORDER ?? ""))
                {
                    string newName = item.BID_NUM_ORDER ?? "";
                    string oldName = before.BID_NUM_ORDER ?? "";
                    string stt = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.STTThau);
                    list.Add(String.Format("{0}: {1}=>{2}", stt, oldName, newName));
                }

                if (list.Count > 0)
                {
                    l = String.Format("{0} ({1})", type.BLOOD_TYPE_NAME, String.Join(", ", list));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                l = "";
            }
            return l;
        }

        private static string GenerateLogDeleteBlood(HIS_BID_BLOOD_TYPE item, List<HIS_SUPPLIER> suppliers)
        {
            string l = "";
            try
            {
                var type = HisBloodTypeCFG.DATA.FirstOrDefault(o => o.ID == item.BLOOD_TYPE_ID);
                if (type == null)
                {
                    type = new HisBloodTypeGet().GetById(item.BLOOD_TYPE_ID);
                }
                var supp = suppliers.FirstOrDefault(o => o.ID == item.SUPPLIER_ID);
                if (supp == null)
                {
                    supp = new HisSupplierGet().GetById(item.SUPPLIER_ID);
                    suppliers.Add(supp);
                }
                string nhathau = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NhaThau);
                l = String.Format("{0} ({1}: {2})", type.BLOOD_TYPE_NAME, nhathau, supp.SUPPLIER_NAME);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                l = "";
            }
            return l;
        }

        internal static void GenerateLogBid(HIS_BID newBid, HIS_BID oldBid, ref List<string> logs)
        {
            try
            {
                string info = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ThongTinChung);
                if (!String.IsNullOrWhiteSpace(info)) info = info.ToUpper();
                List<string> list = new List<string>();
                if (newBid.BID_NUMBER != oldBid.BID_NUMBER)
                {
                    string bidNum = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.QuyetDinhThau);
                    list.Add(String.Format("{0}: {1}=>{2}", bidNum, oldBid.BID_NUMBER, newBid.BID_NUMBER));
                }
                if (newBid.BID_NAME != oldBid.BID_NAME)
                {
                    string bidName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenThau);
                    list.Add(String.Format("{0}: {1}=>{2}", bidName, oldBid.BID_NAME, newBid.BID_NAME));
                }
                if ((newBid.BID_YEAR ?? "") != (oldBid.BID_YEAR ?? ""))
                {
                    string bidYear = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NamThau);
                    list.Add(String.Format("{0}: {1}=>{2}", bidYear, oldBid.BID_YEAR ?? "", newBid.BID_YEAR ?? ""));
                }
                if ((newBid.BID_TYPE_ID ?? 0) != (oldBid.BID_TYPE_ID ?? 0))
                {
                    string newName = "";
                    string oldName = "";
                    if (oldBid.BID_TYPE_ID.HasValue)
                    {
                        var old = new HisBidTypeGet().GetById(oldBid.BID_TYPE_ID.Value);
                        oldName = old.BID_TYPE_NAME ?? "";
                    }
                    if (newBid.BID_TYPE_ID.HasValue)
                    {
                        var newT = new HisBidTypeGet().GetById(newBid.BID_TYPE_ID.Value);
                        newName = newT.BID_TYPE_NAME ?? "";
                    }
                    string bidType = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.LoaiThau);
                    list.Add(String.Format("{0}: {1}=>{2}", bidType, oldName, newName));
                }
                if ((newBid.VALID_FROM_TIME ?? 0) != (oldBid.VALID_FROM_TIME ?? 0))
                {
                    string newName = "";
                    string oldName = "";
                    if (oldBid.VALID_FROM_TIME.HasValue)
                    {
                        oldName = Inventec.Common.DateTime.Convert.TimeNumberToDateString(oldBid.VALID_FROM_TIME.Value);
                    }
                    if (newBid.VALID_FROM_TIME.HasValue)
                    {
                        newName = Inventec.Common.DateTime.Convert.TimeNumberToDateString(newBid.VALID_FROM_TIME.Value);
                    }
                    string fromTime = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.HieuLucTu);
                    list.Add(String.Format("{0}: {1}=>{2}", fromTime, oldName, newName));
                }
                if ((newBid.VALID_TO_TIME ?? 0) != (oldBid.VALID_TO_TIME ?? 0))
                {
                    string newName = "";
                    string oldName = "";
                    if (oldBid.VALID_TO_TIME.HasValue)
                    {
                        oldName = Inventec.Common.DateTime.Convert.TimeNumberToDateString(oldBid.VALID_TO_TIME.Value);
                    }
                    if (newBid.VALID_TO_TIME.HasValue)
                    {
                        newName = Inventec.Common.DateTime.Convert.TimeNumberToDateString(newBid.VALID_TO_TIME.Value);
                    }
                    string toTime = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.HieuLucDen);
                    list.Add(String.Format("{0}: {1}=>{2}", toTime, oldName, newName));
                }
                if (list.Count > 0)
                {
                    logs.Add(String.Format("{0} ({1})", info, String.Join(", ", list)));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private static string GetTypeName(HIS_BID_MATERIAL_TYPE item)
        {
            string typeName = "";
            try
            {
                if (item.MATERIAL_TYPE_ID.HasValue)
                {
                    var type = HisMaterialTypeCFG.DATA.FirstOrDefault(o => o.ID == item.MATERIAL_TYPE_ID);
                    if (type == null)
                    {
                        type = new HisMaterialTypeGet().GetById(item.MATERIAL_TYPE_ID.Value);
                    }

                    typeName = type.MATERIAL_TYPE_NAME;
                }
                else if (item.MATERIAL_TYPE_MAP_ID.HasValue)
                {
                    HIS_MATERIAL_TYPE_MAP type = new HisMaterialTypeMapGet().GetById(item.MATERIAL_TYPE_MAP_ID.Value);

                    typeName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.VatTuTuongDuong) + " " + type.MATERIAL_TYPE_MAP_NAME;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return typeName;
        }
    }
}
