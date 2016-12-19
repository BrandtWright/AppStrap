namespace AppStrap.Validation
{
    using System;
    using Types;

    public sealed class ValidationFault
    {
        public ValidationFault(object attemptedValue, Maybe<object> customState, 
            string errorMessage, string propertyName)
        {
            if (attemptedValue == null)
                throw new ArgumentNullException("attemptedValue");
            
            if (customState == null)
                throw new ArgumentNullException("customState");
            
            if (errorMessage == null)
                throw new ArgumentNullException("errorMessage");

            if (string.IsNullOrWhiteSpace(propertyName)) 
                throw new ArgumentException("Can not be null, empty or whitespace", "propertyName");

            AttemptedValue = attemptedValue;
            CustomState = customState;
            ErrorMessage = errorMessage;
            PropertyName = propertyName;
        }

        public object AttemptedValue  { get; private set; }
        public Maybe<object> CustomState { get; private set; }
        public string ErrorMessage { get; private set; }
        public string PropertyName { get; private set; }
    }
}