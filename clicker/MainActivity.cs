﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace clicker
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : HeaderActivity {

        private Button _clickBtn;
        private TextView _countPoints;

        private Game _game;
        private Shop _shop;

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            
            _clickBtn = FindViewById<Button>(Resource.Id.clickBtn);
            _clickBtn.Click += AddOneToCounterListener;
            _countPoints = FindViewById<TextView>(Resource.Id.countPoints);

            _game = new Game();
            _shop = new Shop();
            
            
            var tableLayout = FindViewById<TableLayout>(Resource.Id.tableLayout);
            var multipliersList = new List<Tuple<int, int, int>>()  {
                Tuple.Create(1, 1, 2),
                Tuple.Create(1, 2, 2),
                Tuple.Create(10, 3, 3),
                Tuple.Create(15, 7, 4),
                Tuple.Create(20, 11, 6),
                Tuple.Create(25, 19, 6),
                Tuple.Create(30, 29, 7)
            };

            foreach (var item in multipliersList)  {
                var (cost, multiplier, costMultiplier) = item;
                var button = MultiplierButtonBuilder.CreateButtonOnNewRow(this, ref tableLayout, cost);
                button.Click += BuyModifier;
                _shop.AddMultiplierCost(new Multiplier(button.Id, cost, multiplier, costMultiplier));
            }


            var startIdleBtn = FindViewById<Button>(Resource.Id.idleStart);
            startIdleBtn.Click += (object obj, EventArgs args) =>
            {
                startIdleBtn.Enabled = false;
                _game.StartIdleFarm();
            };

            _game.OnChangedPoints += SetScoreOnTextView;
            _shop.OnMultiplierCostChanged += UpdateButtonCost;
        }


        public void SetScoreOnTextView(int points) {
            using var h = new Handler(Looper.MainLooper);
            h.Post(() => {
                string intSequence = points.ToString(CultureInfo.CurrentCulture);
                _countPoints.Text = intSequence;
                Console.WriteLine(points);

                foreach (var multiplierCost in Shop.MultipliersCosts)  {
                    var openingButton = FindViewById<Button>(multiplierCost.ButtonId);
                    openingButton.Enabled = points >= multiplierCost.Cost;
                }
            });
            
        }


        public void AddOneToCounterListener(object sender, EventArgs e) {
            _game.AddMultipierPointsToCounter();
        }

        public void UpdateButtonCost(int buttonId, int cost) {
            var button = FindViewById<Button>(buttonId);
            button.Text = cost.ToString(CultureInfo.CurrentCulture); // ух лучше пусть предупреждение будет, чем этот аргмент

        }

        private void BuyModifier(object sender, EventArgs e) {
            var currentBtn = (Button)sender;
            var multiplierCost = Shop.FindById(currentBtn.Id); 
            _game.DecrementCurrentPoints(multiplierCost.Cost);
            _game.IncrementMultiplier(multiplierCost.CounterMultiplier);
            _shop.UpdateMultiplierCost(currentBtn.Id);
        }



        protected override void OnDestroy() {
            base.OnDestroy();
            // запомнимать текщуие пойнты и усилители
        }

	}
}

