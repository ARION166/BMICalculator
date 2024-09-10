using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System.Collections.Generic;
using System.Linq;

namespace CalculatorBMI
{
    public partial class MainPage : ContentPage
    {
        private const string HistoryKey = "BMIHistory";
        private List<string> bmiHistory = new List<string>();

        public MainPage()
        {
            InitializeComponent();
            LoadHistory();
            historyListView.ItemsSource = bmiHistory;
        }

        private void OnCalculateBMI(object sender, EventArgs e)
        {
            double weight;
            double height;
            int age;
            string gender = genderPicker.SelectedItem?.ToString();

            if (double.TryParse(weightEntry.Text, out weight) &&
                double.TryParse(heightEntry.Text, out height) &&
                int.TryParse(ageEntry.Text, out age) &&
                !string.IsNullOrWhiteSpace(gender))
            {
                height /= 100;
                double bmi = weight / (height * height);

                string result = $"Your BMI is {bmi:F2}";

                if (bmi < 18.5)
                {
                    result += "\nYou are underweight.";
                }
                else if (bmi < 24.9)
                {
                    result += "\nYou have a normal weight.";
                }
                else if (bmi < 29.9)
                {
                    result += "\nYou are overweight.";
                }
                else
                {
                    result += "\nYou are obese.";
                }

                result += $"\nAge: {age}\nGender: {gender}";
                resultLabel.Text = result;

                bmiHistory.Add(result);
                historyListView.ItemsSource = null;
                historyListView.ItemsSource = bmiHistory;

                SaveHistory();
            }
            else
            {
                resultLabel.Text = "Please enter valid numbers and select a gender.";
            }
        }

        private void SaveHistory()
        {
            var historyString = string.Join("||", bmiHistory);
            Preferences.Set(HistoryKey, historyString);
        }

        private void LoadHistory()
        {
            var historyString = Preferences.Get(HistoryKey, string.Empty);
            if (!string.IsNullOrEmpty(historyString))
            {
                bmiHistory = historyString.Split(new[] { "||" }, StringSplitOptions.None).ToList();
            }
        }

        private void OnShowBMITable(object sender, EventArgs e)
        {
            bmiTableOverlay.IsVisible = !bmiTableOverlay.IsVisible;
        }

        private void OnCloseImage(object sender, EventArgs e)
        {
            bmiTableOverlay.IsVisible = false;
        }
    }
}
