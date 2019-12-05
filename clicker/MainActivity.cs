using System;
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

            _game = Game.GetInstance();
            _shop = new Shop();
            
            
            var tableLayout = FindViewById<TableLayout>(Resource.Id.tableLayout);
            var multipliersList = new List<Tuple<int, int, int>>()  {
                Tuple.Create(5, 1, 2), Tuple.Create(30, 2, 3)
            };

            foreach (var item in multipliersList)  {
                var (cost, multiplier, costMultiplier) = item;
                var button = MultiplierButtonBuilder.CreateButtonOnNewRow(this, ref tableLayout, cost);
                button.Click += BuyModifier;
                _shop.AddMultiplierCost(new Multiplier(button.Id, cost, multiplier, costMultiplier));
            }


            var startIdleBtn = FindViewById<Button>(Resource.Id.idleStart);
            startIdleBtn.Click += _game.StartIdleFarm;

            _game.OnChangedPoints += SetTextOnTextView;
            _shop.OnMultiplierCostChanged += UpdateButtonCost;
        }


        public void SetTextOnTextView(int points) {
            Console.WriteLine(points);
            var intSequence = points.ToString(CultureInfo.CurrentCulture);
            _countPoints.Text = intSequence;
            foreach (var multiplierCost in Shop.MultipliersCosts)  {
                using var h = new Handler(Looper.MainLooper);
                h.Post(() => {
                    var openingButton = FindViewById<Button>(multiplierCost.ButtonId);
                    openingButton.Enabled = points >= multiplierCost.Cost;
                });
            }
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

