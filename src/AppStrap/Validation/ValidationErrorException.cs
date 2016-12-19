namespace AppStrap.Validation
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

    public class ValidationErrorException : Exception
	{
        public IReadOnlyCollection<ValidationFault> ValidationFaults { get; private set; }
        public Type FaultedType { get; set; }

        public ValidationErrorException(string message, IReadOnlyCollection<ValidationFault> validationFaults)
			: base(message)
		{
		    if (validationFaults == null)
                throw new ArgumentNullException(nameof(validationFaults));

		   ValidationFaults = validationFaults;
		}

        public ValidationErrorException(IReadOnlyCollection<ValidationFault> validationFaults)
			: base(BuildMessage(validationFaults))
        {
            
            if (validationFaults == null) throw new ArgumentNullException(nameof(validationFaults));
           ValidationFaults = validationFaults;
        }

        public ValidationErrorException(ValidationResult validationResult) : base(BuildMessage(validationResult.Faults))
		{
			ValidationFaults = validationResult.Faults;
		}

		private static string BuildMessage(IEnumerable<ValidationFault> faults)
		{
		    var arr = faults.Select(
		        x =>
		            "\r\n\t-- " + "Invalid Argument.  " +
		            string.Format("Argument Type: \"{0}\"  ", x.AttemptedValue.GetType().Name) +
		            string.Format("Argument Name: \"{0}\"  ", x.PropertyName) +
		            string.Format("Attempted Value: \"{0}\"  ", x.AttemptedValue) +
		            string.Format("ErrorMessage: \"{0}\"", x.ErrorMessage)
            ).ToArray();

			var errorMessage =  string.Format("Validation failed for the following reasons(s): {0}", string.Join("", arr));
		    return errorMessage;
		}
	}
}
