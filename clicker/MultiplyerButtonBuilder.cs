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
    class MultiplyerButtonBuilder {
        
        
        private static Button CreateButton(Context context, int cost, int multiplyer, int costMultiplyer, string buttonText = null) {
            var newBtn = new Button(context);
            newBtn.Text = buttonText + cost.ToString(CultureInfo.CurrentCulture);
            //newBtn.Click += BuyModifier; //повесит принимающая сторона
            newBtn.Enabled = false;
            var rand = new Random();
            var barriers = Tuple.Create(1000000000, 2099999999);

            var randomId = rand.Next(barriers.Item1, barriers.Item2);
            while (Shop.ContainsId(randomId)) {
                randomId = rand.Next(barriers.Item1, barriers.Item2);
            }
            newBtn.Id = randomId;
            return newBtn;
        }

        public static Button CreateButtonOnNewRow(Context context, ref TableLayout tableLayout, int cost, int multiplyer, int costMultiplyer, string buttonText = null) {
            var tableRow = new TableRow(context);
            var newBtn = CreateButton(context, cost, multiplyer, costMultiplyer, buttonText);
            
            tableRow.AddView(newBtn);
            tableLayout.AddView(tableRow);
            return newBtn;
        }
    }
}