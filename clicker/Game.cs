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
        private static System.Timers.Timer aTimer;

        MainClass main;

        public Game(MainClass main) {
            this.main = main;
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