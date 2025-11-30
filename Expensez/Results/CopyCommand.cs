using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;

namespace Expensez.Results;

public class CopyCommand : BaseCommand
{

    YearResultsPresentation _results;

    public CopyCommand(YearResultsPresentation results)
    {
        _results = results;
    }

    public override string Header => "Copy";

    public override async void Execute(object? parameter)
    {
        // Build data string
        var sb = new StringBuilder();
        sb.AppendLine("\tJan\tFeb\tMar\tApr\tMaj\tJun\tJul\tAug\tSep\tOkt\tNov\tDec");
        foreach (var item in _results.Categories)
        {
            sb.AppendLine($"{item.Name}\t{item.Jan}\t{item.Feb}\t{item.Mar}\t{item.Apr}\t{item.May}\t{item.Jun}\t{item.Jul}\t{item.Aug}\t{item.Sep}\t{item.Oct}\t{item.Nov}\t{item.Dec}");
        }

        var text = sb.ToString();

        // Copy to clipboard. Use dataobject to force utf-8
        var data = new DataObject();
        data.Set("text/plain;charset=utf-8", text);  
        data.Set(DataFormats.Text, text);            
        await _results.Clipboard.SetDataObjectAsync(data);
    }
}