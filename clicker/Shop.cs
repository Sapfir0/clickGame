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

        Game game;

        public Shop(Game game) {
            this.game = game;
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

        private int Index(List<Multiplyer> multiplyers, int id) {
            for (int i = 0; i < multiplyers.Count; i++) {
                if (multiplyers[i].ButtonId == id) {
                    return i;
                }
            }
            throw new KeyNotFoundException("Не найдено");
        }


        private Button CreateButton(Context context, int cost, int multiplyer, int costMultiplyer, string buttonText=null) {
            var newBtn = new Button(context);
            newBtn.Text = buttonText + cost.ToString();
            newBtn.Click +=  BuyModifier;
            newBtn.Enabled = false;
            var rand = new Random();
            var barriers = Tuple.Create(1000000000, 2099999999);

            var randomId = rand.Next(barriers.Item1, barriers.Item2);
            while (ContainsId(MultiplyersCosts, randomId)) {
                randomId = rand.Next(barriers.Item1, barriers.Item2);
            }
            newBtn.Id = randomId;
            var Mult = new Multiplyer(newBtn.Id, cost, multiplyer, costMultiplyer);

            MultiplyersCosts.Add(Mult);
            return newBtn;
        }

        public TableRow CreateButtonOnNewRow(Context context, ref TableLayout tableLayout, int cost, int multiplyer, int costMultiplyer, string buttonText=null) {
            var tableRow = new TableRow(context);
            var newBtn = CreateButton(context, cost, multiplyer, costMultiplyer, buttonText);

            tableRow.AddView(newBtn);
            tableLayout.AddView(tableRow);
            return tableRow;
        }

       
        // пока метод не работает
        public void CreateButtonOnExistingRow(Context context, ref TableRow tableRow, ref TableLayout tableLayout, 
                                                int cost, int multiplyer, int costMultiplyer, string buttonText = null) {
            var newBtn = CreateButton(context, cost, multiplyer, costMultiplyer, buttonText);
            tableRow.AddView(newBtn);
            tableLayout.AddView(tableRow);

        }

        private void BuyModifier(object sender, EventArgs e) {
            var currentBtn = (Button)sender;
            var multiplyerCost = FindById(MultiplyersCosts, currentBtn.Id);
            game.DecrementCurrentPoints(multiplyerCost.Cost);
            game.IncrementMultiplier(multiplyerCost.CounterMultiplyer);
            UpdateButtonCost(currentBtn);
        }

        private void UpdateButtonCost(Button button) {
            var currentBtnIndex = Index(MultiplyersCosts, button.Id);
            MultiplyersCosts[currentBtnIndex].Cost *= MultiplyersCosts[currentBtnIndex].CostMultiplyer;

            button.Text = MultiplyersCosts[currentBtnIndex].Cost.ToString();
        }

    }
}