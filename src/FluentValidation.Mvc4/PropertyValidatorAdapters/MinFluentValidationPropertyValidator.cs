namespace FluentValidation.Mvc {
    using System.Web.Mvc;
    using Ext.FluentValidation.Internal;
    using Ext.FluentValidation.Validators;

    internal class MinFluentValidationPropertyValidator : AbstractComparisonFluentValidationPropertyValidator<GreaterThanOrEqualValidator> {

        protected override object MinValue {
            get { return AbstractComparisonValidator.ValueToCompare;  }
        }

        protected override object MaxValue {
            get { return null; }
        }

        public MinFluentValidationPropertyValidator(ModelMetadata metadata, ControllerContext controllerContext, PropertyRule propertyDescription, IPropertyValidator validator)
            : base(metadata, controllerContext, propertyDescription, validator) {
        }
    }
}