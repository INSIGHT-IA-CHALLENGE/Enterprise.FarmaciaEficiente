namespace Fiap.Web.FarmaciaEficiente.Models
{
    public class Token
    {
        public string TokenValue { get; }
        public string Type { get; }
        public string Prefix { get; }

        public Token(string token, string type, string prefix)
        {
            TokenValue = token;
            Type = type;
            Prefix = prefix;
        }
    }
}
