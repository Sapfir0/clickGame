using System;
using System.Collections.Generic;
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
    public class MainActivity : AppCompatActivity
    {
        MainClass main;
        Shop shop;
        Dictionary<int, int> MultiplyerCosts = new Dictionary<int, int> {
            [Resource.Id.setX2multiplyer] = 30,
            [Resource.Id.setX3multiplyer] = 100
        };

    protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);


            var clickBtn = FindViewById<Button>(Resource.Id.clickBtn);
            var countPoints = FindViewById<TextView>(Resource.Id.countPoints);


            main = new MainClass(clickBtn, countPoints);
            shop = new Shop();


            var tableLayout = FindViewById<TableLayout>(Resource.Id.tableLayout);
            var startIdleBtn = FindViewById<Button>(Resource.Id.idleStart);
            startIdleBtn.Click += StartIdleFarm;

            var setX2multiplyerBtn = FindViewById<Button>(Resource.Id.setX2multiplyer);
            setX2multiplyerBtn.Enabled = false;
            setX2multiplyerBtn.Click += SetX2Modifier;

            var setX3multiplyerBtn = FindViewById<Button>(Resource.Id.setX3multiplyer);
            setX3multiplyerBtn.Enabled = false;
            setX3multiplyerBtn.Click += SetX3Modifier;

            var currentPointsView = FindViewById<TextView>(Resource.Id.countPoints);
            currentPointsView.AfterTextChanged += CurrentPointsChanged;
        }

        private void CurrentPointsChanged(object sender, Android.Text.AfterTextChangedEventArgs e) {
            TextView pointsView = (TextView)sender;
            string pointsS = pointsView.Text.Clone().ToString();
            int points = Convert.ToInt32(pointsS);
            foreach (KeyValuePair<int, int> buttonCost in MultiplyerCosts) { // можно будет пропускать те, которые мы прошли давно, и не обходить каждый раз их
                if (points > buttonCost.Value) {
                    using (var h = new Handler(Looper.MainLooper))
                        h.Post(() => {
                            var openingButton = FindViewById<Button>(buttonCost.Key);
                            openingButton.Enabled = true;
                        });

                }
            }

        }

        private void SetX3Modifier(object sender, EventArgs e) {
            Button currentBtn = (Button)sender;
            currentBtn.Enabled = false;

            double modifier = 3.0;
            MainClass.IncrementMultiplier(modifier);
        }


        private void SetX2Modifier(object sender, EventArgs e) {
            Button currentBtn = (Button)sender;
            currentBtn.Enabled = false;
            
            double modifier = 2.0;
            MainClass.IncrementMultiplier(modifier);
        }

        private void StartIdleFarm(object sender, EventArgs e) {
            Button currentBtn = (Button)sender;
            currentBtn.Enabled = false;
            main.SetTimer(); //стартовать только один раз
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

