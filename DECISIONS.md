# Decision log

Two lines per lab: what I decided, and why. Important entries are promoted to ADRs in `docs/adr/`.

## Setup
- Decided: one product (Depot, C# / .NET 10) is the whole portfolio. Why: three parallel tracks were stalling each other. See ADR-0001.
- Decided: local first, with AWS emulated by floci until Phase 3. Why: learn without spending money or using up the account's time.
