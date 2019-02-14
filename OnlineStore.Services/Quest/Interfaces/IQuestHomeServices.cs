using OnlineStore.Models.WebModels.Quest.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore.Services.Quest.Interfaces
{
    public interface IQuestHomeServices
    {
        IndexViewModel PrepareIndexModel();

        Task<IEnumerable<ProductConciseViewModel>> GetProductsAsync(string subcategoryId);
    }
}
