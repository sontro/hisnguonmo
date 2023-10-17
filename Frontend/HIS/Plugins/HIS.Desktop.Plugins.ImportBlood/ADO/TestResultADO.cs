using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImportBlood.ADO
{
    public class TestResultADO
    {
        public long Value { get; set; }
        public string Name { get; set; }

        public static List<TestResultADO> ListTestResult_01
        {
            get
            {
                List<TestResultADO> result = new List<TestResultADO>();
                result.Add(new TestResultADO { Value = 0, Name = "Âm tính" });
                result.Add(new TestResultADO { Value = 1, Name = "Dương tính" });
                return result;
            }
        }

        public static List<TestResultADO> ListTestResult_02
        {
            get
            {
                List<TestResultADO> result = new List<TestResultADO>();
                result.Add(new TestResultADO { Value = 0, Name = "Không phản ứng" });
                result.Add(new TestResultADO { Value = 1, Name = "Có phản ứng" });
                return result;
            }
        }

        public static List<TestResultADO> ListTestResult_03
        {
            get
            {
                List<TestResultADO> result = new List<TestResultADO>();
                result.Add(new TestResultADO { Value = 0, Name = "Không phản ứng" });
                result.Add(new TestResultADO { Value = 1, Name = "Có phản ứng" });
                result.Add(new TestResultADO { Value = 2, Name = "Không xét nghiệm" });
                return result;
            }
        }
    }
}
