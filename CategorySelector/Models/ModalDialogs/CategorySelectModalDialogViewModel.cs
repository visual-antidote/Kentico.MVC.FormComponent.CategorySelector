using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualAntidote.Kentico.MVC.FormComponent.CategorySelector.Models.ModalDialogs
{
    public class CategorySelectItemViewModel
    {
        public CategorySelectItemViewModel()
        {
            Categories = new List<CategorySelectItemViewModel>();
        }

        public int ID { get; set; }
        public string CodeName { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public Guid GUID { get; internal set; }
        public int? ParentID { get; internal set; }
        public CategorySelectItemViewModel Parent { get; set; }
        public IList<CategorySelectItemViewModel> Categories { get; set; }
        public int CategoryLevel { get; internal set; }
    }
    public class CategorySelectModalDialogViewModel
    {
        public CategorySelectModalDialogViewModel(IList<CategorySelectItemViewModel> cats)
        {
            Categories = cats;
        }

        public bool IsError { get; set; } = false;
        public string ErrorMessage { get; set; }
        public IList<CategorySelectItemViewModel> Categories { get; internal set; }
        public int? MinimumSelectedCategoryNumber { get; set; }
        public int? MaximumSelectedCategoryNumber { get; set; }
    }
}
