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

        }



        private void TickElapsed(object sender, System.Timers.ElapsedEventArgs e) {
            main.AddMultipierPointsToCounter();
        }



        public void StartIdleFarm(object sender, EventArgs e) {
            Button currentBtn = (Button)sender;
            currentBtn.Enabled = false;
            aTimer = new System.Timers.Timer(1000);
            aTimer.Elapsed += TickElapsed;
            aTimer.Enabled = true;
        }

    }
}