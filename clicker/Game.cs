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
    class Game {

        Dictionary<int, int> MultiplyerCosts = new Dictionary<int, int> {
            [Resource.Id.lowMultiplyer] = 10,
            [Resource.Id.mediumMultiplyer] = 30
        };
        private static System.Timers.Timer aTimer;

        MainClass main;
        Shop shop;

        public Game(Button clickBtn, TextView countPoints) {
            main = new MainClass(clickBtn, countPoints);

            var setX2multiplyerBtn = FindViewById<Button>(Resource.Id.lowMultiplyer);  // TODO вынести это в шоп, сделать его фабрикой
            setX2multiplyerBtn.Enabled = false;
            setX2multiplyerBtn.Text = "L " + MultiplyerCosts[Resource.Id.lowMultiplyer];
            setX2multiplyerBtn.Click += SetLowModifier;

            var setX3multiplyerBtn = FindViewById<Button>(Resource.Id.mediumMultiplyer);
            setX3multiplyerBtn.Enabled = false;
            setX3multiplyerBtn.Text = "M " + MultiplyerCosts[Resource.Id.mediumMultiplyer];
            setX3multiplyerBtn.Click += SetMediumModifier;

        }

        private void CurrentPointsChanged(object sender, Android.Text.AfterTextChangedEventArgs e) {
            TextView pointsView = (TextView)sender;
            int points = Convert.ToInt32(pointsView.Text);

            foreach (KeyValuePair<int, int> buttonCost in MultiplyerCosts) { // можно будет пропускать те, которые мы прошли давно, и не обходить каждый раз их
                using (var h = new Handler(Looper.MainLooper))
                    h.Post(() => {
                        var openingButton = FindViewById<Button>(buttonCost.Key);
                        openingButton.Enabled = points >= buttonCost.Value;
                    });
            }
        }

        public void GameLoop() {
            aTimer = new System.Timers.Timer(1000);
            aTimer.Elapsed += TickElapsed;
            aTimer.Enabled = true;
        }

        private void TickElapsed(object sender, System.Timers.ElapsedEventArgs e) {
            main.AddMultipierPointsToCounter();
        }


        private void SetMediumModifier(object sender, EventArgs e) {
            main.DecrementCurrentPoints(MultiplyerCosts[Resource.Id.mediumMultiplyer]);

            double modifier = 3;
            MainClass.IncrementMultiplier(modifier);
        }


        private void SetLowModifier(object sender, EventArgs e) {
            main.DecrementCurrentPoints(MultiplyerCosts[Resource.Id.lowMultiplyer]);
            double modifier = 2;
            MainClass.IncrementMultiplier(modifier);
        }

        public void StartIdleFarm(object sender, EventArgs e) {
            Button currentBtn = (Button)sender;
            currentBtn.Enabled = false;
            GameLoop(); //стартовать только один раз
        }

    }
}