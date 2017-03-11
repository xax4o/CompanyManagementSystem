namespace CompanyManagementSystem.Web.ViewModels.Common
{
    public class BasePagedViewModel
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }
    }
}