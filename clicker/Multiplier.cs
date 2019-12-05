namespace clicker {
    public class Multiplier {
        public int ButtonId { get; }
        public int Cost { get; set; } // начальная цена
        public int CounterMultiplier { get; } // на сколько будет увеличивать счетчик после каждогонажатия
        public int CostMultiplier { get; set; }// на сколько будет дорожать кнопка после покупки

        public Multiplier(int buttonId, int cost, int counterMultiplier, int costMultiplier) {
            ButtonId = buttonId;
            Cost = cost;
            CounterMultiplier = counterMultiplier;
            CostMultiplier = costMultiplier;
        }
    }
}