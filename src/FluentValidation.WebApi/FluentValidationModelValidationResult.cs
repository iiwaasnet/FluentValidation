namespace Ext.FluentValidation.WebApi {
    using System.Collections.Generic;
    using System.Web.Http.Validation;

    public class FluentValidationModelValidationResult : ModelValidationResult
    {
        public string ErrorCode { get; set; }
        public IDictionary<string, object> PlaceholderValues { get; set; }

    }

}