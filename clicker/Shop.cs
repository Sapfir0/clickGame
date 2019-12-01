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
    class Shop {

        Dictionary<int, int> MultiplyerCosts = new Dictionary<int, int> {
            [Resource.Id.lowMultiplyer] = 10,
            [Resource.Id.mediumMultiplyer] = 30
        };

        public Button CreateMuliplyerBtnWith(int multiplyer) {
            var shopMultiplyerBtn = FindViewById<Button>(Resource.Id.mediumMultiplyer);
            shopMultiplyerBtn.Enabled = false;
            shopMultiplyerBtn.Text = "M " + MultiplyerCosts[Resource.Id.mediumMultiplyer];
            shopMultiplyerBtn.Click += SetMediumModifier;
            return shopMultiplyerBtn;

        }

        private void SetLowModifier(object sender, EventArgs e) {
            main.DecrementCurrentPoints(MultiplyerCosts[Resource.Id.lowMultiplyer]);
            double modifier = 2;
            MainClass.IncrementMultiplier(modifier);
        }
    }
}