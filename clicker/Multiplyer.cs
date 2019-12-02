using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace clicker {
    public class Multiplyer {
        public int ButtonId;
        public int Cost; // начальная цена
        public int CounterMultiplyer; // на сколько будет увеличивать счетчик после каждогонажатия
        public int CostMultiplyer; // на сколько будет дорожать кнопка после покупки

        public Multiplyer(int buttonId, int cost, int counterMultiplyer, int costMultiplyer) {
            ButtonId = buttonId;
            Cost = cost;
            CounterMultiplyer = counterMultiplyer;
            CostMultiplyer = costMultiplyer;
        }
    }
}