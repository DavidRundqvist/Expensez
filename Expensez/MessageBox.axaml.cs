using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Expensez;

public partial class MessageBox : Window
{
    public MessageBox()
    {
        InitializeComponent();
    }

    private void OnOK(object sender, RoutedEventArgs e)
    {
        this.Close(true);
    }

    private void OnCancel(object sender, RoutedEventArgs e)
    {
        this.Close(false);
    }

    public string Message
    {
        set
        {
            _messageTB.Text = value;
        }
    }

    public static async Task<bool> ShowDialog(Window owner, string title, string message)
    {
        var w = new MessageBox();
        w.Title = title;
        w.Message = message;
        var r = await w.ShowDialog<bool>(owner);
        return r == true;

    }
}

