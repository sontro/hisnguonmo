﻿using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OracleUDT
{
    public class THisMestPeriodMate : INullable, IOracleCustomType
    {
        private bool objectIsNull;

        [OracleObjectMappingAttribute("ID")]
        public long ID { get; set; }
        [OracleObjectMappingAttribute("CREATE_TIME")]
        public long? CREATE_TIME { get; set; }
        [OracleObjectMappingAttribute("MODIFY_TIME")]
        public long? MODIFY_TIME { get; set; }
        [OracleObjectMappingAttribute("CREATOR")]
        public string CREATOR { get; set; }
        [OracleObjectMappingAttribute("MODIFIER")]
        public string MODIFIER { get; set; }
        [OracleObjectMappingAttribute("APP_CREATOR")]
        public string APP_CREATOR { get; set; }
        [OracleObjectMappingAttribute("APP_MODIFIER")]
        public string APP_MODIFIER { get; set; }
        [OracleObjectMappingAttribute("IS_ACTIVE")]
        public short? IS_ACTIVE { get; set; }
        [OracleObjectMappingAttribute("IS_DELETE")]
        public short? IS_DELETE { get; set; }
        [OracleObjectMappingAttribute("GROUP_CODE")]
        public string GROUP_CODE { get; set; }
        [OracleObjectMappingAttribute("MEDI_STOCK_PERIOD_ID")]
        public long MEDI_STOCK_PERIOD_ID { get; set; }
        [OracleObjectMappingAttribute("BEGIN_AMOUNT")]
        public decimal BEGIN_AMOUNT { get; set; }
        [OracleObjectMappingAttribute("IN_AMOUNT")]
        public decimal IN_AMOUNT { get; set; }
        [OracleObjectMappingAttribute("OUT_AMOUNT")]
        public decimal OUT_AMOUNT { get; set; }
        [OracleObjectMappingAttribute("INVENTORY_AMOUNT")]
        public decimal? INVENTORY_AMOUNT { get; set; }
        [OracleObjectMappingAttribute("MATERIAL_ID")]
        public long MATERIAL_ID { get; set; }
        [OracleObjectMappingAttribute("AMOUNT")]
        public decimal AMOUNT { get; set; }
        [OracleObjectMappingAttribute("TDL_MATERIAL_TYPE_ID")]
        public long? TDL_MATERIAL_TYPE_ID { get; set; }
        [OracleObjectMappingAttribute("MEDI_STOCK_ID")]
        public long? MEDI_STOCK_ID { get; set; }

        public static THisMestPeriodMate Null
        {
            get
            {
                THisMestPeriodMate company = new THisMestPeriodMate();
                company.objectIsNull = true;
                return company;
            }
        }

        public bool IsNull
        {
            get { return objectIsNull; }
        }

        public void FromCustomObject(Oracle.DataAccess.Client.OracleConnection con, IntPtr pUdt)
        {
            OracleUdt.SetValue(con, pUdt, "ID", ID);
            OracleUdt.SetValue(con, pUdt, "CREATE_TIME", CREATE_TIME);
            OracleUdt.SetValue(con, pUdt, "MODIFY_TIME", MODIFY_TIME);
            OracleUdt.SetValue(con, pUdt, "CREATOR", CREATOR);
            OracleUdt.SetValue(con, pUdt, "MODIFIER", MODIFIER);
            OracleUdt.SetValue(con, pUdt, "APP_CREATOR", APP_CREATOR);
            OracleUdt.SetValue(con, pUdt, "APP_MODIFIER", APP_MODIFIER);
            OracleUdt.SetValue(con, pUdt, "IS_ACTIVE", IS_ACTIVE);
            OracleUdt.SetValue(con, pUdt, "IS_DELETE", IS_DELETE);
            OracleUdt.SetValue(con, pUdt, "GROUP_CODE", GROUP_CODE);
            OracleUdt.SetValue(con, pUdt, "MEDI_STOCK_PERIOD_ID", MEDI_STOCK_PERIOD_ID);
            OracleUdt.SetValue(con, pUdt, "BEGIN_AMOUNT", BEGIN_AMOUNT);
            OracleUdt.SetValue(con, pUdt, "IN_AMOUNT", IN_AMOUNT);
            OracleUdt.SetValue(con, pUdt, "OUT_AMOUNT", OUT_AMOUNT);
            OracleUdt.SetValue(con, pUdt, "INVENTORY_AMOUNT", INVENTORY_AMOUNT);
            OracleUdt.SetValue(con, pUdt, "MATERIAL_ID", MATERIAL_ID);
            OracleUdt.SetValue(con, pUdt, "AMOUNT", AMOUNT);
            OracleUdt.SetValue(con, pUdt, "TDL_MATERIAL_TYPE_ID", TDL_MATERIAL_TYPE_ID);
            OracleUdt.SetValue(con, pUdt, "MEDI_STOCK_ID", MEDI_STOCK_ID);
        }

        public void ToCustomObject(Oracle.DataAccess.Client.OracleConnection con, IntPtr pUdt)
        {
            ID = (long)OracleUdt.GetValue(con, pUdt, "ID");
            CREATE_TIME = (long?)OracleUdt.GetValue(con, pUdt, "CREATE_TIME");
            MODIFY_TIME = (long?)OracleUdt.GetValue(con, pUdt, "MODIFY_TIME");
            CREATOR = (string)OracleUdt.GetValue(con, pUdt, "CREATOR");
            MODIFIER = (string)OracleUdt.GetValue(con, pUdt, "MODIFIER");
            APP_CREATOR = (string)OracleUdt.GetValue(con, pUdt, "APP_CREATOR");
            APP_MODIFIER = (string)OracleUdt.GetValue(con, pUdt, "APP_MODIFIER");
            IS_ACTIVE = (short?)OracleUdt.GetValue(con, pUdt, "IS_ACTIVE");
            IS_DELETE = (short?)OracleUdt.GetValue(con, pUdt, "IS_DELETE");
            GROUP_CODE = (string)OracleUdt.GetValue(con, pUdt, "GROUP_CODE");
            MEDI_STOCK_PERIOD_ID = (long)OracleUdt.GetValue(con, pUdt, "MEDI_STOCK_PERIOD_ID");
            BEGIN_AMOUNT = (decimal)OracleUdt.GetValue(con, pUdt, "BEGIN_AMOUNT");
            IN_AMOUNT = (decimal)OracleUdt.GetValue(con, pUdt, "IN_AMOUNT");
            OUT_AMOUNT = (decimal)OracleUdt.GetValue(con, pUdt, "OUT_AMOUNT");
            INVENTORY_AMOUNT = (decimal?)OracleUdt.GetValue(con, pUdt, "INVENTORY_AMOUNT");
            MATERIAL_ID = (long)OracleUdt.GetValue(con, pUdt, "MATERIAL_ID");
            AMOUNT = (decimal)OracleUdt.GetValue(con, pUdt, "AMOUNT");
            TDL_MATERIAL_TYPE_ID = (long?)OracleUdt.GetValue(con, pUdt, "TDL_MATERIAL_TYPE_ID");
            MEDI_STOCK_ID = (long?)OracleUdt.GetValue(con, pUdt, "MEDI_STOCK_ID");
        }
    }
}
