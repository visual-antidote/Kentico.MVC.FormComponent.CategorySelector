using Kentico.Forms.Web.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using VisualAntidote.Kentico.MVC.FormComponent.CategorySelector.Models.FormComponents;

[assembly: RegisterFormComponent(CategorySelectComponent.IDENTIFIER, typeof(CategorySelectComponent), "Category Select Form Component",
    IsAvailableInFormBuilderEditor = false, ViewName = "FormComponents/_CategorySelectComponent")]
namespace VisualAntidote.Kentico.MVC.FormComponent.CategorySelector.Models.FormComponents
{
    /// <summary>
    /// Represents a Category Selector. All categories from the current site will be listed. Global categories can be included/excluded via the CategorySelectProperties.IncludeGlobalCategories property.  To include categories from other sites, use the CategorySelectProperties.IncludeSites property.
    /// </summary>
    public class CategorySelectComponent : FormComponent<CategorySelectProperties, List<string>>
    {
        public const string IDENTIFIER = "VisualAntidote.Category.Select.Form.Component";

        [BindableProperty]
        public String CategoryCodeNameListAsText { get; set; }

        public String CategoriesSelectedCount
        {
            get
            {
                var count = "0";

                if (!String.IsNullOrEmpty(CategoryCodeNameListAsText))
                {
                    count = CategoryCodeNameListAsText.Split(',').ToList().Count.ToString();
                }

                return $"{count}";
            }
        }

        public String IncludeSites
        {
            get
            {
                if (this.Properties != null && !String.IsNullOrEmpty(this.Properties.IncludeSites))
                {
                    return this.Properties.IncludeSites;
                }

                return "";
            }
        }


        public bool IncludeGlobalCategories
        {
            get
            {
                if (this.Properties != null)
                {
                    return this.Properties.IncludeGlobalCategories;
                }

                return true;
            }
        }

        public bool IncludeDisabledCategories
        {
            get
            {
                if (this.Properties != null)
                {
                    return this.Properties.IncludeDisabledCategories;
                }

                return true;
            }
        }

        public override List<string> GetValue()
        {
            return _ConvertCategoryFieldToList();
        }

        public override void SetValue(List<string> value)
        {
            if (value != null)
            {
                CategoryCodeNameListAsText = String.Join(",", value);
            }
        }

        private List<string> _ConvertCategoryFieldToList()
        {
            var categoryCodeNameList = new List<string>();
            if (!String.IsNullOrEmpty(CategoryCodeNameListAsText))
            {
                categoryCodeNameList = CategoryCodeNameListAsText.Split(',').ToList();
            }

            return categoryCodeNameList;
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //var categoriesQuery = CategoryInfoProvider
            //    .GetCategoriesDocumentsWhereCondition(_ConvertCategoryFieldToList(), true);

            return base.Validate(validationContext);
        }
    }
}
