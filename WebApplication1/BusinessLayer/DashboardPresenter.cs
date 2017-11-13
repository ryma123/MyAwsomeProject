
using System.Collections.Generic;



namespace KPI_Dashboard.BusinessLayer
{

    public class DashboardPresenter
    {

        DashboardDataAccess dashboardDataAccess;
        public DashboardPresenter()
        {
            dashboardDataAccess = new DashboardDataAccess();
        }

        public List<string> GetAllProduct()
        {
            return dashboardDataAccess.UpdateProductComboBox();
        }

        public List<string> GetAllVersion(string SelectedProduct)
        {
            return dashboardDataAccess.UpdateVersionComboBox(SelectedProduct);
        }
        public int GetPercent(string SelectedVersion)
        {
            return dashboardDataAccess.GetPercent(SelectedVersion);
        }

    }
}