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
        static int counter;
        static int multiplier;
        Button clickBtn;
        TextView countPoints;


        public MainClass(Button clickBtn, TextView countPoints) {
            this.clickBtn = clickBtn;
            this.countPoints = countPoints;
            clickBtn.Click += AddOneToCounter;
        }

        public void AddOneToCounter(object sender, EventArgs e) {
            counter++;
            countPoints.Text = counter.ToString();
        }
    }


}