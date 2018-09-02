using Newtonsoft.Json;

namespace WingsOn.API.InfoModels
{
    public class ErrorInfo
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
