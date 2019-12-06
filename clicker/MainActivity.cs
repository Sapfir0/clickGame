using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using SQLite;
using Environment = System.Environment;

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

            //string completePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "clicker.db");
            //var db = new SQLiteConnection(completePath);
            //var exitDateTime = db.Table<ExitState>().Last().ExitDateTime;
            //var currentDateTime = DateTime.UtcNow;
            //var diff = currentDateTime - exitDateTime;
            //var allSeconds = diff.TotalSeconds;
            //var allPoints = Convert.ToInt32(allSeconds * _game.Multiplier);
            //SetScoreOnTextView(allPoints);


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
                var button = MultiplierButtonBuilder.CreateButtonOnNewRow(this, ref tableLayout);
                button.Click += BuyModifier;
                button.Text = Shop.GetTextForMultiplierButton(cost, costMultiplier);

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

        public void UpdateButtonCost(int buttonId, string lining) {
            var button = FindViewById<Button>(buttonId);
            button.Text = lining;

        }

        private void BuyModifier(object sender, EventArgs e) {
            var currentBtn = (Button)sender;
            var multiplierCost = Shop.FindById(currentBtn.Id); 
            _game.DecrementCurrentPoints(multiplierCost.Cost);
            _game.IncrementMultiplier(multiplierCost.CounterMultiplier);
            _shop.UpdateMultiplierCost(currentBtn.Id);
        }


        protected override void OnStop()
        {
            base.OnStop();
            //string completePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "clicker.db");
            //var db = new SQLiteConnection(completePath);
            //db.CreateTable<ExitState>();

            //var exitDateState = new ExitState {
            //    ExitDateTime = DateTime.UtcNow
            //};
            //db.Insert(exitDateState);
        }

        protected override void OnDestroy() {
            base.OnDestroy();
            // запомнимать текщуие пойнты и усилители
        }

	}
}

