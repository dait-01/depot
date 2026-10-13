# ADR-0001: Use Depot (C#) as the single workload for the portfolio

- **Status:** Accepted
- **Date:** 2026-10-10

## Context

I had three learning tracks running at once: a generic AWS lab portfolio planned in Java, the Depot DevOps and system design labs in C#, and a separate C# design-patterns track. Having all three going stalled each of them. My working language and coursework are C#.

## Decision

- The portfolio is built around one product, **Depot**, written in **C# / .NET 10**.
- Phases 0–2 run locally, with AWS services emulated by floci. Phases 3–4 deploy to real AWS.
- AWS is the primary cloud. Azure appears as a mapping exercise in Phase 5.
- The design-patterns track is paused until this one reaches the end of Phase 3.

## Options considered

- **Java / Spring Boot as the primary language (original plan).** Rejected: it adds a third language while I'm still building C# fluency.
- **Separate repos for Depot and the portfolio.** Rejected: the same skills would be split across two half-finished repos.
- **Azure as the primary cloud.** Rejected for now: AWS has wider demand in the roles I'm targeting. Azure is kept as a comparison.

## Consequences

- **Positive:** one coherent story (a real system that gets hardened, deployed and rolled back), and no money spent until Phase 3.
- **Negative:** the local labs depend on the floci emulator behaving like real AWS. Differences will be recorded when they appear.
- **Follow-up:** decide the AWS account plan (Free vs Paid) in ADR-0002 before Phase 3.
