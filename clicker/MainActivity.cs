using System;
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
            var cell1 = new Button(this);
            cell1.Text = "IdleStart";
            cell1.Click += StartIdleFarm;

            var cell2 = new Button(this);
            cell2.Text = "X2";
            cell2.Click += SetX2Modifier;

            TableRow tableRow1 = new TableRow(this);
            tableRow1.AddView(cell1);
            tableRow1.AddView(cell2);
            
            tableLayout.AddView(tableRow1);


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

