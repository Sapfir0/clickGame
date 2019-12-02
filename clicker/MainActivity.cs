using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

        MainClass main;
        Game game;
        Shop shop;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);


            clickBtn = FindViewById<Button>(Resource.Id.clickBtn);
            clickBtn.Click += AddOneToCounterListener;
            countPoints = FindViewById<TextView>(Resource.Id.countPoints);


            main = new MainClass(); 
            game = new Game(main);
            shop = new Shop(main);

            var tableLayout = FindViewById<TableLayout>(Resource.Id.tableLayout);
            shop.CreateButtonOnNewRow(this, ref tableLayout, 10, 1);
            shop.CreateButtonOnNewRow(this, ref tableLayout, 30, 2);



            var startIdleBtn = FindViewById<Button>(Resource.Id.idleStart);
            startIdleBtn.Click += game.StartIdleFarm;

            main.OnChangedPoints += SetTextOnTextView;

        }


        public void SetTextOnTextView(int points) {
            Console.WriteLine(points);
            string intSequence = points.ToString();
            countPoints.Text = intSequence;
            foreach (var multiplyerCost in shop.MultiplyersCosts) { 

                using (var h = new Handler(Looper.MainLooper))
                    h.Post(() => {
                        var openingButton = FindViewById<Button>(multiplyerCost.ButtonId);
                        openingButton.Enabled = points >= multiplyerCost.Cost;
                    });
            }
        }

        public void AddOneToCounterListener(object sender, EventArgs e) {
            main.AddMultipierPointsToCounter();
        }


        protected override void OnDestroy() {
            base.OnDestroy();
            // запомнимать текщуие пойнты и усилители
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
	}
}

