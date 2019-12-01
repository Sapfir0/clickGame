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
        Dictionary<int, int> MultiplyerCosts = new Dictionary<int, int> {
            [Resource.Id.lowMultiplyer] = 10,
            [Resource.Id.mediumMultiplyer] = 30
        };
        MainClass main;
        Game game;

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


            var setX2multiplyerBtn = FindViewById<Button>(Resource.Id.lowMultiplyer);  // TODO вынести это в шоп, сделать его фабрикой
            setX2multiplyerBtn.Enabled = false;
            setX2multiplyerBtn.Text = "L " + MultiplyerCosts[Resource.Id.lowMultiplyer];
            setX2multiplyerBtn.Click += SetLowModifier;

            var setX3multiplyerBtn = FindViewById<Button>(Resource.Id.mediumMultiplyer);
            setX3multiplyerBtn.Enabled = false;
            setX3multiplyerBtn.Text = "M " + MultiplyerCosts[Resource.Id.mediumMultiplyer];
            setX3multiplyerBtn.Click += SetMediumModifier;
            main = new MainClass(); 
            game = new Game(main);

            var startIdleBtn = FindViewById<Button>(Resource.Id.idleStart);
            startIdleBtn.Click += game.StartIdleFarm;

            main.OnChangedPoints += SetTextOnTextView;

        }

        public void SetTextOnTextView(int points) {
            Console.WriteLine(points);
            string intSequence = points.ToString();
            countPoints.Text = intSequence;
            foreach (KeyValuePair<int, int> buttonCost in MultiplyerCosts) { // можно будет пропускать те, которые мы прошли давно, и не обходить каждый раз их
                using (var h = new Handler(Looper.MainLooper))
                    h.Post(() => {
                        var openingButton = FindViewById<Button>(buttonCost.Key);
                        openingButton.Enabled = points >= buttonCost.Value;
                    });
            }
        }

        public void AddOneToCounterListener(object sender, EventArgs e) {
            main.AddMultipierPointsToCounter();
        }

        private void SetMediumModifier(object sender, EventArgs e) {
            main.DecrementCurrentPoints(MultiplyerCosts[Resource.Id.mediumMultiplyer]);

            double modifier = 2;
            MainClass.IncrementMultiplier(modifier);
        }


        private void SetLowModifier(object sender, EventArgs e) {
            main.DecrementCurrentPoints(MultiplyerCosts[Resource.Id.lowMultiplyer]);
            double modifier = 1;
            MainClass.IncrementMultiplier(modifier);
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

