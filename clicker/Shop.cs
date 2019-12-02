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
    class Shop {
        public Dictionary<int, int> MultiplyerCosts = new Dictionary<int, int> {

        };

        class multiplyer {

        }

        MainClass main;
        public Shop(MainClass main) {
            this.main = main;
        }

        private Button CreateButton(Context context, string buttonText, int multiplyer) {
            var cell1 = new Button(context);
            cell1.Text = buttonText + multiplyer.ToString();
            cell1.Click +=  BuyModifier;
            return cell1;
        }

        public void CreateButtonOnNewRow(Context context, ref TableLayout tableLayout, int multiplyer, string buttonText =null) {
            var tableRow1 = new TableRow(context);
            var newBtn = CreateButton(context, buttonText, multiplyer);
            var rand = new Random();
            var randomId = rand.Next(1000000000, 2099999999);
            while (MultiplyerCosts.ContainsKey(randomId)) { //надеюсь я правильно написал условие
                randomId = rand.Next(1000000000, 2099999999);
                newBtn.Id = randomId;
            }
            tableRow1.AddView(newBtn);
            tableLayout.AddView(tableRow1);

            MultiplyerCosts.Add(newBtn.Id, multiplyer);
        }


       
        public void CreateButtonOnExistingRow(Context context, ref TableRow tableRow, ref TableLayout tableLayout, int multiplyer, string buttonText = null) {
            var newBtn = CreateButton(context, buttonText, multiplyer);
            tableRow.AddView(newBtn);
            tableLayout.AddView(tableRow);

            MultiplyerCosts.Add(newBtn.Id, multiplyer);
        }

        private void BuyModifier(object sender, EventArgs e) {
            var currentBtn = (Button)sender;
            var multiplyerCost = MultiplyerCosts[currentBtn.Id];
            main.DecrementCurrentPoints(multiplyerCost);
            MainClass.IncrementMultiplier(multiplyerCost);
        }
    }
}