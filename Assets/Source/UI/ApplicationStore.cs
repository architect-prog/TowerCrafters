using Source.Common.Utils;
using Source.UI.Store;

namespace Source.UI
{
    public class ApplicationStore : Singleton<ApplicationStore>
    {
        public SelectedBuildingStore BuildingStore { get; }

        public ApplicationStore()
        {
            BuildingStore = new SelectedBuildingStore();
        }
    }
}