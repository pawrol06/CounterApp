using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using Microsoft.Maui.Controls;

namespace CounterApp
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<Counter> counters;

        public MainPage()
        {
            InitializeComponent();
            counters = new ObservableCollection<Counter>();
            CountersCollectionView.ItemsSource = counters;
            LoadCounters();
        }

        private async void OnAddCounterClicked(object sender, EventArgs e)
        {
            string initialValueText = await DisplayPromptAsync("Initial Value", "Enter the initial value for the counter:", initialValue: "0", keyboard: Keyboard.Numeric);

            if (!string.IsNullOrEmpty(initialValueText) && int.TryParse(initialValueText, out int initialValue))
            {
                var counterName = $"Counter {counters.Count + 1}";
                var newCounter = new Counter(counterName, initialValue);
                counters.Add(newCounter);
                SaveCounters();
            }
            else
            {
                await DisplayAlert("Invalid Input", "Please enter a valid number.", "OK");
            }
        }

        private void OnIncrementClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var counter = (Counter)button?.BindingContext;
            if (counter != null)
            {
                counter.Increment();
                SaveCounters();
                CountersCollectionView.ItemsSource = null;
                CountersCollectionView.ItemsSource = counters;
            }
        }

        private void OnDecrementClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var counter = (Counter)button?.BindingContext;
            if (counter != null)
            {
                counter.Decrement();
                SaveCounters();
                CountersCollectionView.ItemsSource = null;
                CountersCollectionView.ItemsSource = counters;
            }
        }

        private void SaveCounters()
        {
            var filePath = Path.Combine(FileSystem.AppDataDirectory, "counters.json");
            var json = JsonSerializer.Serialize(counters);
            File.WriteAllText(filePath, json);
        }

        private void LoadCounters()
        {
            var filePath = Path.Combine(FileSystem.AppDataDirectory, "counters.json");
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                var loadedCounters = JsonSerializer.Deserialize<ObservableCollection<Counter>>(json);
                if (loadedCounters != null)
                {
                    foreach (var counter in loadedCounters)
                    {
                        counters.Add(counter);
                    }
                }
            }
        }
    }
}