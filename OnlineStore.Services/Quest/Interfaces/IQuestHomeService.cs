using OnlineStore.Models.WebModels.Quest.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore.Services.Quest.Interfaces
{
    public interface IQuestHomeService
    {
        IndexViewModel PrepareIndexModel();

        Task<IEnumerable<ProductConciseViewModel>> GetProductsBySubcategoryAsync(string subcategoryId);

        IEnumerable<ProductConciseViewModel> GetProductsByKeywords(string words);
    }
}
