# Depot: Solution Architect + DevOps Lab Portfolio

I'm building one real product, **Depot**, an inventory service in C# / .NET 10, and taking it lab by lab from a console app to a secured, observable, blue/green-deployed system on AWS.

Every lab ends with a **gate**: a check that has to pass before the next lab starts. Evidence for each gate lives in the lab's notes.

> **Status:** Building in public. Currently on **Lab 0.1: Hello, Depot**.

## The one rule this system must keep

**On-hand stock must never go negative.**

That's easy in one process and hard across many. Each phase puts new pressure on this same rule: concurrent writers, queues, retries, failures, deployments and load.

## What this repo will demonstrate

Items get ticked off as their gates pass.

- [ ] Clean domain design in C# with tests (`Depot.Core` has no infrastructure dependencies)
- [ ] Containerised services: multi-stage builds, non-root, small images
- [ ] Data modelling and concurrency control (DynamoDB, conditional writes)
- [ ] Asynchronous processing with queues, retries, dead-letter queues and idempotency
- [ ] Observability: health checks, structured logs, distributed tracing
- [ ] Infrastructure as code with Terraform and remote state
- [ ] CI/CD with GitHub Actions and OIDC (no stored cloud keys)
- [ ] Blue/green deployment on ECS Fargate with a timed rollback
- [ ] Security scanning in the pipeline (Trivy, Checkov, Gitleaks)
- [ ] Load testing, autoscaling, backup and disaster recovery with measured RTO/RPO
- [ ] Architecture decisions recorded as ADRs, reviewed against the Well-Architected Framework

## How it's built

- **Local first.** Phases 0–2 run on my machine. AWS services are emulated locally (floci), so no cloud account is needed and no money is spent while learning.
- **Real AWS for Phases 3–4.** Everything is built with Terraform and torn down after each session.
- **Azure comparison in Phase 5.** The same design is mapped to Azure equivalents.

## Lab progress

See [`docs/curriculum.md`](docs/curriculum.md) for every lab and its gate.

| Lab | Topic | Status | Notes |
|---|---|---|---|
| 0.1 | Hello, Depot: solution, domain, tests | ✅ | [notes](docs/labs/0.1-hello-depot.md) |
| 0.2 | Depot gets a door: HTTP API | ☐ | |
| 0.3 | Ship it in a box: Docker | ☐ | |
| 0.4 | Networking fundamentals | ☐ | |
| 0.5 | Terraform basics | ☐ | |
| 1.x | Make it remember (storage, config, concurrency) | ☐ | |
| 2.x | Make it resilient (queues, failure, idempotency, observability) | ☐ | |
| 3.x | Ship it to AWS (IAM, VPC, CI/CD, ECS, blue/green) | ☐ | [ADR-0002](docs/adr/0002-aws-account-baseline.md) |
| 4.x | Scale, secure, recover | ☐ | |
| 5.x | Expert depth (Kubernetes, GitOps, SRE, FinOps, Azure) | ☐ | |

## Repository layout

```
.
├── src/                  # Depot.Core, Depot.Cli, later Depot.Api and Depot.Worker
├── tests/                # xUnit tests
├── infra/                # Terraform (from Phase 3)
├── docs/
│   ├── curriculum.md
│   ├── adr/              # architecture decision records
│   ├── labs/             # one page of notes and gate evidence per lab
│   └── diagrams/
├── .github/workflows/    # CI/CD (from Lab 3.5)
└── DECISIONS.md          # two-line decision log per lab, promoted to ADRs
```

## Tech stack

C# / .NET 10, ASP.NET Core, xUnit, Docker, Terraform, GitHub Actions, AWS (ECS Fargate, DynamoDB, SQS, CloudWatch), OpenTelemetry. Later: Kubernetes (kind), Argo CD, Prometheus, Grafana, Azure.

## Costs

Real costs from each AWS session will be recorded here as they happen.

## Security

No credentials are stored in this repo. Secrets scanning runs before every push. CI will authenticate to AWS through GitHub OIDC from Lab 3.5.

## About

Built by Danny on the path to Solution Architect and DevOps roles. Contact: [GitHub](https://github.com/dait-01)
