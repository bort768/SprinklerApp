namespace SprinklerApp.Resources.Controls;

public partial class EntryWithValidation : ContentView
{
    public static readonly BindableProperty TextProperty =
                    BindableProperty.Create(nameof(Text), typeof(string), typeof(EntryWithValidation), default(string), BindingMode.TwoWay);

    public static readonly BindableProperty ErrorMessageProperty =
        BindableProperty.Create(nameof(ErrorMessage), typeof(string), typeof(EntryWithValidation), default(string));

    public static readonly BindableProperty IsValidProperty =
        BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(EntryWithValidation), default(bool));

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public string ErrorMessage
    {
        get => (string)GetValue(ErrorMessageProperty);
        set => SetValue(ErrorMessageProperty, value);
    }

    public bool IsValid
    {
        get => (bool)GetValue(IsValidProperty);
        set => SetValue(IsValidProperty, value);
    }

    public EntryWithValidation()
    {
        InitializeComponent();
    }
}