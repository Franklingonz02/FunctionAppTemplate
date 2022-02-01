using System.Threading.Tasks;

namespace FunctionAppTemplate.Interfaces
{
    public interface IGraphRepository
    {
        Task<bool> ExistsUserWithEmailAsync(string email);
    }
}