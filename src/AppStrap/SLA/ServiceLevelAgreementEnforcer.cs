namespace AppStrap.SLA
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    // TODO: Move this out of framework.

    public sealed class ServiceLevelAgreementEnforcer
    {
        public void Enforce(Action action, IReadOnlyCollection<IAgreeOnServiceLevels> serviceLevelAgreements)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            if (serviceLevelAgreements == null)
                throw new ArgumentNullException(nameof(serviceLevelAgreements));

            var sw = new Stopwatch();
            sw.Start();
            action.Invoke();
            sw.Stop();

            var violations =
                (
                    from sla in serviceLevelAgreements
                    where sw.ElapsedMilliseconds > sla.AcceptableExecutionTime
                    select new ServiceLevelAgreementViolation(sw.ElapsedMilliseconds, sla)
                    ).ToList();

            if (violations.Any())
                throw new ServiceLevelAgreementViolationException(violations);
        }
    }
}