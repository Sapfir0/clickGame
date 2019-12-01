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
        Button clickBtn;
        TextView countPoints;            

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
            TextView cell1 = new TextView(this);
            cell1.Text = "idle farm";
            TableRow tableRow1 = new TableRow(this);
            tableRow1.AddView(cell1);
            tableRow1.Click += BuyItem;

            tableLayout.AddView(tableRow1);


            this.clickBtn = clickBtn;
            this.countPoints = countPoints;
            clickBtn.Click += AddOneToCounterListener;

        }

        private void BuyItem(object sender, EventArgs e) {
            main.AddOnePerSecondTimerEvent();
        }

        public void AddOneToCounterListener(object sender, EventArgs e) {
            int currentPoints = main.AddOneToCounter();
            SetTextOnTextView(currentPoints);
        }

        public void SetTextOnTextView(int counter) {
            countPoints.Text = counter.ToString();
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

