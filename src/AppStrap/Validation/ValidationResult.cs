namespace AppStrap.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class ValidationResult
    {
        private readonly List<ValidationFault> _faults = new List<ValidationFault>();

        public bool IsValid => Faults.Count.Equals(0);
        public IReadOnlyCollection<ValidationFault> Faults => _faults;

        public ValidationResult(IReadOnlyCollection<ValidationFault> faults)
        {
            if (faults == null)
                throw new ArgumentNullException(nameof(faults));

            _faults.AddRange(faults.Where(fault => fault != null));
        }
    }
}