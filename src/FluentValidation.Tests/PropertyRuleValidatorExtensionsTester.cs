#region License
// Copyright (c) Jeremy Skinner (http://www.jeremyskinner.co.uk)
// 
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
// 
// http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
// 
// The latest version of this file can be found at http://fluentvalidation.codeplex.com
#endregion

namespace Ext.FluentValidation.Tests {
    using System.Linq;
    using Ext.FluentValidation;
    using Ext.FluentValidation.Validators;
    using NUnit.Framework;

    [TestFixture]
	public class PropertyRuleValidatorExtensionsTester {
		AbstractValidator<Person> validator;

		[SetUp]
		public void Setup() {
			validator = new TestValidator();
		}

		[Test]
		public void RemovePropertyValidator_should_remove_property() {
			validator.RuleFor(x => x.Surname).Length(5, 10).WithMessage("foo");

			var result = validator.Validate(new Person {Surname = "Matthew Leibowitz"});
			result.Errors.Single().ErrorMessage.ShouldEqual("foo");

			validator.RemoveRule(x => x.Surname, typeof(LengthValidator));

			result = validator.Validate(new Person {Surname = "Matthew Leibowitz"});
			Assert.AreEqual(0, result.Errors.Count);
		}

		[Test]
		public void ReplacePropertyValidator_should_replace_property() {
			validator.RuleFor(x => x.Surname).Length(5, 10).WithMessage("foo");

			var result = validator.Validate(new Person {Surname = "Matthew Leibowitz"});
			result.Errors.Single().ErrorMessage.ShouldEqual("foo");

			validator.ReplaceRule(x => x.Surname, new LengthValidator(10, 20));

			result = validator.Validate(new Person {Surname = "Matthew Leibowitz"});
			Assert.AreEqual(0, result.Errors.Count);
		}

		[Test]
		public void ReplacePropertyValidator_should_replace_field_rule() {
			validator.RuleFor(x => x.NameField).Length(5, 10).WithMessage("foo");

			var result = validator.Validate(new Person { NameField = "Matthew Leibowitz" });
			result.Errors.Single().ErrorMessage.ShouldEqual("foo");

			validator.ReplaceRule(x => x.NameField, new LengthValidator(10, 20));

			result = validator.Validate(new Person { NameField = "Matthew Leibowitz" });
			Assert.AreEqual(0, result.Errors.Count);
		}

		[Test]
		public void ClearPropertyValidator_should_remove_property() {
			validator.RuleFor(x => x.Surname).Length(5, 10).WithMessage("foo");

			var result = validator.Validate(new Person {Surname = "Matthew Leibowitz"});
			result.Errors.Single().ErrorMessage.ShouldEqual("foo");

			validator.ClearRules(x => x.Surname);

			result = validator.Validate(new Person {Surname = "Matthew Leibowitz"});
			Assert.AreEqual(0, result.Errors.Count);
		}
	}
}