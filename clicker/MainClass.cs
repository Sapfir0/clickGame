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
    class MainClass {
        static int currentPoints { get; set; } //общее число очков
        static double multiplier = 1; // насколько мы будем умножать число снизу
        static int addPointsForOneIteration = 1;
        Button clickBtn;
        TextView countPoints;
        private static System.Timers.Timer aTimer;
// разобраться с этим мусором TODO

        public MainClass(Button clickBtn, TextView countPoints) {

            this.clickBtn = clickBtn;
            this.countPoints = countPoints;
            clickBtn.Click += AddOneToCounterListener;
        }

        public static double IncrementMultiplier(double modifier) {
            multiplier *= modifier;
            addPointsForOneIteration = addPointsForOneIteration * (int)multiplier;
            return multiplier;
        }


        public static int AddOneToCounter() {
            currentPoints++;
            return currentPoints;
        }

        public static int AddMultipierPointsToCounterWithMultiplier() {
            currentPoints += addPointsForOneIteration;
            return currentPoints;
        }

        public void AddOneToCounterListener(object sender, EventArgs e) {
            currentPoints = AddMultipierPointsToCounterWithMultiplier();
            SetTextOnTextView(currentPoints);
        }

        public void SetTextOnTextView(int counter) {
            Console.WriteLine(counter);
            string intSequence = counter.ToString();
            countPoints.Text = intSequence;
        }

        public void SetTimer() {
            aTimer = new System.Timers.Timer(1000);
            aTimer.Elapsed += ATimer_Elapsed;
            aTimer.Enabled = true;
        }

        private void ATimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
            currentPoints = AddMultipierPointsToCounterWithMultiplier();
            SetTextOnTextView(currentPoints);

        }
    }


}