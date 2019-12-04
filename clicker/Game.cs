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
        static int currentPoints { get; set; } //общее число очков
        static double multiplier = 1; // насколько мы будем умножать число снизу

        public delegate void MethodContainer(int currentPoints);
        public event MethodContainer OnChangedPoints;

        private static Game instanse;
        private static object syncRoot = new Object();

        public static Game GetInstanse() {
            if (instanse == null) {
                lock (syncRoot) {
                    if (instanse == null) {
                        instanse = new Game();
                    }
                }
            }
            return instanse;
        }

        public double IncrementMultiplier(double modifier) {
            multiplier += modifier;
            return multiplier;
        }

        public void DecrementCurrentPoints(int subtrahend) {
            currentPoints -= subtrahend;
            OnChangedPoints(currentPoints);
        }


        public void AddMultipierPointsToCounter() {
            currentPoints += (int)multiplier;
            OnChangedPoints(currentPoints);
        }


        private void TickElapsed(object sender, System.Timers.ElapsedEventArgs e) {
            AddMultipierPointsToCounter();
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