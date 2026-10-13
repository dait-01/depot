# Depot Lab Curriculum

One product, grown lab by lab, from beginner to job-ready.

## Ground rules

1. **Never skip a gate.** If it doesn't pass, the next lab waits.
2. **Type the code.** Don't paste it.
3. **One commit per lab.** The commit message names the lab, for example `lab 0.1: hello depot`.
4. **Two lines in `DECISIONS.md` per lab:** what you decided and why. The important ones become ADRs.
5. **Evidence in `docs/labs/`.** Each lab gets a short page with the gate output (test results, screenshots, timings).
6. **From Phase 3 on:** Terraform only, and `terraform destroy` at the end of every session.

There are no week numbers. The gates set the pace.

## Phase 0: Build it (local, no cloud account)

| Lab | What you build | Gate |
|---|---|---|
| 0.1 Hello, Depot | .NET solution: `Depot.Core`, `Depot.Cli`, tests. Domain model and inventory service | 3/3 tests pass, the report prints |
| 0.2 Depot gets a door | Minimal HTTP API, DTOs kept separate from the domain | curl works, `Depot.Core` unchanged |
| 0.3 Ship it in a box | Multi-stage Dockerfile, non-root user, joins the floci network | Image under 250 MB, runs as non-root, answers on its port |
| 0.4 Networking fundamentals | CIDR, subnetting, DNS, TCP and TLS with dig, curl and openssl | You can explain a request end to end, including DNS and TLS |
| 0.5 Terraform basics | Local and Docker providers: variables, outputs, modules, state | One module you wrote is reused twice with different inputs |

## Phase 1: Make it remember (AWS services emulated locally)

| Lab | What you build | Gate |
|---|---|---|
| 1.1 Meet the cloud SDK | Write to S3 from C# using the AWS SDK | An object written from code is visible in the floci console |
| 1.2 Real storage | DynamoDB behind a repository, partition key design | Data survives a restart, and you can defend your key choice |
| 1.3 Stop hardcoding | Options pattern, Parameter Store and Secrets Manager | `git grep` finds no connection strings or secrets |
| 1.4 Two writers, one truth | Optimistic concurrency with conditional writes | 100 parallel shipments against 100 units leave exactly 0, never negative |

## Phase 2: Make it resilient (local)

| Lab | What you build | Gate |
|---|---|---|
| 2.1 Put a queue in the middle | API returns 202, `Depot.Worker` processes from SQS | The worker drains the queue |
| 2.2 Things fail | Retries, backoff, Polly, dead-letter queue | Kill the emulator mid-run: nothing lost, no retry storm |
| 2.3 Exactly once, sort of | Idempotency keys | The same message sent 3 times moves stock once |
| 2.4 Health and sight | Health endpoints, structured logs, OpenTelemetry traces | One request ID traced across the queue hop |
| 2.5 One command up | docker compose, declared networks, health checks | Clean clone, `docker compose up`, everything healthy |

## Phase 3: Ship it to AWS (real account)

| Lab | What you build | Gate |
|---|---|---|
| 3.1 Account hardening | Identity Center, root MFA, budget alerts, CloudTrail ([ADR-0002](adr/0002-aws-account-baseline.md)) | Root has no access keys, the budget alert fires on a test |
| 3.2 IAM deep dive | Roles, policies, permission boundary, assume a role from the CLI | Read-only S3 access is proven to deny delete |
| 3.3 Custom VPC | Public and private subnets across 2 AZs, NAT instance (fck-nat) | A private task reaches the internet but is not reachable from it |
| 3.4 Terraform on AWS | Modules, S3 remote state with native locking (`use_lockfile = true`) | `destroy` then `apply` recreates everything identically |
| 3.5 The robot builds it | GitHub Actions with OIDC: build, test, plan on PR, apply on merge | A failing test turns the pipeline red, and the PR shows the plan |
| 3.6 Run it on Fargate | Push to ECR, run Depot on ECS Fargate behind an ALB | A `git push` builds, scans and deploys |
| 3.7 Ship it, then take it back | ECS native blue/green deployment | A bad release is rolled back in under 2 minutes, timed |
| 3.8 Pipeline security | Trivy (images and IaC), Checkov, Gitleaks | A deliberately vulnerable commit fails the pipeline |

## Phase 4: Scale, secure, recover

| Lab | What you build | Gate |
|---|---|---|
| 4.1 Find the ceiling | Load test (k6), p95, target-tracking autoscaling, cache, re-measure | A before/after number you measured |
| 4.2 Observability on AWS | CloudWatch dashboards and alarms, traces through X-Ray or OpenTelemetry | An injected error raises an alarm, and you find the root cause in traces |
| 4.3 Data protection | KMS keys, Secrets Manager rotation, encryption enforced by policy | An unencrypted write is denied |
| 4.4 Edge and threat detection | CloudFront, WAF managed rules, GuardDuty | A simulated SQL injection request is blocked |
| 4.5 Backup and DR | AWS Backup, pilot-light stack in a second region | You restore and record your real RTO and RPO |
| 4.6 Serverless slice (optional) | One Depot endpoint as a .NET Lambda behind API Gateway | Same contract tests pass against both versions |

## Phase 5: Expert depth (mostly local)

| Lab | Tools | Gate |
|---|---|---|
| 5.1 Kubernetes | kind: deployments, probes, HPA, RBAC, NetworkPolicies | You debug a CrashLoopBackOff unaided |
| 5.2 GitOps | Argo CD with Helm or Kustomize | A Git commit changes the cluster, and drift is corrected |
| 5.3 Metrics and alerting | Prometheus, Grafana, Alertmanager | An SLO dashboard with burn-rate alerts |
| 5.4 Policy as code | Kyverno or OPA Gatekeeper | A privileged pod is rejected at admission |
| 5.5 Chaos and SRE | Chaos Mesh, blameless postmortem | Failure injected, detected and written up |
| 5.6 FinOps | Tagging, Cost Explorer, rightsizing | Three concrete savings explained |
| 5.7 Azure mapping | Rebuild the data and messaging layers on Azure equivalents (local floci-az) | A one-page service mapping, and Depot running on the Azure stack |

## Capstone: design review

- Capacity estimate for Depot at 100x current load.
- A 2-page architecture document organised by the six Well-Architected pillars.
- A final architecture diagram that reflects only what was actually built.
- Draw the system from memory and explain every arrow.

## Certifications, as proof of what was built

1. AWS Solutions Architect – Associate, after Phase 4
2. HashiCorp Terraform Associate, after Phase 3
3. CKA, after Phase 5.1–5.4

Check exam versions on the official sites before booking.
