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
    public class MainActivity : AppCompatActivity {

        Button clickBtn;
        TextView countPoints;

        Game game;
        Shop shop;

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            clickBtn = FindViewById<Button>(Resource.Id.clickBtn);
            clickBtn.Click += AddOneToCounterListener;
            countPoints = FindViewById<TextView>(Resource.Id.countPoints);

            game = Game.GetInstanse();
            shop = new Shop(game);
            
            
            var tableLayout = FindViewById<TableLayout>(Resource.Id.tableLayout);
            var multiplyersList = new List<Tuple<int, int, int>>()  {
                Tuple.Create(10, 1, 2), Tuple.Create(30, 2, 3)
            };

            foreach (var item in multiplyersList)  {
                var (cost, multiplyer, costMultiplyer) = item;
                var button = MultiplyerButtonBuilder.CreateButtonOnNewRow(this, ref tableLayout, cost, multiplyer, costMultiplyer);
                button.Click += BuyModifier;
                var mult = new Multiplyer(button.Id, cost, multiplyer, costMultiplyer);
                shop.AddMultiplyerCost(mult);
            }


            var startIdleBtn = FindViewById<Button>(Resource.Id.idleStart);
            startIdleBtn.Click += game.StartIdleFarm;

            game.OnChangedPoints += SetTextOnTextView;

            shop.OnMultiplyerCostChanged += UpdateButtonCost;
        }


        public void SetTextOnTextView(int points) {
            Console.WriteLine(points);
            string intSequence = points.ToString();
            countPoints.Text = intSequence;

        }


        public void AddOneToCounterListener(object sender, EventArgs e) {
            game.AddMultipierPointsToCounter();
        }

        public void UpdateButtonCost(int buttonId, int cost) {
            var button = FindViewById<Button>(buttonId);
            button.Text = cost.ToString(CultureInfo.CurrentCulture); // ух лучше пусть предупреждение будет, чем этот аргмент

        }

        private void BuyModifier(object sender, EventArgs e) {
            var currentBtn = (Button)sender;
            var multiplyerCost = Shop.FindById(currentBtn.Id); 
            game.DecrementCurrentPoints(multiplyerCost.Cost);
            game.IncrementMultiplier(multiplyerCost.CounterMultiplyer);
        }



        protected override void OnDestroy() {
            base.OnDestroy();
            // запомнимать текщуие пойнты и усилители
        }

	}
}

