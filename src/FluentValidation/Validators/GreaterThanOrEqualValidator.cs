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
// The latest version of this file can be found at http://www.codeplex.com/FluentValidation
#endregion

namespace Ext.FluentValidation.Validators {
    using System;
    using System.Reflection;
    using Internal;
    using Resources;

    public class GreaterThanOrEqualValidator : AbstractComparisonValidator  {
		public GreaterThanOrEqualValidator(IComparable value) : base(value, () => Messages.greaterthanorequal_error) {
		}

		public GreaterThanOrEqualValidator(Func<object, object> valueToCompareFunc, MemberInfo member)
			: base(valueToCompareFunc, member, () => Messages.greaterthanorequal_error) {
		}

		public override bool IsValid(IComparable value, IComparable valueToCompare) {
			if (valueToCompare == null)
				return false;

			return Comparer.GetComparisonResult(value, valueToCompare) >= 0;
		}

		public override Comparison Comparison {
			get { return Validators.Comparison.GreaterThanOrEqual; }
		}
	}
}