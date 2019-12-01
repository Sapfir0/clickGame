using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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


        public MainClass() {

        }


        public int AddOneToCounter() {
            counter++;
            return counter;
        }

        public void AddOnePerSecond(object obj) {
            int stopTime = (int)obj;
            for (int i = 0; i < 20; i++) {
                AddOneToCounter();

            }

        }

        public void AddOnePerSecondTimerEvent() {
            int num = 0;
            int launchDelay = 0; //ms
            int triggerTime = 1000; // ms

            TimerCallback tm = new TimerCallback(AddOnePerSecond);
            // создаем таймер
            Timer timer = new Timer(tm, num, launchDelay, triggerTime);

        }
    }


}