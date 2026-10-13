# Decision log

Two lines per lab: what I decided, and why. Important entries are promoted to ADRs in `docs/adr/`.

## Setup
- Decided: one product (Depot, C#) is the whole portfolio. Why: three parallel tracks were stalling each other. See ADR-0001.
- Decided: local first, with AWS emulated by floci until Phase 3. Why: learn without spending money or using up the account's time.

## Lab 0.1: Hello, Depot
- Decided: three projects, with `Depot.Core` depending on nothing and `Depot.Cli` and the tests depending on Core. Why: the business rules must not know about consoles, HTTP or databases, so later labs can swap those without touching the rules.
- Decided: the "never negative" rule is enforced inside `InventoryService.Ship`, which throws `InsufficientStockException`, not checked by callers. Why: if every caller had to remember the check, one would eventually forget it.
- Decided: domain failures are specific exception types (`UnknownSkuException`, `InsufficientStockException`). Why: tests and future API code can react to each failure precisely instead of parsing error messages.
- Decided: stock is held in memory for now. Why: storage arrives in Phase 1. The goal here was proving the rules with tests first.
- Note: the projects target net8.0 because that was the installed SDK. Retarget to net10.0 at the start of Lab 0.2, since .NET 8 support ends November 2026.