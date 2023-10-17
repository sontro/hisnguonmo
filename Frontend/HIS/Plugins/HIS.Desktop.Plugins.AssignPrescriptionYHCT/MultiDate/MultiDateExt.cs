using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignPrescriptionPK.MultiDate
{
    public class MultiDateExt
    {
        string periodsString;     
        PeriodsSet periodsSet;
        public MultiDateExt()
        {         
            periodsSet = new PeriodsSet();
            periodsSet.MergeWith(DateTime.Today, DateTime.Today);          
            periodsString = periodsSet.ToString();
        }     
        public string PeriodsString
        {
            set { periodsString = value; }
            get { return periodsString; }
        }
        public PeriodsSet PeriodsSet
        {
            set { periodsSet = value; }
            get { return periodsSet; }
        }
    }
    public class MultiDateExts : ArrayList
    {
        public new virtual MultiDateExt this[int index] { get { return base[index] as MultiDateExt; } set { base[index] = value; } }
    }
}
