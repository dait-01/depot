# ADR-0002: AWS account baseline

- **Status:** Proposed (to be accepted in Lab 3.1)
- **Date:** to be set when accepted
- **Related lab:** 3.1 Account hardening

## Context

Phase 3 deploys Depot to a real AWS account. The account needs a secure, auditable, cost-controlled baseline before any workload runs, because every later lab depends on it.

New AWS accounts choose between a Free plan and a Paid plan at sign-up. The Free plan includes up to $200 in credits, lasts at most 6 months, blocks some higher-cost services, and closes the account automatically when it ends. The Paid plan keeps the credits but bills normally once they run out.

## Decisions (to confirm in Lab 3.1)

### 1. Account plan
- **Proposed:** start on the Free plan. Upgrade to Paid before any lab that needs a restricted service (expected: EKS, GuardDuty, multi-region DR).
- **Why:** the Free plan carries no billing risk while learning the basics. Upgrading keeps any unused credits.
- **Risk:** the account closes automatically when the Free plan ends, deleting all resources. Everything must therefore be reproducible from Terraform.

### 2. Daily-use identity
- **Proposed:** IAM Identity Center with short-lived credentials (`aws sso login`).
- **Rejected:** an IAM user with long-lived access keys. It is simpler, but long-lived keys are the most common way credentials leak.

### 3. Root user
- MFA enabled, no access keys, used only for the few tasks that require root.
- **Why:** IAM policies cannot restrict root, so it is locked away.

### 4. Home region
- **Proposed:** us-east-1, with a second region only for the DR lab.
- **Why:** broadest service availability and lowest prices. Latency doesn't matter for labs.

### 5. Cost controls
- AWS Budgets: monthly budget of $5, with alerts at 80% actual and 100% forecasted.
- Free Tier usage alerts enabled.
- **Why:** forgotten resources (NAT, ALB, RDS) are the main source of surprise bills.

### 6. Audit logging
- CloudTrail trail covering all regions, management events, log file validation on, delivered to a dedicated private S3 bucket.
- **Why:** a tamper-evident record of every API call, needed for troubleshooting and the later security labs.

### 7. Account-level guardrails
- S3 Block Public Access on at the account level.
- **Why:** prevents the most common beginner mistake, an accidentally public bucket.

## Consequences

- **Positive:** a secure baseline, spend visibility and a full audit trail from day one.
- **Negative:** Identity Center needs a periodic re-login. CloudTrail log storage adds a small cost.
- **Follow-up:** manage these resources with Terraform from Lab 3.4.

## Verification (filled in during Lab 3.1)

| Check | Result |
|---|---|
| Root has no access keys | |
| Root MFA enabled | |
| Budget alert email received | |
| A CreateBucket event is visible in CloudTrail | |
| `aws sts get-caller-identity` shows a non-root ARN | |
