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
        int CurrentPoints { get; set; } //общее число очков
        public int Multiplier { get; set; } = 1 ; // насколько мы будем умножать число снизу

        public delegate void MethodContainer(int currentPoints);
        public event MethodContainer OnChangedPoints;

        public Game(int currentPoints, int multiplier)
        {
            CurrentPoints = currentPoints;
            Multiplier = multiplier;
        }

        public Game() {
            
        }

        public double IncrementMultiplier(int modifier) {
            Multiplier += modifier;
            return Multiplier;
        }

        public void DecrementCurrentPoints(int subtrahend) {
            CurrentPoints -= subtrahend;
            OnChangedPoints(CurrentPoints);
        }


        public void AddMultipierPointsToCounter() {
            CurrentPoints += (int)Multiplier;
            OnChangedPoints(CurrentPoints);
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