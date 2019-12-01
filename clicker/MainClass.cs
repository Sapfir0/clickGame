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
        Button clickBtn;
        TextView countPoints;
        // разобраться с этим мусором TODO
        public delegate void MethodContainer(int currentPoints);
        public event MethodContainer OnChangedPoints;

        public MainClass(Button clickBtn, TextView countPoints) {

            this.clickBtn = clickBtn;
            this.countPoints = countPoints;
            clickBtn.Click += AddOneToCounterListener;
        }

        public static double IncrementMultiplier(double modifier) {
            multiplier += modifier;
            return multiplier;
        }

        public void DecrementCurrentPoints(int subtrahend) {
            currentPoints -= subtrahend;
            SetTextOnTextView(currentPoints);
            OnChangedPoints(currentPoints);
        }


        public void AddMultipierPointsToCounter() {
            currentPoints += (int)multiplier;
            SetTextOnTextView(currentPoints);
            OnChangedPoints(currentPoints);
        }

        public void AddOneToCounterListener(object sender, EventArgs e) {
            AddMultipierPointsToCounter();
        }

        public void SetTextOnTextView(int counter) {
            Console.WriteLine(counter);
            string intSequence = counter.ToString();
            countPoints.Text = intSequence;
        }


    }


}