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
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Resources;
    using Results;
    using TaskHelpers;

    public abstract class NoopPropertyValidator : IPropertyValidator {
		public IStringSource ErrorMessageSource {
			get { return null; }
			set { }
		}

		public virtual bool IsAsync {
			get { return false; }
		}

		public abstract IEnumerable<ValidationFailure> Validate(PropertyValidatorContext context);

		public virtual Task<IEnumerable<ValidationFailure>> ValidateAsync(PropertyValidatorContext context) {
			return TaskHelpers.FromResult(Validate(context));
		}

		public virtual ICollection<Func<object, object, object>> CustomMessageFormatArguments {
			get { return new List<Func<object, object, object>>(); }
		}

		public virtual bool SupportsStandaloneValidation {
			get { return false; }
		}

		public Func<object, object> CustomStateProvider {
			get { return null; }
			set { }
		}
	}
}