﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clicker;
using Xamarin.Forms;


namespace clickerGame {
    public partial class MainPage : ContentPage {
        
        private Game _game;

        public MainPage() {
            InitializeComponent();
           
            _game = new Game();
            
            var flexLayout = this.FindByName<FlexLayout>("FlexLayout");
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
                var button = CreateButtonOnNewRow(ref flexLayout);
                button.Clicked += BuyModifier;
                button.Text = Shop.GetTextForMultiplierButton(cost, costMultiplier);
            
                _game.Shop.AddMultiplierCost(Convert.ToInt32(button.AutomationId), new Multiplier(cost, multiplier, costMultiplier));
            }
            
            var startIdleBtn = this.FindByName<Button>("IdleStart");
            startIdleBtn.Clicked += (object obj, EventArgs args) =>
            {
                startIdleBtn.IsEnabled = false;
                _game.StartIdleFarm();
            };
            
            _game.OnChangedPoints += SetScoreOnTextView;
            _game.Shop.OnMultiplierCostChanged += UpdateButtonCost;

        }
        public void SetScoreOnTextView(int points) {
            Device.BeginInvokeOnMainThread(() => {
                string intSequence = points.ToString(CultureInfo.CurrentCulture);
                var countPoints = this.FindByName<Label>("CountPoints");
                countPoints.Text = $"Набрал {intSequence} очков" ;
                Console.WriteLine(points);

                var flexLayout = this.FindByName<FlexLayout>("FlexLayout");
                var length = flexLayout.Children.Count;
               
                for (int i = 1; i < length; i++) { //от 1 т.к. первая кнопка отвечает за идл, и я не ей не выдавал автоматед айди
                    var button = (Button)flexLayout.Children.ElementAt(i);
                    var buttonId = Convert.ToInt32(button.AutomationId);
                    var infoAboutMultipier = _game.Shop.MultipliersCosts[buttonId];
                    button.IsEnabled = points >= infoAboutMultipier.Cost;

                }
            });

        }

        private Button CreateButton() {
            var newBtn = new Button() {
                IsEnabled = false,
                WidthRequest = 75,
                HeightRequest = 75
                
            };
            var rand = new Random();
            var(bottomLine, upperLine) = Tuple.Create(1000000000, 2099999999);

            var randomId = rand.Next(bottomLine, upperLine);
            while (_game.Shop.ContainsId(randomId)) {
                randomId = rand.Next(bottomLine, upperLine);
            }
            
            newBtn.AutomationId = randomId.ToString();
            return newBtn;
        }

        public Button CreateButtonOnNewRow(ref FlexLayout tableLayout) {
            var newBtn = CreateButton();
            tableLayout.Children.Add(newBtn);
            return newBtn;
        }

        public void AddOneToCounterListener(object sender, EventArgs e) {
            _game.AddMultipierPointsToCounter();
        }

        public void UpdateButtonCost(int buttonId, string lining) {
            //var flexLayout = this.FindByName<FlexLayout>("FlexLayout");
            //var btn = flexLayout.FindByName<Button>(buttonId.ToString());

            var btn = FindByAutomatedId(buttonId);
            btn.Text = lining;
        }

        public Button FindByAutomatedId(int id) {
            var flexLayout = this.FindByName<FlexLayout>("FlexLayout");
            var length = flexLayout.Children.Count;
            for (int i = 1; i < length; i++) {
                if (flexLayout.Children.ElementAt(i).AutomationId == id.ToString()) {
                    return (Button)flexLayout.Children.ElementAt(i);
                }
            }
            throw new Exception();
        }

        private void BuyModifier(object sender, EventArgs e) {
            var currentBtn = (Button)sender;
            var modifierId = currentBtn.AutomationId;
            _game.BuyModifier(Convert.ToInt32(modifierId));
        }

    }
}