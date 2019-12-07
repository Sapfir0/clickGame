using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clicker {
    class Game : IDisposable {
        private System.Timers.Timer aTimer;
        int CurrentPoints { get; set; } //общее число очков
        public int Multiplier { get; set; } = 1 ; // насколько мы будем умножать число снизу
        public Shop Shop { get; set; } = new Shop();

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

        public void BuyModifier(int multiplierId)
        {
            var multiplier = Shop.FindById(multiplierId);
            this.DecrementCurrentPoints(multiplier.Cost);
            this.IncrementMultiplier(multiplier.CounterMultiplier);
            Shop.UpdateMultiplierCost(multiplierId);
        }


        private void TickElapsed(object sender, System.Timers.ElapsedEventArgs e) {
            AddMultipierPointsToCounter();
        }


        public void StartIdleFarm() {
            aTimer = new System.Timers.Timer(1000);
            aTimer.Elapsed += TickElapsed;
            aTimer.Enabled = true;
        }

        public void Dispose() {
            aTimer.Close();
        }
    }
}