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
        private System.Timers.Timer aTimer;
        int currentPoints { get; set; } //общее число очков
        double multiplier = 1; // насколько мы будем умножать число снизу

        public delegate void MethodContainer(int currentPoints);
        public event MethodContainer OnChangedPoints;

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


        public void StartIdleFarm() {
            aTimer = new System.Timers.Timer(1000);
            aTimer.Elapsed += TickElapsed;
            aTimer.Enabled = true;
        }

    }
}