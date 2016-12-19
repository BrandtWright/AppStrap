namespace AppStrap.SLA
{
    using System;

    public sealed class ServiceLevelAgreementViolation
    {
        public ServiceLevelAgreementViolation(long actualExecutionTime, IAgreeOnServiceLevels serviceLevelAgreement)
        {
            if (serviceLevelAgreement == null)
                throw new ArgumentNullException(nameof(serviceLevelAgreement));

            if (actualExecutionTime <= serviceLevelAgreement.AcceptableExecutionTime)
                throw new InvalidOperationException(
                    "actualExecution time must be greater than serviceLevelAgreement.AcceptableExecutionTime.");

            ActualExecutionTime = actualExecutionTime;
            ServiceLevelAgreement = serviceLevelAgreement;
        }

        public long ActualExecutionTime { get; private set; }

        public IAgreeOnServiceLevels ServiceLevelAgreement { get; private set; }
    }
}