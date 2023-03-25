namespace cotacao_api.General
{
    public class GeneralResult
    {
        public bool failure { get; set; }

        public object data { get; set; }

        public List<string> errors { get; set; }

        public string date { get; set; }

        public string message { get; set; }

        public GeneralResult()
        {
            failure = false;
            errors = new List<string>();
            date = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        }

        public void AddError(string ex)
        {
            failure = true;
            data = new { };
            errors.Add(ex);
        }

        public void AddMessage(string _message)
        {
            message = _message;
        }
    }
}
