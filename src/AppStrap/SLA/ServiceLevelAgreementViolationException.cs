namespace AppStrap.SLA
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class ServiceLevelAgreementViolationException : Exception
    {
        public IReadOnlyCollection<ServiceLevelAgreementViolation> ServiceLevelAgreementResults { get; set; }

        public ServiceLevelAgreementViolationException(IReadOnlyCollection<ServiceLevelAgreementViolation> violations) 
            : base(BuildMessage(violations))
        {
            if (violations == null) throw new ArgumentNullException(nameof(violations));
            ServiceLevelAgreementResults = violations;
        }

        public ServiceLevelAgreementViolationException(IReadOnlyCollection<ServiceLevelAgreementViolation> violations, string message) : base(message)
        {
            ServiceLevelAgreementResults = violations;
        }

        private static string BuildMessage(IEnumerable<ServiceLevelAgreementViolation> violations)
        {
            var messages = violations
                .Select(violation => string.Format(
                    "\r\n\t\t-- {0} enforces an SLA of {1} milliseconds but a {2} millisecond delay was observed", 
                    violation.ServiceLevelAgreement.GetType().Name, 
                    violation.ServiceLevelAgreement.AcceptableExecutionTime, 
                    violation.ActualExecutionTime)).ToList();
            return "Service level agreement failed: " + string.Join("", messages);
        }
    }
}