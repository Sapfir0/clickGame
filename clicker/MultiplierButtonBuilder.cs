﻿using System;
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
    class MultiplierButtonBuilder {
        
        
        private static Button CreateButton(Context context, int cost, int multiplier) {
            var newBtn = new Button(context) {
                Text = cost.ToString(CultureInfo.CurrentCulture) + "P " + multiplier.ToString(CultureInfo.CurrentCulture),
                Enabled = false
            };
            var rand = new Random();
            var(bottomLine, upperLine) = Tuple.Create(1000000000, 2099999999);

            var randomId = rand.Next(bottomLine, upperLine);
            while (Shop.ContainsId(randomId)) {
                randomId = rand.Next(bottomLine, upperLine);
            }
            newBtn.Id = randomId;
            return newBtn;
        }

        public static Button CreateButtonOnNewRow(Context context, ref TableLayout tableLayout, int cost, int multiplier) {
            var tableRow = new TableRow(context);
            var newBtn = CreateButton(context, cost, multiplier);
            
            tableRow.AddView(newBtn);
            tableLayout.AddView(tableRow);
            return newBtn;
        }
    }
}