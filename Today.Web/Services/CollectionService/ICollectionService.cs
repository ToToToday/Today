using System.Collections.Generic;
using Today.Web.DTOModels.CollectionDTO;
using Today.Web.DTOModels.ProductDTO;
using Today.Web.ViewModels;

namespace Today.Web.Services.CollectService
{
    public interface ICollectionService
    {
        void CreateCollect(CollectionVM request);
        void RemoveCollect(CollectionVM request);
        List<ProductDTO.ProductInfo> GetAllCollect(int userid);
    }
}
