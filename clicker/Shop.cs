using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;


namespace clicker {
    class Shop {
        public static List<Multiplier> MultipliersCosts = new List<Multiplier>();

        public delegate void MethodContainer(int buttonId, int cost);
        public event MethodContainer OnMultiplierCostChanged;
        

        public void AddMultiplierCost(Multiplier mult)  {
            MultipliersCosts.Add(mult);
        }

        public static bool ContainsId(int id)  {
            return MultipliersCosts.Any(t => t.ButtonId == id);
        }

        public static Multiplier FindById(int id) {
            for (int i = 0; i < MultipliersCosts.Count; i++) {
                if (MultipliersCosts[i].ButtonId == id) {
                    return MultipliersCosts[i];
                }
            }
            throw new KeyNotFoundException("@string/NotFound");
        }


        public static int Index(int id) {
            for (int i = 0; i < MultipliersCosts.Count; i++) {
                if (MultipliersCosts[i].ButtonId == id) {
                    return i;
                }
            }
            throw new KeyNotFoundException("@string/NotFound");
        }

        public void UpdateMultiplierCost(int buttonId)  {
            var index = Index(buttonId);
            MultipliersCosts[index].Cost *= MultipliersCosts[index].CostMultiplier;
            OnMultiplierCostChanged?.Invoke(buttonId, MultipliersCosts[index].Cost);
        }

    }
}