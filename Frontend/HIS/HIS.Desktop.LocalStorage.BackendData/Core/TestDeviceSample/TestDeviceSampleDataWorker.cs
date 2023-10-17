using HIS.Desktop.LocalStorage.BackendData.ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.BackendData.Core.TestDeviceSample
{
    public class TestDeviceSampleDataWorker
    {
        private static List<TestDeviceSampleADO> testDeviceSamples { get; set; }

        public static List<TestDeviceSampleADO> TestDeviceSamples
        {
            get
            {
                if (testDeviceSamples == null)
                {
                    testDeviceSamples = new List<TestDeviceSampleADO>();
                }
                lock (testDeviceSamples) ;
                return testDeviceSamples;
            }
            set
            {
                lock (testDeviceSamples) ;
                testDeviceSamples = value;
            }
        }
    }
}
