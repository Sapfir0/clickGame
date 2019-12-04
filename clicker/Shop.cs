using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace clicker {
    class Shop {
        private static List<Multiplyer> MultiplyersCosts = new List<Multiplyer>();

        Game game;
        
        public delegate void MethodContainer(int buttonId, int cost);
        public event MethodContainer OnMultiplyerCostChanged;
        public Shop(Game game) {
            this.game = game;
        }

        public void AddMultiplyerCost(Multiplyer mult)  {
            MultiplyersCosts.Add(mult);
        }

        public static bool ContainsId(int id) { // это нужно будет очень редко, но напишу все же
            for (int i = 0; i < MultiplyersCosts.Count; i++) {
                if (MultiplyersCosts[i].ButtonId == id) {
                    return true;
                }
            }
            return false;
        }

        public static Multiplyer FindById(int id) {
            for (int i = 0; i < MultiplyersCosts.Count; i++) {
                if (MultiplyersCosts[i].ButtonId == id) {
                    return MultiplyersCosts[i];
                }
            }
            throw new KeyNotFoundException("Не найдено");
        }

        public static int Index(int id) {
            for (int i = 0; i < MultiplyersCosts.Count; i++) {
                if (MultiplyersCosts[i].ButtonId == id) {
                    return i;
                }
            }
            throw new KeyNotFoundException("Не найдено");
        }

       

        private void UpdateMultiplyerCost(int buttonId) {
            MultiplyersCosts[buttonId].Cost *= MultiplyersCosts[buttonId].CostMultiplyer;
            OnMultiplyerCostChanged(buttonId, MultiplyersCosts[buttonId].Cost);
        }

    }
}