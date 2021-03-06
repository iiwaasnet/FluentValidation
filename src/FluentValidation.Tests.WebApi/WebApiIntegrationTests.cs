﻿#region License
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

using System.Linq;

namespace Ext.FluentValidation.Tests.WebApi {
    using NUnit.Framework;

    [TestFixture]
	public class WebApiIntegrationTests : WebApiBaseTest {
		[Test]
		public void Should_add_all_erorrs_in_one_go_when_NotEmpty_rule_specified_for_non_nullable_value_type() {
			var result = InvokeTest<TestModel5>(@"{
				SomeBool:'false',
				Id:0}");

			result.IsValidField("model.SomeBool").ShouldBeFalse(); //Complex rule
			result.IsValidField("model.Id").ShouldBeFalse(); //NotEmpty for non-nullable value type
		}

		[Test]
		public void Should_add_all_errors_in_one_go() {
			var result = InvokeTest<TestModel4>(@"Email=foo&Surname=foo&Forename=foo&DateOfBirth=&Address1=");

			result.Count.ShouldEqual(4);
			result.IsValidField("model.Email").ShouldBeFalse(); //Email validation failed
			result.IsValidField("model.DateOfBirth").ShouldBeFalse(); //Date of Birth not specified (implicit required error)
			result.IsValidField("model.Surname").ShouldBeFalse(); //cross-property
			result.IsValidField("model.Address1").ShouldBeFalse(); 
		}

		[Test]
		public void When_a_validation_error_occurs_the_error_should_be_added_to_modelstate() {
			var result = InvokeTest<TestModel>(@"Name=");
			result.GetMessage("model.Name").ShouldEqual("Validation Failed");            
		}

        [Test]
		public void When_two_validators_fails_for_same_property_two_validation_errors_should_be_added_to_modelstate() {
			var result = InvokeTest<TestModel9>(@"Name=");
            Assert.IsTrue(result.GetMessages("model.Name").Contains("Should not be null"));
            Assert.IsTrue(result.GetMessages("model.Name").Contains("Should equal 'Bla'"));
		}

		[Test]
		public void Should_not_fail_when_no_validator_can_be_found() {
			var result = InvokeTest<TestModel2>(@"");
			result.IsValid().ShouldBeTrue();
		}
		
		[Test]
		public void Should_add_default_message_to_modelstate_when_there_is_no_required_validator_explicitly_specified() {
			var result = InvokeTest<TestModel6>(@"Id=");
			result.Count.ShouldEqual(1);
			result.IsValidField("model.Id").ShouldBeFalse();
			result.GetMessage("model.Id").ShouldEqual(@"A value is required.");
		}


		[Test]
		public void Should_add_implicit_required_validator() {
			var result = InvokeTest<TestModel6>(@"Id=");
			result.Count.ShouldEqual(1);
			result.IsValidField("model.Id").ShouldBeFalse();
			result.GetMessage("model.Id").ShouldEqual(@"A value is required.");
		}


		[Test]
		public void Should_validate_less_than() {
			 var result = InvokeTest<TestModel7>(@"AnIntProperty=15");
			result.IsValidField("model.AnIntProperty").ShouldBeFalse();
			result.GetMessage("model.AnIntProperty").ShouldEqual("Less than 10");
		}

		[Test]
		public void Should_validate_custom_after_property_errors() {
			var result = InvokeTest<TestModel7>(@"AnIntProperty=7&CustomProperty=14");

			result.IsValidField("model.CustomProperty").ShouldBeFalse();
			result.GetMessage("model.CustomProperty").ShouldEqual("Cannot be 14");

		}
	}
}