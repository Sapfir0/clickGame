namespace clicker {
    public class Multiplier {
        public int Cost { get; set; } // начальная цена
        public int CounterMultiplier { get; } // на сколько будет увеличивать счетчик после каждогонажатия
        public int CostMultiplier { get; set; }// на сколько будет дорожать кнопка после покупки

        public Multiplier(int cost, int counterMultiplier, int costMultiplier) {
            Cost = cost;
            CounterMultiplier = counterMultiplier;
            CostMultiplier = costMultiplier;
        }
    }
}