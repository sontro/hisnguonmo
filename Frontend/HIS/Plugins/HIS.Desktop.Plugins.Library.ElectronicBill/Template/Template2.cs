using HIS.Desktop.Plugins.Library.ElectronicBill.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.ElectronicBill.Template
{
    public class Template2 : IRunTemplate
    {
        private decimal Amount;

        public Template2(decimal amount)
        {
            this.Amount = amount;
        }

        public object Run()
        {
            List<Data.ProductBase> results = new List<Data.ProductBase>();
            try
            {
                Inventec.Common.Logging.LogSystem.Debug("Tram nay do co ma RegisterNumber:" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => Amount), Amount));
                Data.ProductBase productBase = new Data.ProductBase();
                productBase.Amount = Inventec.Common.Number.Convert.NumberToNumberRoundMax4(Amount);
                productBase.ProdName = "V/P";
                productBase.ProdCode = "VP";
                productBase.ProdUnit = "";
                results.Add(productBase);

                if (results.Count > 0)
                {
                    foreach (var product in results)
                    {
                        if (HisConfigCFG.RoundTransactionAmountOption == "1")
                        {
                            product.Amount = Math.Round(product.Amount, 0, MidpointRounding.AwayFromZero);
                            product.ProdPrice = Math.Round(product.ProdPrice ?? 0, 0, MidpointRounding.AwayFromZero);
                        }
                        else if (HisConfigCFG.RoundTransactionAmountOption == "2")
                        {
                            product.Amount = Math.Round(product.Amount, 0, MidpointRounding.AwayFromZero);
                        }

                        if (HisConfigCFG.IsHidePrice)
                        {
                            product.ProdPrice = null;
                        }

                        if (HisConfigCFG.IsHideQuantity)
                        {
                            product.ProdQuantity = null;
                        }

                        if (HisConfigCFG.IsHideUnitName)
                        {
                            product.ProdUnit = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                results = null;
            }
            return results;
        }
    }
}
