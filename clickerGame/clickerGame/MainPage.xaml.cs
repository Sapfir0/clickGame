using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clicker;
using Xamarin.Forms;

namespace clickerGame {
    public partial class MainPage : ContentPage {
        
        private Button _clickBtn;
        private Label _countPoints;

        private Game _game;

        public MainPage() {
            InitializeComponent();

            
            _clickBtn = this.FindByName<Button>("clickBtn");
            _clickBtn.Clicked += AddOneToCounterListener;
            _countPoints = this.FindByName<Label>("countPoints");

            _game = new Game();
            
            
            var tableLayout = this.FindByName("tableLayout");
            var multipliersList = new List<Tuple<int, int, int>>()  {
                Tuple.Create(1, 1, 2),
                Tuple.Create(1, 2, 2),
                Tuple.Create(10, 3, 3),
                Tuple.Create(15, 7, 4),
                Tuple.Create(20, 11, 6),
                Tuple.Create(25, 19, 6),
                Tuple.Create(30, 29, 7)
            };

            foreach (var item in multipliersList)  {
                var (cost, multiplier, costMultiplier) = item;
                var button = CreateButtonOnNewRow(ref tableLayout);
                button.Clicked += BuyModifier;
                button.Text = Shop.GetTextForMultiplierButton(cost, costMultiplier);

                _game.Shop.AddMultiplierCost(new Multiplier(button.Id, cost, multiplier, costMultiplier));
            }


            var startIdleBtn = this.FindByName<Button>("idleStart");
            startIdleBtn.Clicked += (object obj, EventArgs args) =>
            {
                startIdleBtn.IsEnabled = false;
                _game.StartIdleFarm();
            };

            _game.OnChangedPoints += SetScoreOnTextView;
            _game.Shop.OnMultiplierCostChanged += UpdateButtonCost;

        }
        public void SetScoreOnTextView(int points) {
            using (var h = new Handler(Looper.MainLooper))
            h.Post(() =>
            {
                string intSequence = points.ToString(CultureInfo.CurrentCulture);
                _countPoints.Text = intSequence;
                Console.WriteLine(points);

                foreach (var multiplierCost in Shop.MultipliersCosts)  {
                    var openingButton = this.FindByName<Button>(multiplierCost.ButtonId.ToString());
                    openingButton.IsEnabled = points >= multiplierCost.Cost;
                }
            });
        }

        private static Button CreateButton() {
            var newBtn = new Button() {
                IsEnabled = false
            };
            var rand = new Random();
            var(bottomLine, upperLine) = Tuple.Create(1000000000, 2099999999);

            var randomId = rand.Next(bottomLine, upperLine);
            while (Shop.ContainsId(randomId)) {
                randomId = rand.Next(bottomLine, upperLine);
            }
            newBtn.Id = randomId;
            return newBtn;
        }

        public static Button CreateButtonOnNewRow(ref TableLayout tableLayout) {
            var tableRow = new TableRow();
            var newBtn = CreateButton();
            
            tableRow.AddView(newBtn);
            tableLayout.AddView(tableRow);
            return newBtn;
        }

        public void AddOneToCounterListener(object sender, EventArgs e) {
            _game.AddMultipierPointsToCounter();
        }

        public void UpdateButtonCost(int buttonId, string lining) {
            var button = this.FindByName<Button>(buttonId.ToString());
            button.Text = lining;

        }

        private void BuyModifier(object sender, EventArgs e) {
            var currentBtn = (Button)sender;
            var modifierId = currentBtn.Id;
            _game.BuyModifier(modifierId);
        }

    }
}