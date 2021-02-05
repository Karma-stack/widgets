namespace widgets.Services
{
    public interface IUrlGenerator
    {
        string Encode(int id);
        int Decode(string shortLink);
    }
}
