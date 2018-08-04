using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MuslimSurvivalKit.Behaviors
{
    public class NumericEntryBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += Bindable_TextChanged;
        }

        private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool isValid = int.TryParse(e.NewTextValue, out int newText) && newText >= 10 && newText <= 100;

            var entry = (Entry)sender;
            entry.TextColor = isValid ? Color.Default : Color.Red;
            //entry.Text = isValid ? e.NewTextValue : e.OldTextValue;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= Bindable_TextChanged;
        }
    }
}
