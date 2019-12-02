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
        public List<Multiplyer> MultiplyersCosts = new List<Multiplyer>();

        public class Multiplyer {
            public int ButtonId;
            public int Cost;
            public int CounterMultiplyer;
            public Multiplyer(int buttonId, int cost, int counterMultiplyer) {
                ButtonId = buttonId;
                Cost = cost;
                CounterMultiplyer = counterMultiplyer;
            }
        }

        MainClass main;
        public Shop(MainClass main) {
            this.main = main;
        }

        private bool ContainsId(List<Multiplyer> multiplyers, int id) { // это нужно будет очень редко, но напишу все же
            for (int i = 0; i < multiplyers.Count; i++) {
                if (multiplyers[i].ButtonId == id) {
                    return true;
                }
            }
            return false;
        }

        private Multiplyer FindById(List<Multiplyer> multiplyers, int id) {
            for (int i = 0; i < multiplyers.Count; i++) {
                if (multiplyers[i].ButtonId == id) {
                    return multiplyers[i];
                }
            }
            throw new KeyNotFoundException("Не найдено");
        }


        private Button CreateButton(Context context, int cost, int multiplyer, string buttonText=null) {
            var newBtn = new Button(context);
            newBtn.Text = buttonText + multiplyer.ToString();
            newBtn.Click +=  BuyModifier;

            var rand = new Random();
            var barriers = Tuple.Create(1000000000, 2099999999);

            var randomId = rand.Next(barriers.Item1, barriers.Item2);
            while (ContainsId(MultiplyersCosts, randomId)) {
                randomId = rand.Next(barriers.Item1, barriers.Item2);
            }
            newBtn.Id = randomId;
            var Mult = new Multiplyer(newBtn.Id, cost, multiplyer);

            MultiplyersCosts.Add(Mult);
            return newBtn;
        }

        public TableRow CreateButtonOnNewRow(Context context, ref TableLayout tableLayout, int cost, int multiplyer, string buttonText=null) {
            var tableRow = new TableRow(context);
            var newBtn = CreateButton(context, cost, multiplyer, buttonText);

            tableRow.AddView(newBtn);
            tableLayout.AddView(tableRow);
            return tableRow;
        }

       
        public void CreateButtonOnExistingRow(Context context, ref TableRow tableRow, ref TableLayout tableLayout, int cost, int multiplyer, string buttonText = null) {
            var newBtn = CreateButton(context, cost, multiplyer, buttonText);
            tableRow.AddView(newBtn);
            tableLayout.AddView(tableRow);

        }

        private void BuyModifier(object sender, EventArgs e) {
            var currentBtn = (Button)sender;
            var multiplyerCost = FindById(MultiplyersCosts, currentBtn.Id);
            main.DecrementCurrentPoints(multiplyerCost.Cost);
            MainClass.IncrementMultiplier(multiplyerCost.CounterMultiplyer);
        }

    }
}