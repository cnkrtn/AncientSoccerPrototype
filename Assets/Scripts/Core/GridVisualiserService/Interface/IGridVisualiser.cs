using System.Threading.Tasks;

namespace Core.GridVisualiserService.Interface
{
    public interface IGridVisualiserService
    {
        
            Task Inject();
            Task ShowGrid();
            void HideGrid();
    }
}
