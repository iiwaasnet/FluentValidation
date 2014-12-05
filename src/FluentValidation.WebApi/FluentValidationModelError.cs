namespace FluentValidation.WebApi {
    using System;
    using System.Collections.Generic;
    using System.Web.Http.ModelBinding;

    public class FluentValidationModelError : ModelError {
        public FluentValidationModelError(Exception exception)
            : base(exception) {
            PlaceholderValues = new Dictionary<string, object>();
        }

        public FluentValidationModelError(Exception exception, string errorMessage)
            : base(exception, errorMessage) {
            PlaceholderValues = new Dictionary<string, object>();
        }

        public FluentValidationModelError(string errorMessage)
            : base(errorMessage) {
            PlaceholderValues = new Dictionary<string, object>();
        }

        public string ErrorCode { get; set; }
        public IDictionary<string, object> PlaceholderValues { get; set; }
    }
}