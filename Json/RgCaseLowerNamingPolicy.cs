namespace System.Text.Json
{
    internal sealed class RgCaseLowerNamingPolicy : JsonSeparatorNamingPolicy
    {
        public RgCaseLowerNamingPolicy() : base(true, '_')
        {
        }
    }
}