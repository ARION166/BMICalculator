using Microsoft.Maui.Controls;

namespace CalculatorBMI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
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

                resultLabel.Text = $"Your BMI is {bmi:F2}";

                if (bmi < 18.5)
                {
                    resultLabel.Text += "\nYou are underweight.";
                }
                else if (bmi < 24.9)
                {
                    resultLabel.Text += "\nYou have a normal weight.";
                }
                else if (bmi < 29.9)
                {
                    resultLabel.Text += "\nYou are overweight.";
                }
                else
                {
                    resultLabel.Text += "\nYou are obese.";
                }

                resultLabel.Text += $"\nAge: {age}\nGender: {gender}";
            }
            else
            {
                resultLabel.Text = "Please enter valid numbers and select a gender.";
            }
        }
    }
}
