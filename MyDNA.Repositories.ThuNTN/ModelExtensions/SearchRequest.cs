namespace MyDNA.Repositories.ThuNTN.ModelExtensions
{
    public class SearchRequest
    {

        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
    public class SearcTestResultThuNTN : SearchRequest
    {
        public string ResultStatus { get; set; }
        public string SampleMethod { get; set; }
        public string FullName { get; set; }

        public SearcTestResultThuNTN()
        {
            ResultStatus = string.Empty;
            SampleMethod = string.Empty;
            FullName = string.Empty;
        }
    }
}
