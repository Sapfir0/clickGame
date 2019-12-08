using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace clicker {


    class Shop {
        public Dictionary<int, Multiplier> MultipliersCosts = new Dictionary<int, Multiplier>();

        public delegate void MethodContainer(int buttonId, string lining);
        public event MethodContainer OnMultiplierCostChanged;
        

        public void AddMultiplierCost(int buttonId, Multiplier mult)  {
            MultipliersCosts.Add(buttonId, mult);
        }

        public bool ContainsId(int id)  {
            Multiplier mult;
            return MultipliersCosts.TryGetValue(id, out mult);
        }

        public Multiplier FindById(int id) {
            foreach (var item in MultipliersCosts) {
                if (item.Key == id) {
                    return item.Value;
                }
            }
            throw new KeyNotFoundException("@string/NotFound");
        }


        public static string GetTextForMultiplierButton(int cost, int multiplier)  {
            return $"{cost}P +{multiplier}/клик";
        }

        public void UpdateMultiplierCost(int buttonId)  {
            var mult = MultipliersCosts[buttonId];

            mult.Cost *= mult.CostMultiplier;
            var buttonText = GetTextForMultiplierButton(mult.Cost, mult.CostMultiplier);
            OnMultiplierCostChanged?.Invoke(buttonId, buttonText);
        }

    }
}