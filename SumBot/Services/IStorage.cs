using SumBot.Models;

namespace SumBot.Services
{
    public interface IStorage
    {
        Session GetSession(long chatID);
    }
}
