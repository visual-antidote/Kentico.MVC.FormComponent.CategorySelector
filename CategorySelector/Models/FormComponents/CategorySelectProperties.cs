using CMS.DataEngine;
using Kentico.Forms.Web.Mvc;
using System;
using System.Collections.Generic;

namespace VisualAntidote.Kentico.MVC.FormComponent.CategorySelector.Models.FormComponents
{
    public class CategorySelectProperties : FormComponentProperties<List<string>>
    {
        public CategorySelectProperties() : base(FieldDataType.LongText)
        {
        }

        public override List<string> DefaultValue { get; set; }

        /// <summary>
        /// To include categories from other sites, supply a comma-separated list of site code names
        /// </summary>
        [EditingComponent(TextInputComponent.IDENTIFIER, Label = "Include Sites", DefaultValue = "", Tooltip = "To include categories from other sites, supply a comma-separated list of site code names", ExplanationText = "To include categories from other sites, supply a comma-separated list of site code names", Order = -95)]
        public string IncludeSites { get; set; }

        /// <summary>
        /// Include Global categories
        /// </summary>
        [EditingComponent(CheckBoxComponent.IDENTIFIER, Label = "Include Global categories", DefaultValue = true, Tooltip = "Include Global categories", ExplanationText = "Include global categories", Order = -98)]
        public bool IncludeGlobalCategories { get; set; } = true;

        /// <summary>
        /// Include Disabled categories
        /// </summary>
        [EditingComponent(CheckBoxComponent.IDENTIFIER, Label = "Include Disabled categories", DefaultValue = false, Tooltip = "Include disabled categories", ExplanationText = "Include disabled categories", Order = -99)]
        public bool IncludeDisabledCategories { get; set; } = false;

        /// <summary>
        /// Minimum number of selected categories
        /// </summary>
        [EditingComponent(IntInputComponent.IDENTIFIER, Label = "Minimum number of categories", DefaultValue = 0, Tooltip = "At least this many categories must be selected", ExplanationText = "Invalid if the number of selected categories is less than this..", Order = -100)]
        public int? MinimumSelectedCategoryNumber { get; set; } = 0;

        /// <summary>
        /// Maximum number of selected categories
        /// </summary>
        [EditingComponent(IntInputComponent.IDENTIFIER, Label = "Maximum number of categories", DefaultValue = null, Tooltip = "At most this many categories can be selected", ExplanationText = "Invalid if the number of selected categories is more than this.", Order = -101)]
        public int? MaximumSelectedCategoryNumber { get; set; } = null;
    }
}
