using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExportXmlQD4210.Base
{
    class CustomProvider : IFormatProvider
    {
        private string cultureName;

        public CustomProvider(string cultureName)
        {
            this.cultureName = cultureName;
        }

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(DateTimeFormatInfo))
            {
                Console.Write("(CustomProvider retrieved.) ");
                return new CultureInfo(cultureName).GetFormat(formatType);
            }
            else
            {
                return null;
            }
        }
    }
}
