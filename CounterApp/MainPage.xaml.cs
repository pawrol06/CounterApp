using Microsoft.Maui.Controls;
using System;

namespace CounterApp;

public partial class MainPage : ContentPage
{
    private int _counterIndex = 1; 

    public MainPage()
    {
        InitializeComponent();
        AddCounter($"Licznik {_counterIndex}"); 
    }

    
    private void AddCounterClicked(object sender, EventArgs e)
    {
        _counterIndex++;
        AddCounter($"Licznik {_counterIndex}");
    }

   
    private void AddCounter(string counterName)
    {
        
        var counterStack = new HorizontalStackLayout
        {
            Spacing = 10
        };

        
        var nameLabel = new Label
        {
            Text = counterName,
            VerticalOptions = LayoutOptions.Center
        };

       
        var valueLabel = new Label
        {
            Text = "0",
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            FontSize = 18,
            WidthRequest = 50
        };

      
        var decrementButton = new Button
        {
            Text = "-",
            WidthRequest = 50
        };
        decrementButton.Clicked += (s, e) =>
        {
            int currentValue = int.Parse(valueLabel.Text);
            valueLabel.Text = (currentValue - 1).ToString();
        };

        
        var incrementButton = new Button
        {
            Text = "+",
            WidthRequest = 50
        };
        incrementButton.Clicked += (s, e) =>
        {
            int currentValue = int.Parse(valueLabel.Text);
            valueLabel.Text = (currentValue + 1).ToString();
        };

        
        counterStack.Children.Add(nameLabel);
        counterStack.Children.Add(decrementButton);
        counterStack.Children.Add(valueLabel);
        counterStack.Children.Add(incrementButton);

        
        CountersContainer.Children.Add(counterStack);
    }
}
